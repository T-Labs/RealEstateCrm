using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using WebApp.ViewModels;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(AuthPolicy.Employees)]
    public class HousingController : BaseController
    {
        private IAuthorizationService AuthService { get; set; }
        public HousingController(ApplicationDbContext context, IAuthorizationService auth) : base(context)
        {
            AuthService = auth;
        }

        public IActionResult Index(string filter, 
            int page = 1, 
            int? houseType = null, 
            int? cityId = null, 
            int? districtId = null, 
            int? minCost = null, 
            int? maxCost = null, 
            int? objectId = null, 
            bool? isArchive = null)
        {
            var typesHousings = _context.TypesHousing.ToList();
            
            if (User.IsInRole(RoleNames.Employee))
            {
                cityId = CurrentUser?.City?.Id;
            }

            var filterData = new HousingExtensions.FilterParams()
            {
                CityId = cityId,
                PriceTo = minCost,
                PriceFrom = maxCost,
                Page = page,
                IsArchived = isArchive
            };

            if (houseType.HasValue)
            {
                filterData.HouseTypeId = new int[] { houseType.Value };
            }


            var query = _context.Housing.Where(x => HousingExtensions.Filter(filterData)(x));
            
            int totalPages;
            int totalItems;
            var queryResult = query.GetPage(page, out totalItems, out totalPages);
            var items = queryResult.Select(x => HousingEditModel.Create(x, typesHousings, User)).ToList();

            ViewBag.TotalItems = _context.Housing.Count();
            ViewBag.FilteredItemsCount = totalItems;

            var model = new HousingIndexModel
            {
                Items = items,
                Filters = new HousingIndexFilterModel
                {
                    IsArchived = isArchive ?? false,
                    HousingTypeId = houseType ?? 0,
                    CityId = cityId ?? 0,
                    DistrictId = districtId ?? 0
                },
                TotalPages = totalPages,
                CurrentPage = page
            };
            
            return View(model);
        }


        public IActionResult Cards(string filter,
            int page = 1,
            int? houseType = null,
            int? cityId = null,
            int? districtId = null,
            int? minCost = null,
            int? maxCost = null,
            int? objectId = null,
            bool? isArchive = null)
        {
            return Index(filter, page, houseType, cityId, districtId, minCost, maxCost, objectId, isArchive);
        }

        [HttpPost]
        public IActionResult Filter(HousingIndexModel model, int page = 1)
        {
            var filters = model.Filters;
            if (filters == null)
            {
                return RedirectToAction("Index", new { page });
            }

            Func<int?, int?> intOrNull = (value) =>
            {
                if (value.HasValue && value.Value > 0)
                {
                    return value;
                }
                return null;
            };
            
            return RedirectToAction("Index", new
            {
                page,
                houseType = intOrNull(filters.HousingTypeId),
                cityId = intOrNull(filters.CityId),
                districtId = intOrNull(filters.DistrictId),
                minCost = intOrNull(filters.MinCost),
                maxCost = intOrNull(filters.MaxCost),
                objectId = intOrNull(filters.SelectedObjectId),
                isArchive = filters.IsArchived
            });
        }
        

        public IActionResult ResetFilter(int? page = 1)
        {
            return RedirectToAction("Index", new { page });
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Housing housing = _context.Housing.GetFullById(id.Value);
            if (housing == null)
            {
                return HttpNotFound();
            }
            var model = HousingEditModel.Create(_context, housing, User);
            return View(model);
        }
        
        [Authorize(AuthPolicy.CreateHousing)]
        public IActionResult Create()
        {
            var city = User.IsInRole(RoleNames.Employee) ? CurrentUser?.City : _context.Cities.Include(x => x.Districts).FirstOrDefault();
            var housing = new Housing()
            {
                City = city,
                CityId = city?.Id ?? 0,
                Phones = new List<HousingPhone>(),
                Calls = new List<HousingCall>()
            };
            var model = HousingEditModel.Create(_context, housing, User);
            return View("Save", model);
        }

        [HttpPost]
        [Authorize(AuthPolicy.CreateHousing)]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HousingEditModel housing)
        {
            if (ModelState.IsValid)
            {
                var newHousingItem = new Housing()
                {
                    Phones = new List<HousingPhone>()
                };
                housing.UpdateEntity(newHousingItem);
                _context.Housing.Add(newHousingItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Save", housing);
        }

        [Authorize(AuthPolicy.EditHousing)]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            
            Housing housing = _context.Housing.GetFullById(id.Value);
            if (housing == null)
            {
                return HttpNotFound();
            }

            var model = HousingEditModel.Create(_context, housing, User);
            return View("Save", model);
        }

        [HttpPost]
        [Authorize(AuthPolicy.EditHousing)]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HousingEditModel housing, int editId)
        {
            if (ModelState.IsValid)
            {
                var dbItem = _context.Housing.GetFullById(editId);
                housing.UpdateEntity(dbItem);
                _context.Update(dbItem);
                _context.SaveChanges();

                TempData["CrmSuccessMessage"] = "Запись была успешно сохранена";
                return RedirectToAction("Index");
            }

            var errors = ModelState.Values.Where(x => x.Errors.Any()).ToList();
            return View("Save", housing);
        }

        [Authorize(AuthPolicy.DeleteHousing)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Housing housing = _context.Housing.Single(m => m.Id == id);
            _context.Housing.Remove(housing);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(AuthPolicy.EditHousing)]
        public IActionResult ToArchive(int id)
        {
            Housing housing = _context.Housing.Single(m => m.Id == id);
            housing.IsArchive = true;
            _context.Update(housing);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(AuthPolicy.EditHousing)]
        public IActionResult FromArchive(int id)
        {
            Housing housing = _context.Housing.Single(m => m.Id == id);
            housing.IsArchive = false;
            _context.Update(housing);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize(AuthPolicy.EditHousing)]
        public JsonResult AddCall(int housingId, string status)
        {
            var housing = _context.Housing.Include(x => x.Calls).Single(m => m.Id == housingId);
            if (housing == null)
            {
                return Json(new { status = "failed", message = "Housing not found"});
            }

            HousingCallType ctype;
            Enum.TryParse(status, out ctype);

            housing.Calls.Add(new HousingCall
            {
                User = CurrentUser,
                Status = status,
                StatusType = ctype
            });

            _context.Update(housing);
            _context.SaveChanges();

            return Json(new { status = "ok" });
        }

        public IActionResult DetailsDialog(int id)
        {
            var h = _context.Housing.GetFullById(id);
            return PartialView("DetailsDialog", HousingEditModel.Create(_context, h, User));
        }
    }
}

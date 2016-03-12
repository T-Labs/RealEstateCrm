using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Newtonsoft.Json;
using RealEstateCrm.ViewModels;
using WebApp;
using RealEstateCrm.Entities;
using WebApp.Models;
using WebApp.ViewModels;

namespace RealEstateCrm.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class HousingController : BaseController
    {

        public HousingController(ApplicationDbContext context) : base(context, null)
        {
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
        //    var f = JsonConvert.DeserializeObject<HousingIndexFilterModel>(filter);

            var allCities = _context.Cities.Include(x => x.Districts).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            var allStreets = _context.Streets.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            var typesHousings = _context.TypesHousing.ToList();
            

            var query = _context.Housing
                                .AddIsArchiveFilter(isArchive)
                                .AddCityFilter(cityId)
                                .AddDistrictFilter(districtId)
                                .AddHousingTypeFilter(houseType)
                                .AddCostFilter(minCost, maxCost);

            int totalPages;
            int totalItems;
            var items = query.GetPage(page, out totalItems, out totalPages).Select(x => HousingEditModel.Create(x, typesHousings, allCities, allStreets));

            ViewBag.TotalItems = _context.Housing.Count();
            ViewBag.FilteredItemsCount = totalItems;

            var model = new HousingIndexModel()
            {
                Items = items,
                Filters = new HousingIndexFilterModel(_context, houseType, cityId, districtId),
                TotalPages = totalPages,
                CurrentPage = page
            };
            return View(model);
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

            var json = JsonConvert.SerializeObject(filters);
            return RedirectToAction("Index", new
            {
                page,
                //filter = json,
                houseType = intOrNull(filters.HousingTypeList.Id),
                cityId = intOrNull(filters.City?.Id),
                districtId = intOrNull(filters.District?.Id),
                minCost = intOrNull(filters.MinCost),
                maxCost = intOrNull(filters.MaxCost),
                objectId = intOrNull(filters.SelectedObjectId),
                isArchive = filters.IsArchived
            });
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Housing housing = _context.Housing.Single(m => m.Id == id);
            if (housing == null)
            {
                return HttpNotFound();
            }

            return View(housing);
        }
        
        public IActionResult Create()
        {
            var housing = new Housing()
            {
                City = _context.Cities.FirstOrDefault(),
                Phones = new List<HousingPhone>()
            };
            var model = HousingEditModel.Create(_context, housing);
            return View("Save", model);
        }

        [HttpPost]
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

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            
            Housing housing = _context.Housing.GetById(id.Value);
            if (housing == null)
            {
                return HttpNotFound();
            }   
            
            var model = HousingEditModel.Create(_context, housing);
            return View("Save", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HousingEditModel housing, int editId)
        {
            if (ModelState.IsValid)
            {
                var dbItem = _context.Housing.GetById(editId);
                housing.UpdateEntity(dbItem);
                _context.Update(dbItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Save", housing);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Housing housing = _context.Housing.Single(m => m.Id == id);
            _context.Housing.Remove(housing);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public IActionResult ToArchive(int id)
        {
            Housing housing = _context.Housing.Single(m => m.Id == id);
            housing.IsArchive = true;
            _context.Update(housing);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult FromArchive(int id)
        {
            Housing housing = _context.Housing.Single(m => m.Id == id);
            housing.IsArchive = false;
            _context.Update(housing);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Command;
using Data.Query;
using Data.Query.CitiesQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web;
using Web.Helpers;
using WebApp.Data;
using WebApp.ViewModels;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(AuthPolicy.Employees)]
    public class HousingController: BaseController
    {
        private IAuthorizationService AuthService { get; set; }
        private ReadOnlyDataContext ReadOnlyDataContext { get; }
        private IQueryDispatcher QueryDispatcher { get; }
        private ICommandDispatcher CommandDispatcher { get; }
        
        private BuilderFactory BuilderFactory { get; }

        public HousingController(
            ApplicationDbContext context, 
            IAuthorizationService auth, 
            ReadOnlyDataContext readOnlyDataContext, 
            IQueryDispatcher queryDispatcher, 
            HousingEditModelBuilder housingEditModelBuilder, ICommandDispatcher commandDispatcher, BuilderFactory builderFactory) : base(context)
        {
            AuthService = auth;
            ReadOnlyDataContext = readOnlyDataContext;
            QueryDispatcher = queryDispatcher;
            CommandDispatcher = commandDispatcher;
            BuilderFactory = builderFactory;
        }

        public async Task<IActionResult> Index(string filter, 
            int page = 1, 
            int? houseType = null, 
            int? cityId = null, 
            int? districtId = null, 
            int? minCost = null, 
            int? maxCost = null, 
            int? objectId = null, 
            bool? isArchive = null)
        {
            if (User.IsInRole(RoleNames.Employee))
            {
                cityId = CurrentUser?.City?.Id;
            }

            var filterData = new HousingPagedQuery
            {
                PageSize = 20,
                PageNumber = page,
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
            
            
            var query = await QueryDispatcher.ExecuteAsync<HousingPagedQuery, PagedResults<Housing>>(filterData);
            
            var items = query.Items.Select(x =>
            {
                return BuilderFactory.Create<HousingEditModelBuilder>().Build(x);
            }).ToList();

            ViewBag.TotalItems = await QueryDispatcher.ExecuteAsync<HousingCountQuery, int>(new HousingCountQuery());
            ViewBag.FilteredItemsCount = query.PageInfo.TotalItems;

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
                PageInfo = query.PageInfo
            };
            
            return View(model);
        }


        public Task<IActionResult> Cards(string filter,
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Housing housing = await QueryDispatcher.ExecuteAsync<HousingFullByIdQuery, Housing>(new HousingFullByIdQuery(id.Value));
            
            if (housing == null)
            {
                return NotFound();
            }
            var model = await BuilderFactory.Create<HousingEditModelBuilder>().BuildAsync(id.Value);
            return View(model);
        }
        
        [Authorize(AuthPolicy.CreateHousing)]
        public async Task<IActionResult> Create()
        {
            var city = User.IsInRole(RoleNames.Employee)
                ? CurrentUser?.City
                : (await QueryDispatcher.ExecuteAsync<CitiesAllQuery, List<City>>(new CitiesAllQuery())).FirstOrDefault();
                
            var housing = new Housing()
            {
                City = city,
                CityId = city?.Id ?? 0,
                Phones = new List<HousingPhone>(),
                Calls = new List<HousingCall>()
            };
            var model = BuilderFactory.Create<HousingEditModelBuilder>().Build(housing);
            return View("Save", model);
        }

        [HttpPost]
        [Authorize(AuthPolicy.CreateHousing)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HousingEditModel housing)
        {
            if (ModelState.IsValid)
            {
                var newHousingItem = new Housing()
                {
                    Phones = new List<HousingPhone>()
                };
                housing.UpdateEntity(newHousingItem);

                await CommandDispatcher.ExecuteAsync(new AddHousingCommand(newHousingItem));
                SuccessMessage("Запись добавлена");
                
                return RedirectToAction("Index");
            }
            return View("Save", housing);
        }

        [Authorize(AuthPolicy.EditHousing)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Housing housing = await QueryDispatcher.ExecuteAsync<HousingFullByIdQuery, Housing>(new HousingFullByIdQuery(id.Value));
                
            if (housing == null)
            {
                return NotFound();
            }

            var model = await BuilderFactory.Create<HousingEditModelBuilder>().BuildAsync(id.Value);
            return View("Save", model);
        }

        [HttpPost]
        [Authorize(AuthPolicy.EditHousing)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HousingEditModel housing, int editId)
        {
            if (ModelState.IsValid)
            {
                var dbItem = await QueryDispatcher.ExecuteAsync<HousingFullByIdQuery, Housing>(new HousingFullByIdQuery(editId));

                housing.UpdateEntity(dbItem);

                await CommandDispatcher.ExecuteAsync(new UpdateHousingCommand(dbItem));

                TempData["CrmSuccessMessage"] = "Запись была успешно сохранена";
                return RedirectToAction("Index");
            }

            var errors = ModelState.Values.Where(x => x.Errors.Any()).ToList();
            return View("Save", housing);
        }

        [Authorize(AuthPolicy.DeleteHousing)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await QueryDispatcher.ExecuteAsync<HousingExistQuery, bool>(new HousingExistQuery(id)))
            {
                await CommandDispatcher.ExecuteAsync(new DeleteHousingCommand(id));
            }
            return RedirectToAction("Index");
        }

        [Authorize(AuthPolicy.EditHousing)]
        public async Task<IActionResult> ToArchive(int id)
        {
            Housing housing = await QueryDispatcher.ExecuteAsync<HousingFullByIdQuery, Housing>(new HousingFullByIdQuery(id));
            housing.IsArchive = true;
            await CommandDispatcher.ExecuteAsync(new UpdateHousingCommand(housing));
            SuccessMessage("Запись отправлена в архив");
            return RedirectToAction("Index");
        }

        [Authorize(AuthPolicy.EditHousing)]
        public async Task<IActionResult> FromArchive(int id)
        {
            Housing housing = await QueryDispatcher.ExecuteAsync<HousingFullByIdQuery, Housing>(new HousingFullByIdQuery(id));
            housing.IsArchive = false;

            await CommandDispatcher.ExecuteAsync(new UpdateHousingCommand(housing));
            SuccessMessage("Запись больше не в архиве");
            return RedirectToAction("Index");
        }


        [Authorize(AuthPolicy.EditHousing)]
        public async Task<JsonResult> AddCall(int housingId, string status)
        {
            bool housing = await QueryDispatcher.ExecuteAsync<HousingExistQuery, bool>(new HousingExistQuery(housingId));
            if (!housing)
            {
                return Json(new { status = "failed", message = "Housing not found"});
            }

            HousingCallType ctype;
            Enum.TryParse(status, out ctype);

            var housingCall = new HousingCall
            {
                User = CurrentUser,
                Status = status,
                StatusType = ctype,
                HousingId = housingId
            };
            
            await CommandDispatcher.ExecuteAsync(new AddHousingCallCommand(housingCall, housingId));

            return Json(new { status = "ok" });
        }

        public async Task<IActionResult> DetailsDialog(int id)
        {
            var editModel = await BuilderFactory.Create<HousingEditModelBuilder>().BuildAsync(id);
            return PartialView("DetailsDialog", editModel);
        }
    }
}

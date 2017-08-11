using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Entities;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;
using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    [Authorize(AuthPolicy.Employees)]
    public class CustomersController : BaseController
    {
        public CustomersController(ApplicationDbContext context) : base(context)
        {
        }
                
        public IActionResult Index(string filter, int page = 1)
        {

            var filtedObj = new CustomerIndexFilterModel();
            if (!string.IsNullOrEmpty(filter))
            {
                filtedObj = JsonConvert.DeserializeObject<CustomerIndexFilterModel>(filter);
            }

            if (User.IsInRole(RoleNames.Employee))
            {
                filtedObj.CityId = CurrentUser?.City?.Id;
            }

            var typesHousings = _context.TypesHousing.ToList();

            var filterData = new CustomersExtension.FilterParams
            {
                CityId = filtedObj.CityId,
                PriceTo = filtedObj.MinCost,
                PriceFrom = filtedObj.MaxCost,
                Page = page,
                //IsArchived = filtedObj.IsArchive,
                IsSiteAccessOnly = filtedObj.IsSiteAccessOnly
            };

            if (filtedObj.DistrictId.HasValue)
            {
                filterData.DistrictIds = new int[] { filtedObj.DistrictId.Value };
            }

            if (filtedObj.HousingTypeId.HasValue)
            {
                filterData.HouseTypeIds = new int[] { filtedObj.HousingTypeId.Value };
            }

            var applicationDbContext = _context.Clients
                .Include(c => c.City)
                .Include(c => c.CustomerAccount)
                .Include(c => c.Smses)
                .Include(c => c.User)
                .Include(c => c.TypesHousingToCustomers)
                .Include(x=> x.DistrictToClients)
                .Include(x => x.Phones);

            var query = applicationDbContext.Where(x => CustomersExtension.Filter(filterData)(x));

            int totalPages;
            int totalRows;
            var dbItems = query.PagedResult(page, 20, x => x.User, false, out totalRows, out totalPages).ToList();
            
            ViewBag.TotalItems = _context.Clients.Count();
            ViewBag.FilteredItemsCount = totalRows;

            var model = new CustomerIndexModel
            {
                Items = dbItems.Select(x => CustomerEditModel.Create(x)).ToList(),
                Filters = filtedObj,
                TotalPages = totalPages,
                CurrentPage = page
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Filter(CustomerIndexModel model, int page = 1)
        {
            var filters = model.Filters;
            if (filters == null)
            {
                return RedirectToAction("Index", new { page });
            }
            
            return RedirectToAction("Index", new
            {
                page,
                filter = JsonConvert.SerializeObject(filters, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                })
            });
        }


        public IActionResult ResetFilter(int? page = 1)
        {
            return RedirectToAction("Index", new { page });
        }

        // GET: Customer/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = _context.Clients.Single(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [Authorize(AuthPolicy.CreateCustomer)]
        public IActionResult Create()
        {
            var model = new CustomerEditModel();
            return View("Save", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(AuthPolicy.CreateCustomer)]
        public IActionResult Create(CustomerEditModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer();
                model.UpdateEntity(customer);
                _context.Clients.Add(customer);
                _context.SaveChanges();

                model.UpdateDistricts(customer);
                model.UpdateHousingTypes(customer);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
           
            return View("Save",model);
        }

        [Authorize(AuthPolicy.EditCustomer)]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = _context.Clients
                .Include(x => x.DistrictToClients)
                .Include(x => x.TypesHousingToCustomers)
                .Include(x => x.Phones)
                .Single(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            var model = CustomerEditModel.Create(customer);
            return View("Save", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(AuthPolicy.EditCustomer)]
        public IActionResult Edit(CustomerEditModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Clients
                    .Include(x => x.DistrictToClients)
                    .Include(x => x.TypesHousingToCustomers)
                    .Include(x => x.Phones)
                    .Single(x => x.Id == model.EditId);

                model.UpdateEntity(customer);
                model.UpdateDistricts(customer);
                model.UpdateHousingTypes(customer);
                _context.Update(customer);
                
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Save", model);
        }


        [Authorize(AuthPolicy.DeleteCustomer)]
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = _context.Clients.Single(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
                
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(AuthPolicy.DeleteCustomer)]
        public IActionResult DeleteConfirmed(int id)
        {
            Customer customer = _context.Clients.Single(m => m.Id == id);
            _context.Clients.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.Entities;
using WebApp.Models;
using Microsoft.AspNet.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(AuthPolicy.Employees)]
    public class StreetsController : BaseController
    {
        public StreetsController(ApplicationDbContext context) : base(context, null)
        {
             
        }

        // GET: Streets
        public IActionResult Index(int page = 1, string name = "", int cityId = 0)
        {
            int totalRows;
            int totalPages;
            IQueryable<Street> query = _context.Streets.Include(s => s.City).OrderBy(x => x.Name);

            if (User.IsInRole(RoleNames.Employee))
            {
                cityId = CurrentUser?.City?.Id ?? 0;
            }

            if (cityId > 0)
            {
                query = query.Where(x => x.CityId == cityId);
            }


            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }


            var dbItems = query.PagedResult(page, 20, x => x.Name, false, out totalRows, out totalPages);

            var cityList = _context.Cities.ToSelectList(true);
            var items = dbItems.ToList().Select(x => StreetViewModel.Create(x, cityList));

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;


            var model = new SteetMainModel
            {
                Items = items,
                Filter = new StreetFilterModel
                {
                    Name = name,
                    City = new DropDownViewModel(cityId, cityList)
                    {
                        Disabled = User.IsInRole(RoleNames.Employee)
                    }
                }
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Filter(StreetFilterModel filter, int page = 1)
        {
            return RedirectToAction("Index", new
            {
                page = page,
                name = filter.Name,
                cityId = filter.City.Id
            });
        }
        // GET: Streets/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Street street = _context.Streets.Single(m => m.Id == id);
            if (street == null)
            {
                return HttpNotFound();
            }

            return View(street);
        }

        // GET: Streets/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "City");
            return View();
        }

        // POST: Streets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Street street)
        {
            if (ModelState.IsValid)
            {
                _context.Streets.Add(street);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "City", street.CityId);
            return View(street);
        }

        // GET: Streets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Street street = _context.Streets.Single(m => m.Id == id);
            if (street == null)
            {
                return HttpNotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "City", street.CityId);
            return View(street);
        }

        // POST: Streets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Street street)
        {
            if (ModelState.IsValid)
            {
                _context.Update(street);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "City", street.CityId);
            return View(street);
        }

        // GET: Streets/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Street street = _context.Streets.Single(m => m.Id == id);
            if (street == null)
            {
                return HttpNotFound();
            }

            return View(street);
        }

        // POST: Streets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Street street = _context.Streets.Single(m => m.Id == id);
            _context.Streets.Remove(street);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

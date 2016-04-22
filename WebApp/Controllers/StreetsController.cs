using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.Entities;
using WebApp.Models;
using Microsoft.AspNet.Authorization;
using WebApp.Models.SemanticUI;
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
        public IActionResult Index(int page = 1, string name = "", int cityId = 0, char? letter = null)
        {
            int totalRows;
            int totalPages;
            IQueryable<Street> query = _context.Streets.Include(s => s.City).OrderBy(x => x.Name);

            var isEmployee = User.IsInRole(RoleNames.Employee);
            if (isEmployee)
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

            if (letter.HasValue)
            {
                query = query.Where(x => x.Name.Length > 1 && Char.ToUpper(x.Name[0]) == Char.ToUpper(letter.Value));
            }


            var dbItems = query.PagedResult(page, 20, x => x.Name, false, out totalRows, out totalPages);
            
            var items = dbItems.ToList().Select(x => StreetItemViewModel.Create(x));

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Letter = letter;


            var model = new SteetMainModel
            {
                Items = items.ToList(),
                Filter = new StreetFilterModel
                {
                    Name = name,
                    CityId = cityId
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
                cityId = filter.CityId
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
            var model = new StreetItemViewModel()
            {
                 Name = "",
                 CityId = 0
            };
            return View("Save", model);
        }

        // POST: Streets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StreetItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingStreet = _context.Streets.FirstOrDefault(x => x.CityId == model.CityId && x.Name.ToLower() == model.Name.ToLower());
                if (existingStreet != null)
                {
                    ErrorMessage("Улица с таким именем уже существует!");
                    model.CityId = model.CityId;
                    return View("Save", model);
                }

                var street = new Street
                {
                    Name = model.Name,
                    CityId = model.CityId
                };
                _context.Streets.Add(street);
                _context.SaveChanges();

                var editUrl = Url.Action("Edit", new { id = street.Id });
                SuccessMessage($"<a href=\"{editUrl}\">Запись</a> была создана");
                return RedirectToAction("Index");
            }
            return View("Save", model);
        }

        // GET: Streets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Street street = _context.Streets.Include(x => x.City).Single(m => m.Id == id);
            if (street == null)
            {
                return HttpNotFound();
            }
            
            var model = StreetItemViewModel.Create(street);
            return View("Save", model);
        }

        // POST: Streets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StreetItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                Street street = _context.Streets.Include(x => x.City).Single(m => m.Id == model.Id);
                street.Name = model.Name;
                street.CityId = model.CityId;
                _context.Update(street);
                _context.SaveChanges();

                var editUrl = Url.Action("Edit", new { id = model.Id });
                SuccessMessage($"<a href=\"{editUrl}\">Запись</a> была успешно изменена");

                return RedirectToAction("Index");
            }
            
            return View(model);
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

            _context.Streets.Remove(street);
            _context.SaveChanges();

            SuccessMessage("Запись была удалена");

            return RedirectToAction("Index");
        }
        
        public JsonResult Search(string q)
        {
            var query = _context.Streets.Include(x => x.City).Where(x => x.Name.Contains(q)).ToList();

            return new JsonResult(new
            {
                success = true,
                results = query.Select(x => new SearchResultItem { title= x.Name, description = x.City.Name })
            });
        }
    }
}

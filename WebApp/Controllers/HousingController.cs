using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.Entities;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class HousingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HousingController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: EditHousings
        public IActionResult Index()
        {
            var applicationDbContext = _context.Housing
                .Include(h => h.City)
                .Include(h => h.District)
                .Include(h => h.Street)
                .Include(h => h.User).ToList();

            return View(applicationDbContext.Select(x => HousingEditModel.Create(_context,x)).ToList());
        }

        // GET: EditHousings/Details/5
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

        // GET: EditHousings/Create
        public IActionResult Create()
        {
            var housing = new Housing()
            {
                City = _context.Cities.FirstOrDefault()
            };
            var model = HousingEditModel.Create(_context, housing);
            return View("Save", model);
        }

        // POST: EditHousings/Create
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

        // GET: EditHousings/Edit/5
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

        // POST: EditHousings/Edit/5
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

        // GET: EditHousings/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
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

        // POST: EditHousings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Housing housing = _context.Housing.Single(m => m.Id == id);
            _context.Housing.Remove(housing);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

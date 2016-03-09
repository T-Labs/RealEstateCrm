using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
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

        public IActionResult Index(int page = 1)
        {
            const int pageSize = 10;
            int start = (page - 1)*10;

            var applicationDbContext = _context.Housing.Skip(start).Take(pageSize)
                .Include(h => h.City)
                .Include(h => h.District)
                .Include(h => h.Street)
                .Include(h => h.User)
                .Include(x => x.Phones).ToList();

            return View(applicationDbContext.Select(x => HousingEditModel.Create(_context,x)).ToList());
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
    }
}

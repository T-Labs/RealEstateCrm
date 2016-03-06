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
    [Authorize]
    public class EditHousingsController : Controller
    {
        private ApplicationDbContext _context;

        public EditHousingsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: EditHousings
        public IActionResult Index()
        {
            var applicationDbContext = _context.Objects.Include(h => h.City).Include(h => h.District).Include(h => h.Street).Include(h => h.User);
            return View(applicationDbContext.ToList());
        }

        // GET: EditHousings/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Housing housing = _context.Objects.Single(m => m.Id == id);
            if (housing == null)
            {
                return HttpNotFound();
            }

            return View(housing);
        }

        // GET: EditHousings/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "City");
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "District");
            ViewData["StreetId"] = new SelectList(_context.Streets, "Id", "Street");
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "User");
            return View();
        }

        // POST: EditHousings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Housing housing)
        {
            if (ModelState.IsValid)
            {
                _context.Objects.Add(housing);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "City", housing.CityId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "District", housing.DistrictId);
            ViewData["StreetId"] = new SelectList(_context.Streets, "Id", "Street", housing.StreetId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "User", housing.ApplicationUserId);
            return View(housing);
        }

        // GET: EditHousings/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Housing housing = _context.Objects.Single(m => m.Id == id);
            if (housing == null)
            {
                return HttpNotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "City", housing.CityId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "District", housing.DistrictId);
            ViewData["StreetId"] = new SelectList(_context.Streets, "Id", "Street", housing.StreetId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "User", housing.ApplicationUserId);
            _context.Cities.Include(x => x.Districts);
            return View(HousingEditModel.Create(housing, _context.Cities.ToList()));
        }

        // POST: EditHousings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HousingEditModel housing)
        {
            if (ModelState.IsValid)
            {
           //     _context.Update(housing);
             //   _context.SaveChanges();
                return RedirectToAction("Index");
            }
         //   ViewData["CityId"] = new SelectList(_context.Cities, "Id", "City", housing.CityId);
         //   ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "District", housing.DistrictId);
         //   ViewData["StreetId"] = new SelectList(_context.Streets, "Id", "Street", housing.StreetId);
         //   ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "User", housing.ApplicationUserId);
            return View(housing);
        }

        // GET: EditHousings/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Housing housing = _context.Objects.Single(m => m.Id == id);
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
            Housing housing = _context.Objects.Single(m => m.Id == id);
            _context.Objects.Remove(housing);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

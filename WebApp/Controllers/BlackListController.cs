using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.Entities;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(AuthPolicy.Employees)]
    public class BlackListController : BaseController
    {
        public BlackListController(ApplicationDbContext context) : base(context, null)
        {
        }

        // GET: BlackLists
        public IActionResult Index()
        {
            var applicationDbContext = _context.BlackLists.Include(b => b.User);
            var model = applicationDbContext.ToList().Select(BlackListViewModel.Create);
            return View(model);
        }

        // GET: BlackLists/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Blacklist blacklist = _context.BlackLists.Single(m => m.Id == id);
            if (blacklist == null)
            {
                return HttpNotFound();
            }

            return View(blacklist);
        }

        // GET: BlackLists/Create
        public IActionResult Create()
        {
            return View(new BlackListViewModel());
        }

        // POST: BlackLists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlackListViewModel model, FormCollection fields)
        {
            if (ModelState.IsValid)
            {
                var item = new Blacklist
                {
                    Description = model.Description,
                    PhoneNumber = model.PhoneNumber,
                    User = CurrentUser
                };
                _context.BlackLists.Add(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: BlackLists/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Blacklist blacklist = _context.BlackLists.Single(m => m.Id == id);
            if (blacklist == null)
            {
                return HttpNotFound();
            }
           
            return View(BlackListViewModel.Create(blacklist));
        }

        // POST: BlackLists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlackListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = new Blacklist
                {
                    Id = model.Id,
                    Description = model.Description,
                    PhoneNumber = model.PhoneNumber,
                    User = CurrentUser
                };
                _context.Update(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: BlackLists/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Blacklist blacklist = _context.BlackLists.Single(m => m.Id == id);
            if (blacklist == null)
            {
                return HttpNotFound();
            }

            _context.BlackLists.Remove(blacklist);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}

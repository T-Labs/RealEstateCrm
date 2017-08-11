using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using WebApp.Entities;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(AuthPolicy.Employees)]
    public class BlackListController : BaseController
    {
        public BlackListController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: BlackLists
        public IActionResult Index(int page = 1)
        {
            int totalRows;
            int totalPages;
            var applicationDbContext = _context.BlackLists.Include(b => b.User)
                                                .PagedResult(page, 10, x => x.DateAdd, false, out totalRows, out totalPages);
            var model = applicationDbContext.ToList().Select(BlackListViewModel.Create);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(model);
        }

        // GET: BlackLists/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blacklist blacklist = _context.BlackLists.Single(m => m.Id == id);
            if (blacklist == null)
            {
                return NotFound();
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
        public IActionResult Create(BlackListViewModel model)
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

                var editUrl = Url.Action("Edit", new { id = item.Id });
                SuccessMessage($"<a href=\"{editUrl}\">Запись</a> была создана");

                return RedirectToAction("Index");
            }
            
            return View(model);
        }

        // GET: BlackLists/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blacklist blacklist = _context.BlackLists.Single(m => m.Id == id);
            if (blacklist == null)
            {
                return NotFound();
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

                var editUrl = Url.Action("Edit", new {id = model.Id});
                SuccessMessage($"<a href=\"{editUrl}\">Запись</a> была успешно изменена");
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
                return NotFound();
            }

            Blacklist blacklist = _context.BlackLists.Single(m => m.Id == id);
            if (blacklist == null)
            {
                return NotFound();
            }

            _context.BlackLists.Remove(blacklist);
            _context.SaveChanges();
            SuccessMessage("Запись была удалена");
            return RedirectToAction("Index");
        }
        
    }
}

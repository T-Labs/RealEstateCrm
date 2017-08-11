using System.IO;
using System.Linq;
using System.Security.Claims;
using Data.Query;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

using Microsoft.Extensions.WebEncoders;
using WebApp.ViewModels;
using Newtonsoft.Json;
using Web;

namespace WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        protected ApplicationUser CurrentUser
        {
            //todo: проверить
            get
            {
                if (QueryDispatcher != null)
                {
                    var user = QueryDispatcher
                        .ExecuteAsync<UserByIdQuery, ApplicationUser>(new UserByIdQuery(User.GetUserId()))
                        .GetAwaiter()
                        .GetResult();
                    
                    return user;
                }
                else
                {
                    return _context.Users.Include(x => x.City).Include(x => x.City.Districts).FirstOrDefault(x => x.Id == User.GetUserId());
                }
            }
        }

        private IQueryDispatcher QueryDispatcher { get; }

        protected CustomerUser CustomerUser
        {
            get => CustomerUser.FromSession(HttpContext.Session);
            set => CustomerUser.ToSession(value, HttpContext.Session);
        }
        
        protected BaseController(ApplicationDbContext context, IQueryDispatcher queryDispatcher = null)
        {
            _context = context;
            QueryDispatcher = queryDispatcher;
        }


        protected void SuccessMessage(string message)
        {
            TempData["CrmSuccessMessage"] = message;
        }

        protected void ErrorMessage(string message)
        {
            TempData["CrmErrorMessage"] = message;
        }

    }
}

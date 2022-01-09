using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPProject.Models.Home;

namespace ASPProject.Controllers
{
    public class LoginController : Controller
    {
        private ShopEntities context = new ShopEntities();
       
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(User u)
        {
            var user = from e in context.Users
                       where e.Email == u.Email && e.Password == u.Password
                       select e;
            int p = user.Count();
            if (user.Count() == 1)
            {
                return View("Successful");
            }

            return View("Error");
        }
    }
}

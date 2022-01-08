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
                       where e.Id == u.Id
                       select e;
            if (user.Where(x => x.Id == u.Id && u.Password == x.Password).Count() == 1)
            {

                return View("Successful");
            }

            return View("Error");
        }
    }
}

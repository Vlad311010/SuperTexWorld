using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPProject.Models.Home;

namespace ASPProject.Controllers
{
    public class HomeController : Controller
    {
        private ShopDB db = new ShopDB();

        [HttpGet]
        public ActionResult Index()
        {
            var items = from e in db.Items
                          orderby e.Id
                          select e;
            return View(items);
        }

        [HttpGet]
        public ActionResult Items()
        {
            var items = from e in db.Items
                        orderby e.Id
                        select e;
            return View(items);
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return Content("Create");
            //return View();
        }

    }
}
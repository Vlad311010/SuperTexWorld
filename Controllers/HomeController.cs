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
        public ActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddItem(ItemModel item)
        {
            try
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Items");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ItemDetails(int id)
        {
            ItemModel item = db.Items.Single(m => m.Id == id);
            return View(item);
        }

        [HttpGet]
        public ActionResult DeleteItem(int id)
        {
            ItemModel item = db.Items.Single(m => m.Id == id);
            return View(item);
        }

        [HttpPost]
        public ActionResult DeleteItem(int id, FormCollection collection)
        {
            ItemModel item = db.Items.Single(m => m.Id == id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Items");
        }

        [HttpGet]
        public ActionResult EditItem(int id)
        {
            ItemModel item = db.Items.Single(m => m.Id == id);
            return View(item);
        }

        [HttpPost]
        public ActionResult EditItem(int id, FormCollection collection)
        {
            try
            {
                ItemModel item = db.Items.Single(m => m.Id == id);
                if (TryUpdateModel(item))
                {
                    db.SaveChanges();
                    return RedirectToAction("Items");
                }
                return View(item);
            }
            catch
            {
                return View();
            }
        }
    }
}
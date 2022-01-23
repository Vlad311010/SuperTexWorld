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
        private ShopEntities context = new ShopEntities();

        [HttpGet]
        public ActionResult Index()
        {
            Item elementItems = new Item();
            
            //context.Orders.AddObject(order);
            var items = from e in context.Items
                          orderby e.Id
                          select e;
            foreach (var elem in items) 
            {
                Console.WriteLine(elem.ItemName + " " + elem.Description + " " + elem.Id);
            }
            //return View(items);

            return Content(elementItems.Id.ToString());
        }

        [HttpGet]
        public ActionResult Items()
        {
            /*TODO remove*/
            Item elementItems = new Item();
            elementItems.ItemName = "Acer 150L";
            elementItems.Description = "Laptop 1";
            elementItems.ImagePath =  "none";
            elementItems.Price =  1900.0;

            context.Items.Add(elementItems);

            var items = from e in context.Items
                        orderby e.Id
                        select e;
            return View(items);

            //return Content("Items");

            //return Content(elementItems.Id.ToString());
        }


        [HttpGet]
        public ActionResult AddItem()
        {
            //return View();
            return Content("Details");
        }

    }
}
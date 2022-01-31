using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPProject.Models.Home;

namespace ASPProject.Controllers
{
    public class ItemsController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Items
        public async System.Threading.Tasks.Task<ActionResult> Index(string searchString="")
        //public ActionResult Index(string searchString = "")
        {
            Item elementItems = new Item();
            elementItems.ItemName = "HP 150L";
            elementItems.Description = "Laptop 1";
            elementItems.ImagePath = "\\Content\\DataImage\\HP-laptop1.png";
            elementItems.Price = 1900.0;

            db.Items.Add(elementItems);

            var items = from e in db.Items
                        select e;


            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.ItemName.Contains(searchString) || s.Description.Contains(searchString));
            }
            ViewBag.SearchString = searchString;

            return View(await items.ToListAsync());
            //return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemName,Description,ImagePath,Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (item.ImagePath == null)
                    item.ImagePath = "\\Content\\DataImage\\no-image.png";
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemName,Description,ImagePath,Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ToCart(int itemId, string userEmail) 
        {
            User user = (from e in db.Users
                          where e.Email == userEmail
                          select e).FirstOrDefault();

            if (user == null)
                return RedirectToAction("Login", "Login");

            Item item = db.Items.Find(itemId);
            State state = db.States.Find(1); //InOrder - stateId = 1
            Order order = new Order();

            //order.Id = db.Items.Count();
            order.PutchaseDate = DateTime.Now;
            order.UserId = user.Id;
            order.StateId = 1;
            order.ItemId = itemId;

            order.User = user;
            order.State = state;
            order.Item = item;

            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index", "Items");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

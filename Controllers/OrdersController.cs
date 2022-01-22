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
    public class OrdersController : Controller
    {
        private ShopEntities db = new ShopEntities();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.User).Include(o => o.State).Include(o => o.Item);
            return View(orders.ToList());
        }

        // GET: Orders
        public ActionResult Cart(string userEmail)
        {
            User user = (from e in db.Users
                         where e.Email == userEmail
                         select e).FirstOrDefault();

            if (user.Role == "Admin")
                return RedirectToAction("AdminCartSelection");

            var cartItems = (from e in db.Orders
                             where e.UserId == user.Id
                               && e.State.Id == 1 //id of "InOrder" state
                             select e);

            return View(cartItems.ToList());
        }
        
        [Authorize(Roles = "User")]
        public ActionResult Purchase(string userEmail)
        {
            User user = (from e in db.Users
                         where e.Email == userEmail
                         select e).FirstOrDefault();

            var cartItems = (from e in db.Orders
                             where e.UserId == user.Id
                               && e.State.Id == 1 //id of "InOrder" state
                             select e);

            foreach (var cartItem in cartItems)
            {
                db.Orders.Remove(cartItem);
                State state = db.States.Find(2); //InOrder - stateId = 1
                Order order = new Order();

                //order.Id = db.Items.Count();
                order.Item = cartItem.Item;
                order.ItemId = cartItem.ItemId;
                order.UserId = cartItem.UserId;
                order.PutchaseDate = DateTime.Now;
                order.StateId = 2;

                order.User = user;
                order.State = state;
                db.Orders.Add(order);
            }
            db.SaveChanges();

            return RedirectToAction("Index", "Items");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminCartSelection()
        {
            ShopEntities db = new ShopEntities();
            return View(db.Users.ToList());
        }


        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            ViewBag.StateId = new SelectList(db.States, "Id", "Description");
            ViewBag.ItemId = new SelectList(db.Items, "Id", "ItemName");
            return View();
        }


        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "Id,PutchaseDate,UserId,StateId,ItemId")]*/ Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Date = DateTime.Now;
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", order.UserId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Description", order.StateId);
            ViewBag.ItemId = new SelectList(db.Items, "Id", "ItemName", order.ItemId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", order.UserId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Description", order.StateId);
            ViewBag.ItemId = new SelectList(db.Items, "Id", "ItemName", order.ItemId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PutchaseDate,UserId,StateId,ItemId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", order.UserId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Description", order.StateId);
            ViewBag.ItemId = new SelectList(db.Items, "Id", "ItemName", order.ItemId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Cart", "Orders", new { userEmail = this.User.Identity.Name });
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Drva.Models.Entities;
using Drva.Models.View;

namespace Drva.Controllers
{
    public class OrdersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Orders
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.LastNameSortParam = sortOrder == "date_asc" ? "date_desc" : "date_asc";

            var orders = db.Orders.Include(x => x.Units);

            switch (sortOrder)
            {
                case "date_desc":
                    orders = orders.OrderByDescending(x => x.DeliveryDate);
                    break;
                case "date_asc":
                    orders = orders.OrderBy(x => x.DeliveryDate);
                    break;
                default:
                    orders = orders.OrderBy(x => x.DeliveryDate);
                    break;
            }

            var orderDataModel = new List<OrderIndexViewModel>();
            foreach (var order in orders.ToList())
            {
                var newOrder = new OrderIndexViewModel {
                    DeliveryDate = order.DeliveryDate ?? DateTime.Now,
                    OrderID = order.OrderID,
                    Customer = String.Format("{0} {1}", order.Customer.FirstName, order.Customer.LastName),
                    Units = new List<String>()
                };
                foreach (var unit in order.Units.ToList())
                {
                    newOrder.Units.Add(String.Format("{0}m {1} na {2} cijene {3} eura", unit.Amount, unit.WoodType, unit.SawingType, unit.Price));
                }
                orderDataModel.Add(newOrder);              
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(orderDataModel.ToPagedList(pageNumber, pageSize));
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
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,DeliveryDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,DeliveryDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            return RedirectToAction("Index");
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

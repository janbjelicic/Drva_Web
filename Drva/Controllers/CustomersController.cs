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
    public class CustomersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Customers
        public ActionResult Index(string sortOrder, string currentFilter, string query, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.LastNameSortParam = sortOrder == "last_name_asc" ? "last_name_desc" : "last_name_asc";
            ViewBag.DistrictSortParam = sortOrder == "district_asc" ? "district_desc" : "district_asc";
            ViewBag.PlaceSortParam = sortOrder == "place_asc" ? "place_desc" : "place_asc";
            ViewBag.PostNumberSortParam = sortOrder == "post_number_asc" ? "post_number_desc" : "post_number_asc";

            if (query != null)
            {
                page = 1;
            }
            else
            {
                query = currentFilter;
            }

            ViewBag.CurrentFilter = query;

            var customers = db.Customers.Include(c => c.Address).Take(100);

            if (!String.IsNullOrEmpty(query))
            {
                query = query.Trim();
                customers = customers.Where(s => s.LastName.Contains(query));
            }

            switch (sortOrder)
            {
                case "last_name_desc":
                    customers = customers.OrderByDescending(x => x.LastName);
                    break;
                case "last_name_asc":
                    customers = customers.OrderBy(x => x.LastName);
                    break;
                case "district_desc":
                    customers = customers.OrderByDescending(x => x.Address.District);
                    break;
                case "district_asc":
                    customers = customers.OrderBy(x => x.Address.District);
                    break;
                case "place_desc":
                    customers = customers.OrderByDescending(x => x.Address.Place);
                    break;
                case "place_asc":
                    customers = customers.OrderBy(x => x.Address.Place);
                    break;
                case "post_number_desc":
                    customers = customers.OrderByDescending(x => x.Address.PostNumber);
                    break;
                case "post_number_asc":
                    customers = customers.OrderBy(x => x.Address.PostNumber);
                    break;
                default:
                    customers = customers.OrderBy(x => x.LastName);
                    break;
            }

            var customerDataModel = new List<CustomerIndexViewModel>();
            foreach (var customer in customers.ToList())
            {
                customerDataModel.Add(new CustomerIndexViewModel
                {
                    CustomerID = customer.CustomerID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    PhoneNumbers = String.Join(",", customer.PhoneNumbers.Select(x => x.Number))
                });
            }
            return View(customerDataModel.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Include(x => x.Address).Include(x => x.PhoneNumbers)
                .Include(x => x.Orders).FirstOrDefault(x => x.CustomerID == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerDetailViewModel model = new CustomerDetailViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                CustomerID = customer.CustomerID,
                Orders = customer.Orders.ToList()
            };

            model.PhoneNumbers = String.Join(",", customer.PhoneNumbers.Select(x => x.Number));
            return View(model);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCreateViewModel customer)
        {
            if (ModelState.IsValid)
            {
                Customer newCustomer = new Customer
                {
                    Address = customer.Address,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumbers = new List<PhoneNumber>()
                };
                List<string> phoneNumbers = customer.PhoneNumbers.Split(',').ToList();
                foreach (var number in phoneNumbers)
                {
                    newCustomer.PhoneNumbers.Add(new PhoneNumber
                    {
                        Number = number
                    });
                }

                db.Customers.Add(newCustomer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Include(x => x.Address).Include(x => x.PhoneNumbers).FirstOrDefault(x => x.CustomerID == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerCreateViewModel oldCustomer = new CustomerCreateViewModel
            {
                CustomerID = customer.CustomerID,
                Address = customer.Address,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumbers = String.Join(",", customer.PhoneNumbers.Select(x => x.Number))
            };

            return View(oldCustomer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerCreateViewModel customer)
        {
            if (ModelState.IsValid)
            {
                Customer oldCustomer = db.Customers.Include(x => x.Address).Include(x => x.PhoneNumbers)
                                        .Include(x => x.Orders).FirstOrDefault(x => x.CustomerID == customer.CustomerID);
                oldCustomer.FirstName = customer.FirstName;
                oldCustomer.LastName = customer.LastName;
                oldCustomer.Address = customer.Address;
                oldCustomer.PhoneNumbers = new List<PhoneNumber>();

                List<string> phoneNumbers = customer.PhoneNumbers.Split(',').ToList();
                foreach (var number in phoneNumbers)
                {
                    oldCustomer.PhoneNumbers.Add(new PhoneNumber
                    {
                        Number = number
                    });
                }

                db.Entry(oldCustomer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, string currentFilter, string sortOrder)
        {
            Customer customer = db.Customers.Include(x => x.Address).Include(x => x.PhoneNumbers).SingleOrDefault(x => x.CustomerID == id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index", new { currentFilter = currentFilter, sortOrder = sortOrder });
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using service_station.Models;

namespace service_station.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        [HttpGet]
        public ActionResult Find()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Find(FindCustomerViewModel Customer)
        {
            if (ModelState.IsValid)
            {
                var result =
                        db.Customers.FirstOrDefault(
                            p => p.LastName == Customer.LastName && p.FirstName == Customer.FirstName && p.Phone == Customer.Phone);


                if (result != null)
                {
                    return RedirectToAction("Personal", new RouteValueDictionary(
                        new { Id = result.Id }));
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Personal(int? id)
        {
            var customer = db.Customers.FirstOrDefault(p => p.Id == id);
            return View(customer);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Personal", new RouteValueDictionary(
                        new { Id = customer.Id }));
            }
            return View(customer);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Personal", new RouteValueDictionary(
                       new { Id = customer.Id }));
            }
            return View(customer);
        }
    }
}
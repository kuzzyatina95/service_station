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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Orders
        public ActionResult Index(int? id)
        {
            var customer = db.Customers.Find(id);

            var orders = new CarCustomerOrders()
            {
                CustomerId = customer.Id,
                Orders = db.Orders.Include(c => c.Car).Where(p => p.Car.CustomerId == id).
                OrderByDescending(p => p.Id)
            };
            return View(orders);
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {

            ViewBag.CarCustomerId = new SelectList(db.Cars.Where(p=>p.CustomerId == id), "Id", "Model");
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = new Order()
            {
                Car = new Car()
                {
                    CustomerId = customer.Id
                }
            };


            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            var car = db.Cars.Find(order.CarCustomerId);
            ViewBag.CarCustomerId = new SelectList(db.Cars, "Id", "Model");

            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index", new RouteValueDictionary(
                        new { Id = order.CarCustomerId }));
            }
            order.Car.CustomerId = car.CustomerId;
            return View(order);
        }

        [HttpGet]
        public ActionResult Add(int? id)
        {
            var car = db.Cars.Find(id);
            if (car == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = new Order()
            {
                Car = car,
                CarCustomerId = car.Id
            };

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Order order)
        {
            var car = db.Cars.Find(order.CarCustomerId);
           
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Personal", "Customer", new RouteValueDictionary(
                        new { Id = car.CustomerId }));
            }
            order.Car.CustomerId = car.CustomerId;
            return View(order);
        }

        public ActionResult LastOrdersList(int? id)
        {
            var orders = db.Orders.Include(c => c.Car).Where(p => p.Car.CustomerId == id).
                OrderByDescending(p => p.Id).Take(3);

            return PartialView("_LastOrdersListPartial", orders);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = db.Orders.Include(c => c.Car).FirstOrDefault(p => p.Id == id);


            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
    
                return RedirectToAction("Index");
            }
            return View(order);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.Include(p => p.Car).FirstOrDefaultAsync(p => p.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: MobilePhoneAaluxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.Include(p=>p.Car).FirstOrDefaultAsync(p=>p.Id == id);
            int customerId = order.Car.CustomerId;
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Orders", new RouteValueDictionary(
                  new { Id = customerId }));
        }
    }
}
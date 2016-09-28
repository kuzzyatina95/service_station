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
    public class GarageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Garage
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add(int? id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var car = new Car()
            {
                CustomerId = customer.Id
            };

            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Personal","Customer", new RouteValueDictionary(
                        new { Id = car.CustomerId }));
            }
            return View(car);
        }

        public ActionResult GarageList(int? id)
        {
            var Cars = db.Cars.OrderByDescending(p => p.Id).Where(p=>p.CustomerId == id);
            return PartialView("_GarageListPartial", Cars);
        }

        public ActionResult DeleteWithoutOrder(int? id)
        {
            var orders = db.Orders.ToList();

            var car = db.Cars.Find(id);

            var carOrders = new CarOrders()
            {
                Orders = db.Orders.Where(p=>p.CarCustomerId == id).ToList(),
                CarId = car.Id
            };

            return PartialView("_DeleteWithoutOrderPartial", carOrders);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Car car =  db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }

            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Personal", "Customer", new RouteValueDictionary(
                     new { Id = car.CustomerId }));
            }
            return View(car);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: MobilePhoneAaluxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Car car = await db.Cars.FindAsync(id);
            db.Cars.Remove(car);
            await db.SaveChangesAsync();
            return RedirectToAction("Personal", "Customer", new RouteValueDictionary(
                  new { Id = car.CustomerId }));
        }

      
    }
}
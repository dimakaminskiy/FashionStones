using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;

namespace FashionStones.Areas.Admin.Controllers
{
    public class MethodOfDeliveriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/MethodOfDeliveries
        public ActionResult Index()
        {
            return View(db.MethodOfDeliveries.ToList());
        }

        // GET: Admin/MethodOfDeliveries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MethodOfDeliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] MethodOfDelivery methodOfDelivery)
        {
            if (ModelState.IsValid)
            {
                db.MethodOfDeliveries.Add(methodOfDelivery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(methodOfDelivery);
        }

        // GET: Admin/MethodOfDeliveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MethodOfDelivery methodOfDelivery = db.MethodOfDeliveries.Find(id);
            if (methodOfDelivery == null)
            {
                return HttpNotFound();
            }
            return View(methodOfDelivery);
        }

        // POST: Admin/MethodOfDeliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] MethodOfDelivery methodOfDelivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(methodOfDelivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(methodOfDelivery);
        }

        // GET: Admin/MethodOfDeliveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MethodOfDelivery methodOfDelivery = db.MethodOfDeliveries.Find(id);
            if (methodOfDelivery == null)
            {
                return HttpNotFound();
            }
            return View(methodOfDelivery);
        }

        // POST: Admin/MethodOfDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MethodOfDelivery methodOfDelivery = db.MethodOfDeliveries.Find(id);
            db.MethodOfDeliveries.Remove(methodOfDelivery);
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

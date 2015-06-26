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
    public class MethodOfPaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/MethodOfPayments
        public ActionResult Index()
        {
            return View(db.MethodOfPayments.ToList());
        }

        // GET: Admin/MethodOfPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MethodOfPayment methodOfPayment = db.MethodOfPayments.Find(id);
            if (methodOfPayment == null)
            {
                return HttpNotFound();
            }
            return View(methodOfPayment);
        }

        // GET: Admin/MethodOfPayments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MethodOfPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] MethodOfPayment methodOfPayment)
        {
            if (ModelState.IsValid)
            {
                db.MethodOfPayments.Add(methodOfPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(methodOfPayment);
        }

        // GET: Admin/MethodOfPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MethodOfPayment methodOfPayment = db.MethodOfPayments.Find(id);
            if (methodOfPayment == null)
            {
                return HttpNotFound();
            }
            return View(methodOfPayment);
        }

        // POST: Admin/MethodOfPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] MethodOfPayment methodOfPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(methodOfPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(methodOfPayment);
        }

        // GET: Admin/MethodOfPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MethodOfPayment methodOfPayment = db.MethodOfPayments.Find(id);
            if (methodOfPayment == null)
            {
                return HttpNotFound();
            }
            return View(methodOfPayment);
        }

        // POST: Admin/MethodOfPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MethodOfPayment methodOfPayment = db.MethodOfPayments.Find(id);
            db.MethodOfPayments.Remove(methodOfPayment);
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

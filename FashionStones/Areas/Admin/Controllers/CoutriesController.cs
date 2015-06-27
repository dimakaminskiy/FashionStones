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
    public class CoutriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Coutries
        public ActionResult Index()
        {
            return View(db.Coutries.ToList());
        }

        

        // GET: Admin/Coutries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Coutries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Coutry coutry)
        {
            if (ModelState.IsValid)
            {
                db.Coutries.Add(coutry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coutry);
        }

        // GET: Admin/Coutries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coutry coutry = db.Coutries.Find(id);
            if (coutry == null)
            {
                return HttpNotFound();
            }
            return View(coutry);
        }

        // POST: Admin/Coutries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Coutry coutry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coutry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coutry);
        }

        // GET: Admin/Coutries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coutry coutry = db.Coutries.Find(id);
            if (coutry == null)
            {
                return HttpNotFound();
            }
            var error = Request.Params["msg"];
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError("", error);
            }
            return View(coutry);
        }

        // POST: Admin/Coutries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coutry coutry = db.Coutries.Find(id);
            if (db.Users.Any(t => t.CountryId == id))
            {
                return RedirectToAction("Delete", new { msg = "Обнаружены зарегистрированные пользователи данной страны. Удаление невозможно" });
            }
            db.Coutries.Remove(coutry);
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

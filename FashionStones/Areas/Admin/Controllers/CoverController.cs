using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;

namespace FashionStones.Areas.Admin.Controllers
{
    public class CoverController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Cover
        public ActionResult Index()
        {
            return View(db.Covers.ToList().OrderBy(x=>x.Name));
        }

  

        // GET: Admin/Cover/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cover/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Cover cover)
        {
            if (ModelState.IsValid)
            {
                db.Covers.Add(cover);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cover);
        }

        // GET: Admin/Cover/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cover cover = db.Covers.Find(id);
            if (cover == null)
            {
                return HttpNotFound();
            }
            return View(cover);
        }

        // POST: Admin/Cover/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Cover cover)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cover).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cover);
        }

        // GET: Admin/Cover/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cover cover = db.Covers.Find(id);
            if (cover == null)
            {
                return HttpNotFound();
            }
            var error = Request.Params["msg"];
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError("", error);
            }
            return View(cover);
        }

        // POST: Admin/Cover/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cover cover = db.Covers.Find(id);
            if (cover.Products.Any())
            {
                return RedirectToAction("Delete", new { msg = "Обнаружены записи товара с данным покрытием. Удаление невозможно" });
            }
            db.Covers.Remove(cover);
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

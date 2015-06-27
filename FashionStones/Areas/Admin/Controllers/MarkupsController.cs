using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;

namespace FashionStones.Areas.Admin.Controllers
{
    public class MarkupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Markups
        public ActionResult Index()
        {
            return View(db.Markups.ToList().OrderBy(x=>x.Name));
        }

        // GET: Admin/Markups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markup markup = db.Markups.Find(id);
            if (markup == null)
            {
                return HttpNotFound();
            }
            return View(markup);
        }

        // GET: Admin/Markups/Create
        public ActionResult Create()
        {
            return View(new Markup {RetailMarkup = 100,TradeMarkup = 0});
        }

        // POST: Admin/Markups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,RetailMarkup,TradeMarkup")] Markup markup)
        {
            if (ModelState.IsValid)
            {
                db.Markups.Add(markup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(markup);
        }

        // GET: Admin/Markups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markup markup = db.Markups.Find(id);
            if (markup == null)
            {
                return HttpNotFound();
            }
            return View(markup);
        }

        // POST: Admin/Markups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RetailMarkup,TradeMarkup")] Markup markup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(markup);
        }

        // GET: Admin/Markups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markup markup = db.Markups.Find(id);
            if (markup == null)
            {
                return HttpNotFound();
            }
            var error = Request.Params["msg"];
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError("", error);
            }
            return View(markup);
        }

        // POST: Admin/Markups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Markup markup = db.Markups.Find(id);
            if (markup.Products.Any())
            {
                return RedirectToAction("Delete", new { msg = "Обнаружены записи товара с данной наценкой. Удаление невозможно" });
            }
            db.Markups.Remove(markup);
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

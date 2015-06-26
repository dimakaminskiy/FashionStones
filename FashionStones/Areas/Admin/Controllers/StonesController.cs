using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;

namespace FashionStones.Areas.Admin.Controllers
{
    public class StonesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Stones
        public async Task<ActionResult> Index()
        {
            return View((await db.Stones.ToListAsync()).OrderBy(x=>x.Name));
        }

      
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Stones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Stone stone)
        {
            if (ModelState.IsValid)
            {
                db.Stones.Add(stone);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stone);
        }

        // GET: Admin/Stones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stone stone = await db.Stones.FindAsync(id);
            if (stone == null)
            {
                return HttpNotFound();
            }
            return View(stone);
        }

        // POST: Admin/Stones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Stone stone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stone).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stone);
        }

        // GET: Admin/Stones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stone stone = await db.Stones.FindAsync(id);
            if (stone == null)
            {
                return HttpNotFound();
            }
            return View(stone);
        }

        // POST: Admin/Stones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Stone stone = await db.Stones.FindAsync(id);
            db.Stones.Remove(stone);
            await db.SaveChangesAsync();
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

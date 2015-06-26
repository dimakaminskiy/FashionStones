using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;
using FashionStones.Utils;
using Ninject.Infrastructure.Language;

namespace FashionStones.Areas.Admin.Controllers
{
    public class PHotoController : Controller
    {
        private PhotoEditor _photoEditor;

        private  string TempFolder
        {
            get { return "~/Content/images/Temp"; }
        } // временно храним изображения, механизм их удалениея предусмотрен ))

         private  string JewelPHotoFolder
        {
            get { return "~/Content/images/JewelPHoto"; }
        } 

        private string UrlToLocal(string url)
        {
            return HttpContext.Server.MapPath(url);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/PHoto
        public ActionResult Index(int page=1, int itemPerPage=16)
        {
            var listOptions = new SelectItemPerPageList();
            var list = db.JewelPHotos.OrderBy(x => x.Caption).AsQueryable();
            if (listOptions.Options.Any(x => x == itemPerPage) == false)
            {
                itemPerPage = 16;
            }
            var pd = new PageableData<JewelPHoto>(list,page,itemPerPage);
            ViewBag.OptionsListViewItems = listOptions.GetSelectListItems(itemPerPage);
            return View(pd);
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadPreImage(HttpPostedFileWrapper qqfile)
        {
            _photoEditor = new PhotoEditor(UrlToLocal(TempFolder), UrlToLocal(JewelPHotoFolder));
            try
            {
                var fName=_photoEditor.SaveTempFile(qqfile);
                return Json(new { success = true, errorMessage = string.Empty, fileName = "/Content/images/Temp" + "/" + fName });
            }
            catch (Exception e)
            {
                return Json(new {success = true, errorMessage = e.Message});
            }
        }

        [HttpPost]
        public ActionResult Save(string t, string l, string h, string w, string fileName, string caption)
        {
            if ((!string.IsNullOrEmpty(fileName)) && (!string.IsNullOrEmpty(caption)))
            {
                int top = Convert.ToInt32(t.Replace("-", "").Replace("px", ""));
                int left = Convert.ToInt32(l.Replace("-", "").Replace("px", ""));
                int height = Convert.ToInt32(h.Replace("-", "").Replace("px", ""));
                int width = Convert.ToInt32(w.Replace("-", "").Replace("px", ""));
                try
                {
                    _photoEditor = new PhotoEditor(UrlToLocal(TempFolder), UrlToLocal(JewelPHotoFolder));
                    string name =  _photoEditor.SaveJevelPhot(top, left, height, width, UrlToLocal(fileName));

                    var photo = new JewelPHoto {Name = name, Caption = caption};
                    db.JewelPHotos.Add(photo);
                    db.SaveChanges();
                    
                    return Json(new {success = true, errorMessage = string.Empty});
                }
                catch (Exception e)
                {
                    return Json(new {success = false, errorMessage = e.Message});
                }
            }
            return Json(new {success = false, errorMessage = "Произошла ошибка при попытке сохранения изображения."});

        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var photo = db.JewelPHotos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View (photo);
        }

        // GET: Admin/Cover/Edit/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            var photo = db.JewelPHotos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }


        // GET: Admin/Cover/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var photo = db.JewelPHotos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Admin/Cover/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var photo = db.JewelPHotos.Find(id);
            db.JewelPHotos.Remove(photo);
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
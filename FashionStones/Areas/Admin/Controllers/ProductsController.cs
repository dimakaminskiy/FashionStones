using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using FashionStones.Models;
using FashionStones.Models.Domain.Entities;
using FashionStones.Utils;

namespace FashionStones.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public JsonResult GetSelectImage(int id)
        {
            try
            {
                var photo = db.JewelPHotos.Find(id);
                return Json(new { success = true, fileName = photo.PathToSmall }, JsonRequestBehavior.AllowGet); // success
            }
            catch (Exception e)
            {
                return Json(new { success = false, errorMessage = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
       
        public ActionResult Index(
            string sortOrder, 
            string currentFilter,
            string searchByName,
            string searchByCategory,
            int? page)
        {
            if (searchByName != null)
            {
                page = 1;
            }
            else
            {
                searchByName = currentFilter;
            }
            SetCollectionForFilter();
          

            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            
            // filter
            IQueryable<Product> products = db.Products;
            if (!String.IsNullOrEmpty(searchByName))
            {
                products = products.Where(b => b.Id.ToString().ToUpper().Contains(searchByName.ToUpper()));
            }
            if (!string.IsNullOrEmpty(searchByCategory))
            {
                products = products.Where(x => x.Category.Name == searchByCategory);
            }
            // Sort
            IQueryable<Product> productsToReturn = null;
            switch (sortOrder)
            {
                case "NameDesc":
                    productsToReturn = products.OrderByDescending(b => b.Name);
                    break;
               case "Price":
                    productsToReturn = products.OrderBy(b => b.ShoppingPrice);
                    break;
                case "PriceDesc":
                    productsToReturn = products.OrderByDescending(b => b.ShoppingPrice);
                    break;
               case "Category":
                    productsToReturn = products.OrderBy(c => c.Category.Name);
                    break;
                case "CategoryDesc":
                    productsToReturn = products.OrderByDescending(c => c.Category.Name);
                    break;
                default:  // Name ascending 
                    productsToReturn = products.OrderBy(b => b.Name);
                    break;
            }

            string nameSortParm = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            string priceSortParm = sortOrder == "Price" ? "PriceDesc" : "Price";
            string categorySortParm = sortOrder == "Category" ? "CategoryDesc" : "Category";
            string CurrentFilter = searchByName;
            string categoryParm = searchByCategory;
            var pd = new PageableData3<Product>(productsToReturn.ToList().AsQueryable(), pageIndex, 16, sortOrder,
                nameSortParm,priceSortParm,categorySortParm,CurrentFilter,categoryParm);              
            return View(pd);
        }

        private void SetCollectionForFilter()
        {
            var categories = new List<string>();
            var categoriesQuery = from category in db.Categories.ToList()
                                  orderby category.Name
                                  select category.Name;
            categories.AddRange(categoriesQuery.Distinct());
            ViewBag.SearchByCategory = new SelectList(categories);
        }


        private SelectList GetSelectListCategories(int? id)
        {
            SelectList list;

            if (id == null){
            list=  new SelectList(db.Categories.OrderBy(x=>x.Name), "Id", "Name").PreAppend("-----------", "", true);
            }
            else
            {
                list = new SelectList(db.Categories.OrderBy(x => x.Name), "Id", "Name", id).PreAppend("-----------", "", false);
            }
            return list;
        }
         private SelectList GetSelectListCovers(int? id)
        {
            SelectList list;

            if (id == null)
            {
                list = new SelectList(db.Covers.OrderBy(x => x.Name), "Id", "Name").PreAppend("-----------", "", true);
            }
            else
            {
                list = new SelectList(db.Covers.OrderBy(x => x.Name), "Id", "Name", id).PreAppend("-----------", "", false);
            }
            return list;
        }
         private SelectList GetSelectListDiscounts(int? id)
         {
             SelectList list;

             if (id == null)
             {
                 list = new SelectList(db.Discounts.OrderBy(x => x.Name), "Id", "Name").PreAppend("-----------", "", true);
             }
             else
             {
                 list = new SelectList(db.Discounts.OrderBy(x => x.Name), "Id", "Name", id).PreAppend("-----------", "", false);
             }
             return list;
         }
         private SelectList GetSelectListJewelPHotos(int? id)
         {
             SelectList list;

             if (id == null)
             {
                 list = new SelectList(db.JewelPHotos.OrderBy(x => x.Caption), "Id", "Caption").PreAppend("-----------", "", true);
             }
             else
             {
                 list = new SelectList(db.JewelPHotos.OrderBy(x => x.Caption), "Id", "Caption", id).PreAppend("-----------", "");
             }

             return list;
         }
         private SelectList GetSelectListMarkups(int? id)
         {
             SelectList list;

             if (id == null)
             {
                 list = new SelectList(db.Markups.OrderBy(x => x.Name), "Id", "Name").PreAppend("-----------", "", true);
             }
             else
             {
                 list = new SelectList(db.Markups.OrderBy(x => x.Name), "Id", "Name", id).PreAppend("-----------", "", false);
             }
             return list;
         }

         private SelectList GetSelectListMaterials(int? id)
         {
             SelectList list;

             if (id == null)
             {
                 list = new SelectList(db.Materials.OrderBy(x => x.Name), "Id", "Name").PreAppend("-----------", "", true);
             }
             else
             {
                 list = new SelectList(db.Materials.OrderBy(x => x.Name), "Id", "Name", id).PreAppend("-----------", "", false);
             }
             return list;
         }

         private SelectList GetSelectListStones(int? id)
         {
             SelectList list;

             if (id == null)
             {
                 list = new SelectList(db.Stones.OrderBy(x => x.Name), "Id", "Name").PreAppend("-----------", "", true);
             }
             else
             {
                 list = new SelectList(db.Stones.OrderBy(x => x.Name), "Id", "Name", id).PreAppend("-----------", "", false);
             }
             return list;
         }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            var product = new Product { DateOfPublishing = DateTime.Now };

          
        

            ViewBag.CategoryId = GetSelectListCategories(product.CategoryId);
            ViewBag.CoverId = GetSelectListCovers(product.CoverId);
            ViewBag.DiscountId = GetSelectListDiscounts(product.DiscountId);
            ViewBag.JewelPHotoId = GetSelectListJewelPHotos(product.JewelPHotoId);
            ViewBag.MarkupId = GetSelectListMarkups(product.MarkupId);
            ViewBag.MaterialId = GetSelectListMaterials(product.MaterialId);
            ViewBag.StoneId = GetSelectListStones(product.StoneId);
            return View(product);
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CategoryId,Size,Lenght,Weight,Diameter,ShoppingPrice,DiscountId,DateOfPublishString,MaterialId,CoverId,StoneId,JewelPHotoId,MarkupId,Count,QuantityInStock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = GetSelectListCategories(product.CategoryId);
            ViewBag.CoverId = GetSelectListCovers(product.CoverId);
            ViewBag.DiscountId = GetSelectListDiscounts(product.DiscountId);
            ViewBag.JewelPHotoId = GetSelectListJewelPHotos(product.JewelPHotoId);
            ViewBag.MarkupId = GetSelectListMarkups(product.MarkupId);
            ViewBag.MaterialId = GetSelectListMaterials(product.MaterialId);
            ViewBag.StoneId = GetSelectListStones(product.StoneId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            ViewBag.CategoryId = GetSelectListCategories(product.CategoryId);
            ViewBag.CoverId = GetSelectListCovers(product.CoverId);
            ViewBag.DiscountId = GetSelectListDiscounts(product.DiscountId);
            ViewBag.JewelPHotoId = GetSelectListJewelPHotos(product.JewelPHotoId);
            ViewBag.MarkupId = GetSelectListMarkups(product.MarkupId);
            ViewBag.MaterialId = GetSelectListMaterials(product.MaterialId);
            ViewBag.StoneId = GetSelectListStones(product.StoneId);
          
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CategoryId,Size,Lenght,Weight,Diameter,ShoppingPrice,DiscountId,DateOfPublishString,MaterialId,CoverId,StoneId,JewelPHotoId,MarkupId,Count,QuantityInStock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = GetSelectListCategories(product.CategoryId);
            ViewBag.CoverId = GetSelectListCovers(product.CoverId);
            ViewBag.DiscountId = GetSelectListDiscounts(product.DiscountId);
            ViewBag.JewelPHotoId = GetSelectListJewelPHotos(product.JewelPHotoId);
            ViewBag.MarkupId = GetSelectListMarkups(product.MarkupId);
            ViewBag.MaterialId = GetSelectListMaterials(product.MaterialId);
            ViewBag.StoneId = GetSelectListStones(product.StoneId);      
           return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

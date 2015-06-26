using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FashionStones.Controllers;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;
using FashionStones.Utils;
using FashionStones.ViewModel;


namespace FashionStones.Areas.Default.Controllers
{
    public class StoreController : BaseController
    {
        #region public
        private IQueryable<Product> GetProductsByCategoryId(int? id)
        {
            if (id.HasValue && DataManager.Categories.SearchFor(t => t.Id == id).Any())
            {
                return DataManager.Products.SearchFor(t => t.CategoryId == id);
            }
             return DataManager.Products.GetAll();
        }
        private IQueryable<Product> GetProductByStone(IQueryable<Product> products,int? id)
        {
            return products.Where(t => t.StoneId == id);
        }
        private IEnumerable<Product> SortProductByOptions(IEnumerable<Product> products, string sort)
        {
            if (sort == "novelty")
            {
                return products.OrderByDescending(t => t.DateOfPublishing);
            }
            if (sort == "expensive")
            {
                return products.OrderByDescending(t => t.ShoppingPrice);
            }
            if (sort == "cheap")
            {
                return products.OrderBy(x => x.ShoppingPrice);
            }
                return products.OrderByDescending(t => t.DateOfPublishing);
        }
        public ActionResult Index(int? catId, string catName, int? stoneId, string stone,  
            string sort,string search,int page=1, int limit=15 )
        {
            IQueryable<Product> query;
            
            if (catId.HasValue == false)
            {
                catId = 0;
                catName = "All";
                ViewBag.Title = "Fashion Stones";
            }
            else if (DataManager.Categories.SearchFor(x=>x.Id==catId).Any())
            {
                var cat = DataManager.Categories.SearchFor(x => x.Id == catId).Single();
                catName = cat.TranslitName();
                ViewBag.Title = cat.Name;
            }
            query = GetProductsByCategoryId(catId); // товар выбраной категории
           
            if (stoneId.HasValue)
            {
                query = GetProductByStone(query, stoneId);
            }
            IEnumerable<Product> list = query.ToList(); 
            list= SortProductByOptions(list, sort);
            var model = new ProductViewModel(list.AsQueryable(), page, limit, sort, catId, catName, stoneId, stone, search);
            return View((model));
        }

        private IEnumerable<Product> GetProductBySearch(IEnumerable<Product> products, string search)
        {
         
            List<Product> list = new List<Product>();
            search = search.ToLower();
            foreach (var pr in products)
            {
                if (pr.Id.ToString().Contains(search))
                {
                    list.Add(pr); 
                    continue;
                }
                if (pr.Category != null && pr.Category.Name.ToLower().Contains(search))
                {
                    list.Add(pr);
                    continue;
                }
                if (pr.Stone != null && pr.Stone.Name.ToLower().Contains(search))
                {
                    list.Add(pr);
                    continue;
                }

                 if (pr.Material != null &&  pr.Material.Name.ToLower().Contains(search))
                {
                    list.Add(pr);
                    continue;
                 }

                 if (pr.Cover != null && pr.Cover.Name.ToLower().Contains(search))
                {
                    list.Add(pr);
                    continue;
                }
            }
            return list;
        }
      
        public ActionResult Search(string text, string sort = "novelty", int page = 1, int limit = 15)
        {
            var query = DataManager.Products.GetAll().ToList();
            var list=  GetProductBySearch(query, text);
            var model = new ProductViewModel(list.AsQueryable(), page, limit, sort, text);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult CatalogStones()
        {
            var list = DataManager.Stones.GetAll().OrderBy(x => x.Name).Where(x => x.Products.Count > 0);
            return PartialView(list);
        }
        [ChildActionOnly]
        public ActionResult CatalogCategory()
        {
            var list = DataManager.Categories.GetAll().OrderBy(x => x.Name);
            return PartialView(list);
        }

        [ChildActionOnly]
        public int ShoppingCartCount()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, DataManager);
            return cart.GetCountItems();
        }



        public ActionResult InformPayment()
        {
            return View();
        }
        [HttpPost]
         public ActionResult InformPayment(InformPaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View("InformPaymentConfirm", "", model);
            }
            return View();
        }









        //public JsonResult AutoCompleteCountry(string term)
        //{
        //    var result = (from r in DataManager.Products.GetAll()
        //                  where r.Name.ToLower().Contains(term.ToLower())
        //                  select new { Name = r.Name, Value = r.Id }).ToList();
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        public StoreController(DataManager dataManager) : base(dataManager)
        {
        }

        #endregion     
    }
}
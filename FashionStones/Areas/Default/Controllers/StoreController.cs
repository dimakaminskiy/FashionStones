using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using FashionStones.Controllers;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;
using FashionStones.Utils;
using FashionStones.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


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

            if (Request.HttpMethod.ToString() == "POST")
            {
                return RedirectToAction("Index", new
                {
                    catId = catId,
                    catName = catName,
                    stoneId = stoneId,
                    stone = stone,
                    sort = sort,
                    search = search,
                    page = page,
                    limit = limit
                });
            }


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
                ViewBag.Title = "Fashion Stones - "+cat.Name;
            }
            query = GetProductsByCategoryId(catId); // товар выбраной категории
           
            if (stoneId.HasValue)
            {
                query = GetProductByStone(query, stoneId);
                ViewBag.Title = "Fashion Stones - " + DataManager.Stones.SearchFor(x => x.Id == stoneId).Single().Name;
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
      
        public ActionResult Search(string text, string sort, int? page, int? limit)
        {

            if (string.IsNullOrEmpty(text)) return RedirectToAction("Index");



            if (Request.HttpMethod.ToString() == "POST")
            {
                return RedirectToAction("Search", new
                {
                    text = text,
                    sort = sort,
                    page = page,
                    limit = limit,
                });
            }








            if (!page.HasValue) page = 1;
            if (!limit.HasValue) limit = 15;
            if (string.IsNullOrEmpty(sort)) sort = "novelty";
           










            var query = DataManager.Products.GetAll().ToList();
            var list=  GetProductBySearch(query, text);
            list = SortProductByOptions(list, sort);
            var model = new ProductViewModel(list.AsQueryable(), page.Value, limit.Value, sort, text);
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
                EmailSettings settings = new EmailSettings();
                SmtpClient smtp = new SmtpClient();
                smtp.Host = settings.ServerName;
                smtp.Port = settings.ServerPort;
                smtp.EnableSsl = settings.UseSsl;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(settings.MailFromAddress, settings.password);

                using (var msg = new MailMessage(settings.MailFromAddress, settings.MailFromAddress))
                {
                        string message= string.Format("Номер заказа {0}<br>Номер телефона {1}<br>Сумма оплаты {2}<br>Время платежа {3}",
                        model.OrderId, model.PhoneNumber, model.Total,model.TimePay);
                        msg.Subject = "Сообщение об оплате";//message;
                        msg.IsBodyHtml = true;
                        msg.Body = message;
                    try
                    {
                         smtp.Send(msg);
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                       
                }
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
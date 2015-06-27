using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FashionStones.Controllers;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;
using FashionStones.ViewModel;

namespace FashionStones.Areas.Default.Controllers
{
    public class ShoppingCartController : BaseController
    {
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this, DataManager);
            try
            {
                var viewModel = new ShoppingCartViewModel
                {
                    CartItems = cart.GetCartItems(),
                    CartTotal = cart.GetTotal()
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                cart.EmptyCart();


            }
            return View(new ShoppingCartViewModel { CartItems = new List<Cart>() });
        }
        [HttpPost]
        public ActionResult AddToCard(int id, int count)
        {
            try
            {
              bool flag = User.Identity.IsAuthenticated;
             var cart = ShoppingCart.GetCart(this,DataManager);
             cart.AddToCart(id,count,flag);
             return Json(new{success = true,item=id, itemCount = cart.GetCountItems().ToString() });          
            }
            catch (Exception e)
            {
                return Json(new { success = false, errorMessage = e.Message });
            }
           
        }
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(HttpContext, DataManager);
            ViewData["CartCount"]=cart.GetCountItems();
            return PartialView();
        }
        [HttpPost]
        public ActionResult EditCartProduct(int id, int count)
        {
            try
            {
                var shopCart = ShoppingCart.GetCart(HttpContext, DataManager);
                var cart= shopCart.SetProductCount(id, count);
                return Json(new
                {
                    success = true,
                    item = id,
                    itemCount = cart.Count,
                    itemPrice=cart.TotalPrice,
                    itemTotalCount = shopCart.GetCountItems(),
                    itemToralPrice = String.Format("{0:0.00}", (shopCart.GetTotal()))
                });
            }
            catch (Exception e)
            {
                return Json(new { success = false, errorMessage = e.Message });
            }
        }      
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            try
            {
                var cart = ShoppingCart.GetCart(HttpContext,DataManager);
                cart.RemoveCart(id);
                return Json(new
                {
                    success = true,item = id, itemTotalCount = cart.GetCountItems(),
                    itemToralPrice = String.Format("{0:0.00}", (cart.GetTotal()))
                });
            }
            catch (Exception e)
            {
                return Json(new { success = false, errorMessage = e.Message });
            }
           
           // Server.HtmlEncode(name) +
        }
        [HttpPost]
        public ActionResult CleanCart()
        {
            try
            {
                var cart = ShoppingCart.GetCart(HttpContext,DataManager);
                cart.EmptyCart();
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
        public ShoppingCartController(DataManager dataManager) : base(dataManager){}
      }
}
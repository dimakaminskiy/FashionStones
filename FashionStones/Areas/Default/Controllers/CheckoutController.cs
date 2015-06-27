using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FashionStones.Controllers;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;
using FashionStones.Utils;
using FashionStones.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FashionStones.Areas.Default.Controllers
{
    public class CheckoutController : BaseController
    {

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Complete(int id)
        {
            if (DataManager.Orders.SearchFor(t => t.Id == id).Any())
            {
                 return View(id);
            }
            return HttpNotFound();
        }




        public ActionResult AddressAndPayment()
        {
            var cart = ShoppingCart.GetCart(HttpContext,DataManager);
            if (cart.GetCountItems() == 0) return RedirectToAction("Index", "ShoppingCart");

            var mPayId = DataManager.MethodOfPayments.GetAll().OrderBy(x => x.Id).First().Id;
            var mDelId = DataManager.MethodOfDeliveries.GetAll().OrderBy(x => x.Id).First().Id;
            var model = new Order()
            {
                Total = cart.GetTotal(),
                MethodOfDeliveryId = mDelId,
                MethodOfPaymentId = mPayId,
            };


            

            if (User.Identity.IsAuthenticated)
            {
                var user = UserManager.FindById(CurrentUserId);
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.MiddleName = user.MiddleName;
                model.Phone = user.PhoneNumber;
                model.City = user.City;
                model.Email = user.Email;
                model.Country = DataManager.Coutries.SearchFor(t => t.Id == user.CountryId).Single().Name;
            }





            ViewBag.MethodOfDeliveryId =
                new SelectList(DataManager.MethodOfDeliveries.GetAll()
                    .OrderBy(x => x.Id), "Id", "Name", model.MethodOfDeliveryId);
            ViewBag.MethodOfPaymentId = new SelectList(DataManager.MethodOfPayments.GetAll()
                    .OrderBy(x => x.Id), "Id", "Name", model.MethodOfPaymentId);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddressAndPayment(Order model)
        {
            if (ModelState.IsValid)
            {
                var cart = ShoppingCart.GetCart(HttpContext, DataManager);
                if (cart.GetCountItems() == 0) return RedirectToAction("Index", "ShoppingCart");
                var orderId = cart.CreateOrder(model);
                return RedirectToAction("Complete","Checkout",
                    new { id = orderId });
            }
            ViewBag.MethodOfDeliveryId =
                new SelectList(DataManager.MethodOfDeliveries.GetAll()
                    .OrderBy(x => x.Name), "Id", "Name", model.MethodOfDeliveryId);
            ViewBag.MethodOfPaymentId = new SelectList(DataManager.MethodOfPayments.GetAll()
                    .OrderBy(x => x.Name), "Id", "Name", model.MethodOfPaymentId);
            return View(model);
        }
        public CheckoutController(DataManager dataManager) : base(dataManager)
        {
        }
    }
}
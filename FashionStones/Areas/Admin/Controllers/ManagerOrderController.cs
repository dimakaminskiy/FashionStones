using System;
using System.Linq;
using System.Web.Mvc;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;
using FashionStones.ViewModel;

namespace FashionStones.Areas.Admin.Controllers
{

    public class ManagerOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult BrowseNewOrders()
        {
            var list = db.Orders.Where(i => i.OrderStatusId == StatusOrder.New);
            return View(list);
        }

        [HttpGet]
        public ActionResult BrowseOrdersInProcessing()
        {
            var list = db.Orders.Where(i => i.OrderStatusId == StatusOrder.InProcessing);
            return View(list);
        }

        [HttpGet]
        public ActionResult BrowseExecutedOrders()
        {
            var list = db.Orders.Where(i => i.OrderStatusId == StatusOrder.Executed);
            return View(list);
        }
        [HttpPost]
        public ActionResult DeleteFromListOrder(int id)
        {
            try
            {
                var o = db.OrderDetails.Single(t=>t.Id==id);
                var order = db.Orders.Single(t=>t.Id==o.OrderId);
                db.OrderDetails.Remove(o);


                var list = db.OrderDetails.Where(t => t.OrderId == order.Id).ToList();
                var total = (from d in list select d.UnitPrice * d.Quantity).Sum();
                order.Total = total;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false
                });

            }
            return Json(new { success = true });
        }



        [HttpPost]
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                var listDetails = db.OrderDetails.Where(i => i.OrderId == id).ToList();
                if (listDetails.Any())
                {
                    foreach (OrderDetail orderDetail in listDetails)
                    {
                        db.OrderDetails.Remove(orderDetail);
                    }
                   
                }

                var order = db.Orders.Single(x => x.Id == id);
                db.Orders.Remove(order);
                db.SaveChanges();

            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Произошла ошибка при удалении заказа"
                });
            }
            return Json(new { success = true });
        }



        [HttpGet]
        public ActionResult EditOrder(int id)
        {
            var or = db.Orders.First(t => t.Id == id);
            if (or == null) return HttpNotFound();

            var orDetails = db.OrderDetails.Where(i => i.OrderId == or.Id).ToList();

            // var list = orDetails.Where(t => t.OrderId == id).ToList();
            var model = new OrderViewModel
            {
                order = or,
                orderDetails = orDetails
            };


            ViewBag.OrderStatusId = new SelectList(db.OrderStatuses.ToList(), "Id", "Name", model.order.OrderStatusId);
            ViewBag.MethodOfDeliveryId = new SelectList(db.MethodOfDeliveries.ToList(), "Id", "Name",
                model.order.MethodOfDeliveryId);
            ViewBag.MethodOfPaymentId = new SelectList(db.MethodOfPayments.ToList(), "Id", "Name",
                model.order.MethodOfPaymentId);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditOrder(OrderViewModel model)
        {

            if (ModelState.IsValid)
            {

                var original = db.Orders.Find(model.order.Id);
                if (original != null)
                {
                    original.City = model.order.City;
                    original.Country = model.order.Country;
                    original.Email = model.order.Email;
                    original.LastName = model.order.LastName;
                    original.FirstName = model.order.FirstName;
                    original.MiddleName = model.order.MiddleName;
                    original.Phone = model.order.Phone;
                    original.Total = model.order.Total;
                    original.TextInfo = model.order.TextInfo;
                    original.MethodOfDeliveryId = model.order.MethodOfDeliveryId;
                    original.MethodOfPaymentId = model.order.MethodOfPaymentId;
                    original.OrderStatusId = model.order.OrderStatusId;
                    db.SaveChanges();
                }
                if (model.order.OrderStatusId == StatusOrder.New) return RedirectToAction("BrowseNewOrders");
                else if (model.order.OrderStatusId == StatusOrder.InProcessing)
                    return RedirectToAction("BrowseOrdersInProcessing");
                else return RedirectToAction("BrowseExecutedOrders");
            }
            ViewBag.OrderStatusId = new SelectList(db.OrderStatuses.ToList(), "Id", "Name", model.order.OrderStatusId);
            ViewBag.MethodOfDeliveryId = new SelectList(db.MethodOfDeliveries.ToList(), "Id", "Name",
                model.order.MethodOfDeliveryId);
            ViewBag.MethodOfPaymentId = new SelectList(db.MethodOfPayments.ToList(), "Id", "Name",
                model.order.MethodOfPaymentId);
            return View(model);
        }

        public ActionResult EditListOrder(int id)
        {
            var or = db.Orders.FirstOrDefault(t => t.Id == id);
            if (or == null) return HttpNotFound();
            var orDetails = db.OrderDetails.Where(i => i.OrderId == or.Id).ToList();
            return View(orDetails);
        }

        [HttpGet]
        public ActionResult EditItemFromOrderDetails(int id)
        {
            var model = db.OrderDetails.FirstOrDefault(t => t.Id == id);
            if (model == null) return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditItemFromOrderDetails(OrderDetail model)
        {

            if (ModelState.IsValid)
            {
                var original = db.OrderDetails.Single(t => t.Id == model.Id);
                if (original != null)
                {
                    if (original.Quantity != model.Quantity || Math.Abs(original.UnitPrice - model.UnitPrice) > 0)
                        original.Quantity = model.Quantity;
                    original.UnitPrice = model.UnitPrice;
                    db.SaveChanges();
                    var order = db.Orders.Single(t => t.Id == original.OrderId);
                    var list = db.OrderDetails.Where(t => t.OrderId == order.Id).ToList();
                    var total = (from d in list select d.UnitPrice*d.Quantity).Sum();
                    order.Total = total;
                    db.SaveChanges();
                    return RedirectToAction("EditListOrder", "ManagerOrder", new {id = order.Id});
                }
            }
            return View(model);

        }

    }

    public static class StatusOrder
        {
            public const int New = 1;
            public const int InProcessing = 2;
            public const int Executed = 3;
        }
    }

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;

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

     //       [HttpPost]
     //       public ActionResult DeleteOrder(int id)
     //       {
     //           try
     //           {
     //               var listDetails = dataManager.OrderDetails.GetOrderDetails().Where(i => i.OrderId == id);
     //               if (listDetails.Any())
     //               {
     //                   foreach (OrderDetail orderDetail in listDetails)
     //                   {
     //                       dataManager.OrderDetails.DeleteOrderDetailById(orderDetail.Id);
     //                   }
     //               }
     //               dataManager.Orders.DeleteOrderById(id);
     //           }
     //           catch (Exception)
     //           {
     //               return Json(new
     //               {
     //                   success = false,
     //                   errorMessage = "Произошла ошибка при удалении заказа"
     //               });
     //           }
     //           return Json(new { success = true });
     //       }



     //       [HttpGet]
     //       public ActionResult EditOrder(int id)
     //       {
     //           var or = dataManager.Orders.GetOrderById(id);
     //           var orDetails = dataManager.OrderDetails.GetOrderDetails().Where(i => i.OrderId == or.Id).ToList();

     //           List<ProduceModel> list = orDetails.Select(d => new ProduceModel
     //           {
     //               PrepackageId = d.ProduceInPackageId,
     //               Name = d.ProduceInPackage.Produce.Name,
     //               Package = d.ProduceInPackage.Prepackage.Weight.ToString(CultureInfo.InvariantCulture),
     //               Category = d.ProduceInPackage.Produce.Category.Name,
     //               Price = d.UnitPrice,
     //               Count = d.Quantity
     //           }).ToList();

     //           var model = new OrderViewModel
     //           {
     //               order = or,
     //               orderDetails = list
     //           };

     //           ViewBag.OrderStatusId = new SelectList(dataManager.OrderStatuses.GetOrderStatuses(), "Id", "Name", model.order.OrderStatusId);
     //           return System.Web.UI.WebControls.View(model);
     //       }

     //       [HttpPost]
     //       public ActionResult EditOrder(OrderViewModel model)
     //       {

     //           if (ModelState.IsValid)
     //           {
     //               dataManager.Orders.SaveOrder(model.order);

     //               if (model.order.OrderStatusId == StatusOrder.New) return RedirectToAction("BrowseNewOrders");
     //               else if (model.order.OrderStatusId == StatusOrder.InProcessing) return RedirectToAction("BrowseOrdersInProcessing");
     //               else return RedirectToAction("BrowseExecutedOrders");

     //           }
     //           var orDetails = dataManager.OrderDetails.GetOrderDetails().Where(i => i.OrderId == model.order.Id).ToList();

     //           List<ProduceModel> list = orDetails.Select(d => new ProduceModel
     //           {
     //               PrepackageId = d.ProduceInPackageId,
     //               Name = d.ProduceInPackage.Produce.Name,
     //               Package = d.ProduceInPackage.Prepackage.Weight.ToString(CultureInfo.InvariantCulture),
     //               Category = d.ProduceInPackage.Produce.Category.Name,
     //               Price = d.UnitPrice,
     //               Count = d.Quantity
     //           }).ToList();
     //           model.orderDetails = list;
     //           ViewBag.OrderStatusId = new SelectList(dataManager.OrderStatuses.GetOrderStatuses(), "Id", "Name", model.order.OrderStatusId);
     //           return System.Web.UI.WebControls.View(model);
     //       }

     //       public ActionResult EditListOrder(int id)
     //       {



     //           var or = dataManager.Orders.GetOrderById(id);
     //           var orDetails = dataManager.OrderDetails.GetOrderDetails().Where(i => i.OrderId == or.Id).ToList();

     //           List<OrderDetailViewModel> list = orDetails.Select(d => new OrderDetailViewModel
     //           {
     //               Id = d.Id,
     //               Category = d.ProduceInPackage.Produce.Category.Name,
     //               Package = d.ProduceInPackage.Prepackage.Weight.ToString(CultureInfo.InvariantCulture),
     //               Producer = d.ProduceInPackage.Produce.Name,
     //               Quantity = d.Quantity,
     //               UnitPrice = d.UnitPrice
     //           }).ToList();

     //           var model = new EditOrderDetailViewModel
     //           {
     //               OrderId = or.Id,
     //               list = list
     //           };

     //           return System.Web.UI.WebControls.View(model);
     //       }

     //       public ActionResult DeleteFromListOrder(int id)
     //       {
     //           try
     //           {
     //               var o = dataManager.OrderDetails.GetOrderDetailById(id);

     //               var order = dataManager.Orders.GetOrderById(o.OrderId);
     //               dataManager.OrderDetails.DeleteOrderDetailById(id);

     //               var total =
     //                   dataManager.OrderDetails.GetOrderDetails()
     //                       .Where(p => p.OrderId == order.Id)
     //                       .Sum(i => i.UnitPrice * i.Quantity);


     //               order.Total = total;
     //               dataManager.Orders.SaveOrder(order);


     //           }
     //           catch (Exception)
     //           {
     //               return Json(new
     //               {
     //                   success = false
     //               });

     //           }
     //           return Json(new { success = true });
     //       }

     //       [HttpGet]
     //       public ActionResult AddToListOrder(int id)
     //       {
     //           var productCatalog = new ProduceCatalog
     //           {
     //               OrderId = id
     //           };
     //           return System.Web.UI.WebControls.View(productCatalog);
     //       }

     //       [HttpPost]
     //       public ActionResult AddToListOrder(ProduceCatalog model)
     //       {
     //           if (ModelState.IsValid)
     //           {
     //               int pId = (model.SelectedPrepackageId.HasValue) ? (int)model.SelectedPrepackageId : 0;
     //               int prod = (model.SelectedProduceId.HasValue) ? (int)(model.SelectedProduceId) : 0;
     //               int storeId = GetStoreIdByProduceAndPackage(pId, prod);

     //               OrderDetail or = new OrderDetail
     //               {
     //                   OrderId = model.OrderId,
     //                   ProduceInPackageId = storeId,
     //                   Quantity = model.Count,
     //                   UnitPrice = dataManager.ProduceInPackages.GetProduceInPackageById(storeId).Price
     //               };

     //               dataManager.OrderDetails.CreateOrderDetail(or);



     //               var o = dataManager.Orders.GetOrderById(model.OrderId);
     //               o.Total = GetTotalPriceByOrder(o);
     //               dataManager.Orders.SaveOrder(o);


     //               return RedirectToAction("EditListOrder", "ManagerOrder", new { id = model.OrderId });

     //           }

     //           return System.Web.UI.WebControls.View(model);
     //       }

     //       double GetTotalPriceByOrder(Order order)
     //       {
     //           return dataManager.OrderDetails.GetOrderDetails()
     //                        .Where(i => i.OrderId == order.Id)
     //                        .Select(s => s.UnitPrice * s.Quantity)
     //                        .Sum();
     //       }



     //       [AcceptVerbs(HttpVerbs.Get)]
     //       public JsonResult GetJsonProduceByCategoryId(int id = 0)
     //       {

     //           var list = from p in dataManager.Produces.GetProduces()
     //                      where p.CategoryId == id
     //                      select new { id = p.Id, Text = p.Name };

     //           return Json(list, JsonRequestBehavior.AllowGet);

     //       }

     //       [AcceptVerbs(HttpVerbs.Get)]
     //       public JsonResult GetJsonPackagesbyProduce(int id)
     //       {
     //           var list = (from p in dataManager.ProduceInPackages.GetProduceInPackages()
     //                       where p.ProduceId == id
     //                       orderby p.Prepackage.Weight
     //                       select new { id = p.Prepackage.Id, Text = p.Prepackage.Weight + " г" }).Distinct();
     //           return Json(list, JsonRequestBehavior.AllowGet);

     //       }

     //       int GetStoreIdByProduceAndPackage(int packId, int produceId)
     //       {

     //           return dataManager.ProduceInPackages.GetProduceInPackages()
     //               .FirstOrDefault(i => i.PrepackageId == packId && i.ProduceId == produceId).Id;
     //       }

     //       [HttpGet]
     //       public ActionResult EditItemFromOrderDetails(int id)
     //       {
     //           var or = dataManager.OrderDetails.GetOrderDetailById(id);
     //           var productCatalog = new ProduceCatalog
     //           {
     //               OrderId = or.OrderId,
     //               SelectedProduceId = or.ProduceInPackage.ProduceId,
     //               SelectedCategoryId = or.ProduceInPackage.Produce.Category.Id,
     //               SelectedPrepackageId = or.ProduceInPackageId,
     //               Count = or.Quantity,
     //               OrderDetailId = or.Id
     //           };
     //           return System.Web.UI.WebControls.View(productCatalog);

     //       }
     //       [HttpPost]
     //       public ActionResult EditItemFromOrderDetails(ProduceCatalog model)
     //       {

     //           if (ModelState.IsValid)
     //           {
     //               var or = dataManager.OrderDetails.GetOrderDetailById(model.OrderDetailId);
     //               var order = dataManager.Orders.GetOrderById(model.OrderId);

     //               int pId = (model.SelectedPrepackageId.HasValue) ? (int)model.SelectedPrepackageId : 0;
     //               int prod = (model.SelectedProduceId.HasValue) ? (int)(model.SelectedProduceId) : 0;
     //               int storeId = GetStoreIdByProduceAndPackage(pId, prod);
     //               or.ProduceInPackageId = storeId;
     //               or.Quantity = model.Count;
     //               or.UnitPrice = dataManager.ProduceInPackages.GetProduceInPackageById(storeId).Price;
     //               dataManager.OrderDetails.SaveOrderDetail(or);
     //               order.Total = GetTotalPriceByOrder(order);
     //               dataManager.Orders.SaveOrder(order);

     //               return RedirectToAction("EditListOrder", "ManagerOrder", new { id = order.Id });
     //           }
     //           return System.Web.UI.WebControls.View(model);

         //   }
        }
    public static class StatusOrder
    {
        public const int New = 1;
        public const int InProcessing = 2;
        public const int Executed = 3;
    }
}
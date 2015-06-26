using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using FashionStones.Controllers;
using FashionStones.Models;

namespace FashionStones.Areas.Default.Controllers
{
    public class ProductsController : BaseController
    {
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model= DataManager.Products.SearchFor(x => x.Id == id).FirstOrDefault();
            if (model == null)
            {
                return HttpNotFound();
            }
            return PartialView(model);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        public ActionResult ProductInfo(int id)
        {
            var model = DataManager.Products.SearchFor(x => x.Id == id).FirstOrDefault();
            if (model == null)
            {
                return HttpNotFound();
            }
            return Json(new { success = true, 
              id=model.Id,
              name=model.Name,
              imgBig=model.JewelPHoto.PathToBig,
              imgSmall=model.JewelPHoto.PathToSmall,
              data = RenderPartialViewToString("ItemInfoView", model)
              , JsonRequestBehavior.AllowGet});
        }





        public ProductsController(DataManager dataManager) : base(dataManager)
        {
        }
    }


    



}
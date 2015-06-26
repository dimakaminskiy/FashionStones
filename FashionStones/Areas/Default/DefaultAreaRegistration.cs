using System.Web.Mvc;

namespace FashionStones.Areas.Default
{
    public class DefaultAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Default";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                 name: "",
                 url: "Store/cat-{catId}/category-{catName}/page-{page}/limit-{limit}/sort-{sort}",
                 defaults: new { Controller = "Store", action = "Index" },
                 constraints: new { catId = @"\d+" },
                 namespaces: new string[] { "FashionStones.Areas.Default.Controllers" }
             );
            context.MapRoute(
                  name: "",
                  url: "Store/cat-{catId}/category-{catName}/page-{page}/limit-{limit}",
                  defaults: new { Controller = "Store", action = "Index" },
                  constraints: new { catId = @"\d+" },
                  namespaces: new string[] { "FashionStones.Areas.Default.Controllers" }
              );
            context.MapRoute(
                  name: "",
                  url: "Store/Search/text-{text}/page-{page}/limit-{limit}/sort-{sort}",
                  defaults: new { Controller = "Store", action = "Search" },
                  namespaces: new string[] { "FashionStones.Areas.Default.Controllers" }
              );
            context.MapRoute(
                  name: "",
                  url: "Store/cat-{catId}/category-{catName}/",
                  defaults: new { Controller = "Store", action = "Index", catId = UrlParameter.Optional },
                  constraints: new { catId = @"\d+" },
                  namespaces: new string[] { "FashionStones.Areas.Default.Controllers" }
              );

            context.MapRoute(
                 name: "stone",
                 url: "Store/stoneId-{stoneId}/stone-{stone}",
                 defaults: new { Controller = "Store", action = "Index", catId = 0 },
                 namespaces: new string[] { "FashionStones.Areas.Default.Controllers" }
             );





            context.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}",
                defaults: new { Controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FashionStones.Areas.Default.Controllers" }
            );


            //context.MapRoute(
            //    "Default_default",
            //    "Default/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
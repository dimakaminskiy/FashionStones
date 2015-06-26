using System.Web.Mvc;

namespace FashionStones.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
             null,
             "Admin/PHoto/{action}",
             new { controller = "PHoto" }
            , new string[] { "FashionStones.Areas.Admin.Controllers" }
            );


            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FashionStones.Areas.Admin.Controllers" }
            );
        }
    }
}
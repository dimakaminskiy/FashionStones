using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionStones.Models;
using FashionStones.Utils;

namespace FashionStones.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return PartialView(db.Categories);
        }

   

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionStones.Controllers;
using FashionStones.Models;
using FashionStones.Models.Domain.Entities;
using FashionStones.Utils;

namespace FashionStones.Areas.Default.Controllers
{
    public class StoneController : BaseController
    {
        // GET: Default/Stone
        public ActionResult Index(int id, string name, int page = 1, int limit = 15, string sort = "")
        {
            //var stone = DataManager.Stones.SearchFor(x => x.Id == id).Single();
            //if (stone == null)
            //{
            //    ViewBag.Title = "Fashion Stones";
            //}
            //else
            //{
            //    ViewBag.Title = stone.Name;
            //}
            //if (stone != null)
            //{
            //    var query = stone.Products.AsQueryable();

            //    var options = new ItemSortOptions<Product>(new SortProductsProvider());
            //    var limitOptions = new ItemLimitOptions(ItemLimitSettings.GlobalLimitOptions, limit);
            //    var pb = new PageableData2<Product>(query, options, limitOptions, page, limit, sort);

            //    return View(pb);
            //}
            return View();
        }

        public StoneController(DataManager dataManager) : base(dataManager)
        {
        }
    }
}
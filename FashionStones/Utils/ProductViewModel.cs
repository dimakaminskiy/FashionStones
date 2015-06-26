using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionStones.Models.Domain.Entities;

namespace FashionStones.Utils
{
    public class ProductViewModel
    {
        protected static IEnumerable<int> GlobalLimitOptions = new List<int>() { 12, 15, 18, 21, 24 };
        protected static int ItemPerPageDef=15;

        public string DefSortOption { get { return "novelty"; } }
        public string Sort { get; set; }
        private List<SelectListItem> _sortOptions = new List<SelectListItem>()
        {
            new SelectListItem {Text = "от дешевых к дорогим", Value = "cheap"},
            new SelectListItem {Text = "от дорогих к дешевым", Value = "expensive"},
            new SelectListItem {Text = "новинки", Value = "novelty"},
        };
        public List<SelectListItem> SortOptions
        {
            get
            {
            
                    if (string.IsNullOrEmpty(Sort))
                    {
                        if (_sortOptions != null) _sortOptions.Single(t => t.Value == DefSortOption).Selected = true;
                    }
                    else
                    {
                        if (_sortOptions != null) _sortOptions.Single(t => t.Value == Sort).Selected = true;
                    }
                return _sortOptions;
            }
        }

        public List<SelectListItem> LimitOptions
        {
            get { return _limitOptions ?? (_limitOptions = GetLimitSelectList(GlobalLimitOptions, ItemPerPage)); }
        }
        private List<SelectListItem> _limitOptions;
        private List<SelectListItem> GetLimitSelectList(IEnumerable<int> options, int limit)
        {
            List<SelectListItem> selectList =
                (from c in options
                 select new SelectListItem
                 {
                     Selected = (c == limit),
                     Text = c.ToString(),
                     Value = c.ToString()
                 }).ToList();
            return selectList;
        }
     
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Stone { get; set; }
        public int? StoneId { get; set; }
        public string SearchString { get; set; }
        public IEnumerable<Product> List { get; set; }
        public int PageNo { get; set; }
        public int CountPage { get; set; }
        public int ItemPerPage { get; set; }
        public ProductViewModel(IQueryable<Product> products, int page, int itemPerPage,string sort,int? catId,
            string catName,int? stoneId,string stoneName,string search)
        {
            if (GlobalLimitOptions.Count(t => t == itemPerPage) <= 0)
            {
                itemPerPage = ItemPerPageDef;
            }
            ItemPerPage = itemPerPage;
            Sort = sort;
            CategoryId = catId;
            CategoryName = catName;
            StoneId= stoneId;
            Stone = stoneName;
            SearchString = search;
            if (page < 0) page = 1;
            PageNo = page;
            var count = products.Count();
            CountPage = (int) decimal.Remainder(count, itemPerPage) == 0 ? count/itemPerPage : count/itemPerPage + 1;
            List = products.Skip((PageNo - 1) * itemPerPage).Take(itemPerPage);
        }

        public ProductViewModel(IQueryable<Product> products, int page, int itemPerPage, string sort,  string search)
        {
            if (GlobalLimitOptions.Count(t => t == itemPerPage) <= 0)
            {
                itemPerPage = ItemPerPageDef;
            }
            if (page < 0) page = 1;
            PageNo = page;
            ItemPerPage = itemPerPage;
            ItemPerPage = itemPerPage;
            Sort = sort;
            SearchString = search;
            var count = products.Count();
            CountPage = (int)decimal.Remainder(count, itemPerPage) == 0 ? count / itemPerPage : count / itemPerPage + 1;
            List = products.Skip((PageNo - 1) * itemPerPage).Take(itemPerPage);
        }

      
            


  }
}
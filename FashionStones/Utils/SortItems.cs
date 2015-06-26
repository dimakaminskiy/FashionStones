using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using FashionStones.Models.Domain.Entities;

namespace FashionStones.Utils
{
    public  interface ISortItemsProvider<T> where T : class
    {
          IQueryable<T> SortByDate(IQueryable<T> listItems);
          IQueryable<T> SortByPrice(IQueryable<T> listItems);
          IQueryable<T> SortByPriceDesc(IQueryable<T> listItems);
          IQueryable<T> DefaultSort(IQueryable<T> listItems);
    }

      public class SortProductsProvider : ISortItemsProvider<Product>
    {
          public  IQueryable<Product> SortByDate(IQueryable<Product> listItems)
          {
              return listItems.OrderByDescending(t => t.DateOfPublishing);
          }

          public  IQueryable<Product> SortByPrice(IQueryable<Product> listItems)
          {
             return listItems.OrderBy(x => x.ShoppingPrice);
          }

          public  IQueryable<Product> SortByPriceDesc(IQueryable<Product> listItems)
          {
              return listItems.OrderByDescending(x => x.ShoppingPrice);
          }

          public  IQueryable<Product> DefaultSort(IQueryable<Product> listItems)
          {
              return SortByDate(listItems);
          }
    }

    public class ItemSortOptions<T> where T : class
    {
        public ISortItemsProvider<T> Provider { get; set; }
        public ItemSortOptions(ISortItemsProvider<T> provider)
        {
            Provider = provider;
            DefaultSortOption = "novelty";
        }

        public List<SelectListItem> ListSortOptions
        {
            get { return _sortOptions; }
        }
        public string DefaultSortOption { get; set; }
        public string SelectOption { get; set; }
        private List<SelectListItem> _sortOptions = new List<SelectListItem>()
        {
            new SelectListItem {Text = "от дешевых к дорогим", Value = "cheap"},
            new SelectListItem {Text = "от дорогих к дешевым", Value = "expensive"},
            new SelectListItem {Text = "новинки", Value = "novelty"},
        };

        public void SetSelectOpions(string select)
        {
            if (string.IsNullOrEmpty(select) || string.IsNullOrWhiteSpace(select))
            {
                SelectOption = "";
                _sortOptions.Single(x => x.Value == DefaultSortOption).Selected = true;
            }
            else
            {
                _sortOptions.Single(x => x.Value == SelectOption).Selected = true;
            }

        }
      



    }


    public static class ItemLimitSettings
    {
        public static IEnumerable<int> AdminLimitOptions = new List<int>() { 8, 12, 16, 24 };

        public static IEnumerable<int> GlobalLimitOptions = new List<int>() { 12, 15, 18, 21, 24 };

    }
       
    public class ItemLimitOptions
    {
        private readonly IEnumerable<int> _options;
        private readonly int _itemPerPage;

        public ItemLimitOptions(IEnumerable<int> options, int itemPerPage)
        {
            _options = options;
            _itemPerPage = itemPerPage;
        }

        public IEnumerable<SelectListItem> LimitSelectList()
        {
            IEnumerable<SelectListItem> selectList =
                (from c in _options
                 select new SelectListItem
                 {
                     Selected = (c == _itemPerPage),
                     Text = c.ToString(),
                     Value = c.ToString()
                 }).ToList();
            return selectList;
        }

    }
  




}
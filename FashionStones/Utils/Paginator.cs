using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FashionStones.Areas.Default.Controllers;
using FashionStones.Models.Domain.Entities;

namespace FashionStones.Utils
{

    public class PageableData<T> where T : class
    {
        protected static int ItemPerPageDefault = 6;

        public IEnumerable<T> List { get; set; }

        public int PageNo { get; set; }

        public int CountPage { get; set; }

        public int ItemPerPage { get; set; }

        public PageableData(IQueryable<T> queryableSet, int page, int itemPerPage = 0)
        {
            if (itemPerPage == 0)
            {
                itemPerPage = ItemPerPageDefault;
            }
            ItemPerPage = itemPerPage;

            PageNo = page;
            var count = queryableSet.Count();

            CountPage = (int) decimal.Remainder(count, itemPerPage) == 0 ? count/itemPerPage : count/itemPerPage + 1;
            List = queryableSet.Skip((PageNo - 1)*itemPerPage).Take(itemPerPage);
        }
    }

    //public class PageableData2<T> where T : class
    //{
    //    public ItemLimitOptions LimitOptions { get; set; }
    //    public ItemSortOptions<T> SortOptions { get; set; }

    //    protected static int ItemPerPageDefault = 6;

    //    public IEnumerable<T> List { get; set; }

    //    public int PageNo { get; set; }

    //    public int CountPage { get; set; }

    //    public int ItemPerPage { get; set; }

    //    public string Category { get; set; }

    //    public PageableData2(IQueryable<T> queryableSet, ItemSortOptions<T> sortOptions, ItemLimitOptions limitOptions,
    //        int page, int itemPerPage = 0, string sort = "")
    //    {
    //        if (itemPerPage == 0)
    //        {
    //            itemPerPage = ItemPerPageDefault;
    //        }
    //        SortOptions = sortOptions;
    //        LimitOptions = limitOptions;

    //        ItemPerPage = itemPerPage;

    //        PageNo = page;
    //        var count = queryableSet.Count();

    //        CountPage = (int) decimal.Remainder(count, itemPerPage) == 0 ? count/itemPerPage : count/itemPerPage + 1;


    //        SortOptions.SelectOption = sort;
    //        if (string.IsNullOrEmpty(sort) || string.IsNullOrWhiteSpace(sort))
    //        {
    //            SortOptions.SetSelectOpions(sort);
    //            queryableSet = SortOptions.Provider.DefaultSort(queryableSet);
    //        }
    //        else
    //        {
    //            switch (sort)
    //            {
    //                case "novelty":
    //                    SortOptions.SetSelectOpions(sort);
    //                    queryableSet = SortOptions.Provider.SortByDate(queryableSet);
    //                    break;
    //                case "expensive":
    //                    SortOptions.SetSelectOpions(sort);
    //                    queryableSet = SortOptions.Provider.SortByPriceDesc(queryableSet);
    //                    break;
    //                case "cheap":
    //                    SortOptions.SetSelectOpions(sort);
    //                    queryableSet = SortOptions.Provider.SortByPrice(queryableSet);
    //                    break;
    //                default:
    //                    SortOptions.SetSelectOpions(sort);
    //                    queryableSet = SortOptions.Provider.DefaultSort(queryableSet);
    //                    break;

    //            }
    //        }

    //        List = queryableSet.Skip((PageNo - 1)*itemPerPage).Take(itemPerPage).ToList();


    //    }
    //}



    public class PageableData3<T> where T : class
    {
        protected static int ItemPerPageDefault = 15;

        public IEnumerable<T> List { get; set; }

        public int PageNo { get; set; }

        public int CountPage { get; set; }

        public int ItemPerPage { get; set; }
        public string CurrentSort { get; set; }
        public string NameSortParm { get; set; }
        public string PriceSortParm { get; set; }
        public string CategorySortParm { get; set; }
        public string CurrentFilter { get; set; }
        public string CategoryParm { get; set; }
       
        public string Category { get; set; }

        public PageableData3(IQueryable<T> queryableSet, int page, int itemPerPage,
          string currentSort,string nameSortParm,string priceSortParm,
          string categorySortParm,string currentFilter,string categoryParm) 
          {
            
            if (itemPerPage == 0)
            {
                itemPerPage = ItemPerPageDefault;
            }
            
            ItemPerPage = itemPerPage;
            CurrentSort = currentSort;
            NameSortParm = nameSortParm;
            PriceSortParm = priceSortParm;
            CategorySortParm = categorySortParm;
            CurrentFilter = currentFilter;
            CategoryParm = categoryParm;
            PageNo = page;
            var count = queryableSet.Count();
            CountPage = (int) decimal.Remainder(count, itemPerPage) == 0 ? count/itemPerPage : count/itemPerPage + 1;
            List = queryableSet.Skip((PageNo - 1)*itemPerPage).Take(itemPerPage).ToList();
        }




    }





















    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, int currentPage, int totalPages,
            Func<int, string> pageUrl)
        {
            StringBuilder builder = new StringBuilder();

            //Prev
            var prevBuilder = new TagBuilder("a");
            prevBuilder.InnerHtml = "&laquo;"; //закрывает тег а.
            if (currentPage == 1)
            {
                prevBuilder.MergeAttribute("href", pageUrl.Invoke(currentPage));
                builder.AppendLine("<li class=\"active\">" + prevBuilder.ToString() + "</li>");
            }
            else
            {
                prevBuilder.MergeAttribute("href", pageUrl.Invoke(currentPage - 1));
                builder.AppendLine("<li>" + prevBuilder.ToString() + "</li>");
            }
            //По порядку
            for (int i = 1; i <= totalPages; i++)
            {
                //Условие что выводим только необходимые номера
                if (((i <= 3) || (i > (totalPages - 3))) || ((i > (currentPage - 2)) && (i < (currentPage + 2))))
                {
                    var subBuilder = new TagBuilder("a");
                    subBuilder.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                    if (i == currentPage)
                    {
                        subBuilder.MergeAttribute("href", pageUrl.Invoke(i));
                        builder.AppendLine("<li class=\"active\">" + subBuilder.ToString() + "</li>");
                    }
                    else
                    {
                        subBuilder.MergeAttribute("href", pageUrl.Invoke(i));
                        builder.AppendLine("<li>" + subBuilder.ToString() + "</li>");
                    }
                }
                else if ((i == 4) && (currentPage > 5))
                {
                    //Троеточие первое
                    builder.AppendLine("<li class=\"disabled\"> <a href=\"#\">...</a> </li>");
                }
                else if ((i == (totalPages - 3)) && (currentPage < (totalPages - 4)))
                {
                    //Троеточие второе
                    builder.AppendLine("<li class=\"disabled\"> <a href=\"#\">...</a> </li>");
                }
            }
            //Next
            var nextBuilder = new TagBuilder("a");
            nextBuilder.InnerHtml = "&raquo;";
            if (currentPage == totalPages)
            {
                nextBuilder.MergeAttribute("href", pageUrl.Invoke(currentPage));
                builder.AppendLine("<li class=\"active\">" + nextBuilder.ToString() + "</li>");
            }
            else
            {
                nextBuilder.MergeAttribute("href", pageUrl.Invoke(currentPage + 1));
                builder.AppendLine("<li>" + nextBuilder.ToString() + "</li>");
            }
            return new MvcHtmlString("<ul class='pagination'>" + builder.ToString() + "</ul>");
        }





        public static MvcHtmlString SortOptionsLink(this HtmlHelper html, string curent,
            List<SelectListItem> listOptions,
            Func<string, string> pageUrl)
        {

            if (listOptions.Any(x => x.Selected) == false)
            {
                listOptions.First().Selected = true;
            }


            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("<div id='sortable' class=\"right_float\" >Сортировка");
            var selectListItem = listOptions.Single(x => x.Selected);
            builder.AppendFormat(
                "<span class='show_block_sortable'>{0}<span><img id='orderSelect' style='float: right' src='/Content/images/sortable.png'/></span></span>",
                selectListItem.Text);



            builder.AppendLine("<div class='li_block_sortable'>");
            for (int index = 0; index < listOptions.Count; index++)
            {
                var option = listOptions[index];
                if (option.Value == curent)
                {
                    builder.AppendFormat(
                        "<div><img src='/Content/images/sprite.png' style='float: left'/>{0}</div>", option.Text);
                }
                else
                {
                    var nextBuilder = new TagBuilder("a");
                    nextBuilder.InnerHtml = listOptions[index].Text;
                    nextBuilder.MergeAttribute("href", pageUrl.Invoke(listOptions[index].Value));
                    builder.AppendLine("<div>" + nextBuilder.ToString() + "</div>");
                }
            }
            builder.AppendLine("</div></div>");
            return new MvcHtmlString(builder.ToString());
        }

    }
}
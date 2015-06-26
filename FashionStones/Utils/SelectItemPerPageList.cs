

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FashionStones.Utils
{
    public class SelectItemPerPageList
    {
        public List<int> Options = new List<int>() { 8, 12, 16, 24 };

        public IEnumerable<SelectListItem> GetSelectListItems(int itemPerPage)
        {
            IEnumerable<SelectListItem> selectList =
                (from c in Options
                    select new SelectListItem
                    {
                        Selected = (c == itemPerPage),
                        Text = c.ToString(),
                        Value = c.ToString()
                    }).ToList();
            return selectList;
        }
    }
}
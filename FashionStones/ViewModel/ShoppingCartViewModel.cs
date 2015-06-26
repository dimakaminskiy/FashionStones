using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FashionStones.Models.Domain.Entities;

namespace FashionStones.ViewModel
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<Cart> CartItems { get; set; }
        public double CartTotal { get; set; }
    }
}
using FashionStones.Models.Domain.Entities;
using FashionStones.Models.Domain.Interfaces;

namespace FashionStones.Models
{
   public  class DataManager
    {

       public IGenericRepository<Coutry> Coutries { get; set; }
       public IGenericRepository<Stone> Stones { get; set; }
       public IGenericRepository<JewelPHoto> JewelPHotos { get; set; }
       public IGenericRepository<Material> Materials { get; set; }
       public IGenericRepository<Cover> Covers { get; set; }
       public IGenericRepository<Markup> Markups { get; set; }
       public IGenericRepository<Product> Products { get; set; }
       public IGenericRepository<Discount> Discounts { get; set; }
       public IGenericRepository<Category> Categories { get; set; }
       public IGenericRepository<Cart> Carts { get; set; }
       public IGenericRepository<Order> Orders { get; set; }
       public IGenericRepository<OrderDetail> OrderDetails { get; set; }
       public IGenericRepository<MethodOfDelivery> MethodOfDeliveries { get; set; }
       public IGenericRepository<MethodOfPayment> MethodOfPayments { get; set; }


       public DataManager
           (
           IGenericRepository<Coutry> coutries,
           IGenericRepository<Stone> stones,
           IGenericRepository<JewelPHoto> jewelPHotos,
           IGenericRepository<Material> materials,
           IGenericRepository<Cover> covers,
           IGenericRepository<Markup> markups,
           IGenericRepository<Product> products,
           IGenericRepository<Discount> discounts,
           IGenericRepository<Category> categories,
           IGenericRepository<Cart> carts,
           IGenericRepository<Order> orders,
           IGenericRepository<OrderDetail> orderDetails, 
           IGenericRepository<MethodOfDelivery> methodOfDeliveries,
           IGenericRepository<MethodOfPayment> methodOfPayments 
           )

       {
         
           Coutries = coutries;
           Stones = stones;
           JewelPHotos = jewelPHotos;
           Materials = materials;
           Covers = covers;
           Markups = markups;
           Products = products;
           Discounts = discounts;
           Categories = categories;
           Carts = carts;
           Orders = orders;
           OrderDetails = orderDetails;
           MethodOfDeliveries = methodOfDeliveries;
           MethodOfPayments = methodOfPayments;
       }
    }
}
       

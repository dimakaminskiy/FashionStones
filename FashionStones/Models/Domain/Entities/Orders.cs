using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashionStones.App_LocalResources;
using WebGrease;


namespace FashionStones.Models.Domain.Entities
{
    public partial class Order
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        [Display(ResourceType = typeof(GlobalResource), Name = "OrderDataTime")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy  HH:mm}",
               ApplyFormatInEditMode = true)]
        public DateTime? OrderDate { get; set; }

        
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserFirstName", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserFirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserLastName", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserLastName")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserMiddleName", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserMiddleName")]
        public string MiddleName { get; set; }

        
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserPhoneNumber", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserPhone")]
        public string Phone { get; set; }
        [StringLength(160)]

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserEmail", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserEmail")]
        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessageResourceType = typeof(GlobalResource),ErrorMessageResourceName = "ErrorMessageRegularExpressionEmail")]
        public string Email { get; set; }


        [NotMapped]
        public string TotalPriceString
        {
            get
            {
                return  Total.HasValue ?(Total.Value).ToString("F2"):0.ToString();
             }
            set
            {
                try
                {
                    string buf = value;
                    if (buf.Contains("."))
                    {
                       buf= buf.Replace(".", ",");
                    }
                    Total = double.Parse(buf);
                }
                catch (Exception)
                {
                    Total = 0;
                }
            }

        }


        [Display(ResourceType = typeof(GlobalResource), Name = "OrderTotalCount")]
        public double? Total { get; set; }
        [Display(ResourceType = typeof(GlobalResource), Name = "OrderCountry")]
        public string Country { get; set; }
        [Display(ResourceType = typeof(GlobalResource), Name = "OrderCity")]
        public string City { get; set; }
        [Display(ResourceType = typeof(GlobalResource), Name = "OrderTextInfo")]
        [DataType(DataType.MultilineText)]
        public string TextInfo { get; set; }
        
        public int OrderStatusId { get; set; }
        [Display(ResourceType = typeof(GlobalResource), Name = "OrderMethodOfPaymentId")]
        public int? MethodOfPaymentId { get; set; }
        [Display(ResourceType = typeof(GlobalResource), Name = "OrderMethodOfDeliveryId")]
        public int MethodOfDeliveryId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual MethodOfDelivery MethodOfDelivery { get; set; }
        public virtual MethodOfPayment MethodOfPayment { get; set; }
    }


        public partial class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
            [DataType(DataType.Text)]
        public double UnitPrice { get; set; }

        [NotMapped]
        public string UnitPriceString
        {
            get
            {
                return UnitPrice.ToString("F2");  
            }
            set
            {
                try
                {
                    string buf = value;
                    if (buf.Contains("."))
                    {
                        buf = buf.Replace(".", ",");
                    }
                    UnitPrice = double.Parse(buf);
            }
                catch (Exception)
                {
                    UnitPrice = 0;
                }
            }

        }

        [NotMapped]
        public string UnitTotalProce
        {
            get { return(Quantity*UnitPrice).ToString("F2"); }

        }


        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        }

        public partial class OrderStatus
    {
        public OrderStatus()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

        public class MethodOfDelivery
        {
            public int Id { get; set; }
            [Display(ResourceType = typeof(GlobalResource), Name = "MethodOfDeliveryName")]
            public string Name { get; set; }
            public virtual ICollection<Order> Orders { get; set; }
        }

        public class MethodOfPayment
        {
            public int Id { get; set; }
            [Display(ResourceType = typeof(GlobalResource), Name = "MethodOfPaymentName")]
            public string Name { get; set; }
            public virtual ICollection<Order> Orders { get; set; }
        }

}



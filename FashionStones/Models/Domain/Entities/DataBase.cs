using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using FashionStones.App_LocalResources;


namespace FashionStones.Models.Domain.Entities
{

    public class Discount
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(GlobalResource), Name = "DiscountName")]
        public string Name { get; set; }
        [Display(ResourceType = typeof(GlobalResource), Name = "DiscountValue")]
        public int Value { get; set; }
    }
    public class Coutry
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(GlobalResource), Name = "CoutryName")]
        public string Name { get; set; }
    }
    public class Markup // наценка
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredJewelName",
            ErrorMessageResourceType = typeof (GlobalResource))]
        [Display(ResourceType = typeof (GlobalResource), Name = "ProductName")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredRetaulMarkup",
            ErrorMessageResourceType = typeof (GlobalResource))]
        [Display(ResourceType = typeof (GlobalResource), Name = "MarkupRetailMarkup")]
        public int RetailMarkup { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredTradeMarkup",
            ErrorMessageResourceType = typeof (GlobalResource))]
        [Display(ResourceType = typeof (GlobalResource), Name = "MarkupTradeMarkup")]
        public int TradeMarkup { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(GlobalResource), Name = "CategoryName")]
        public string Name { get; set; }

         public string TranslitName()
        {
            string str = Name.ToLower();
            string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", "\"", "y","" /*"'"*/, "e", "yu", "ya" };
            string[] rus_low = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            for (int i = 0; i <= 32; i++)
            {
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return char.ToUpper(str[0]) + str.Substring(1);
        }

    }
    public class Product
    {
        
        public int Id { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        [Display(ResourceType = typeof (GlobalResource), Name = "ProductName")]
        public string Name
        {
            get
            {
                StringBuilder builder = new StringBuilder();
              
                if (Category != null && Category.Name != "")
                {
                    builder.Append(" " + Category.Name);
                }
                if (Stone != null && Stone.Name != "")
                {
                    builder.Append(" " + Stone.Name);
                }

                else if (Material != null && Material.Name != "")
                {
                    builder.Append(" " + Material.Name);
                }

                else if (!string.IsNullOrEmpty(Diameter))
                {
                    builder.Append(" " + Diameter);
                }
                else if (!string.IsNullOrEmpty(Weight))
                {
                    builder.Append(" " + Weight);
                }
                else if (!string.IsNullOrEmpty(Lenght))
                {
                    builder.Append(" " + Lenght);

                }
                return builder.ToString(); ;
            }
        }

        [Display(ResourceType = typeof(GlobalResource), Name = "ProductCategoryId")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Display(ResourceType = typeof (GlobalResource), Name = "ProductSize")]
        public string Size { get; set; }

        [Display(ResourceType = typeof (GlobalResource), Name = "ProductLenght")]
        public string Lenght { get; set; } // dlina

        [Display(ResourceType = typeof (GlobalResource), Name = "ProductWeight")]
        public string Weight { get; set; } // vec

        [Display(ResourceType = typeof (GlobalResource), Name = "ProductDiameter")]
        public string Diameter { get; set; } //диаметр

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredJewelPrice",
            ErrorMessageResourceType = typeof (GlobalResource))]
        [Display(ResourceType = typeof (GlobalResource), Name = "ProductPrice")]
        public double ShoppingPrice { get; set; } // цена

        [Display(ResourceType = typeof(GlobalResource), Name = "ProductDiscountId")]
        public int? DiscountId { get; set; }
        public virtual Discount Discount { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateOfPublishing { get; set; }

        [Display(ResourceType = typeof(GlobalResource), Name = "ProductMaterialId")]
        public int? MaterialId { get; set; } // mater
        public virtual Material Material { get; set; }
        
        [Display(ResourceType = typeof(GlobalResource), Name = "ProductCover")]
        public int? CoverId { get; set; } // покрытие
        public virtual Cover Cover { get; set; }
        
        [Display(ResourceType = typeof(GlobalResource), Name = "ProductStone")]
        public int? StoneId { get; set; } // камень
        public virtual Stone Stone { get; set; }

        [Display(ResourceType = typeof (GlobalResource), Name = "ProductPHotoId")]
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredJewelPhoto",
            ErrorMessageResourceType = typeof (GlobalResource))]

        public int JewelPHotoId { get; set; } //картинки
        public virtual JewelPHoto JewelPHoto { get; set; }

        [Display(ResourceType = typeof (GlobalResource), Name = "ProductMarkupId")]
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredJewelMarkup",
            ErrorMessageResourceType = typeof (GlobalResource))]
        public int? MarkupId { get; set; } // наценка
        public virtual Markup Markup { get; set; }

        [Display(ResourceType = typeof (GlobalResource), Name = "ProductCount")]
        public int Count { get; set; } // количество
         [Display(ResourceType = typeof(GlobalResource), Name = "ProductQuantityInStock")]
        public int QuantityInStock { get; set; } // количество в наличае
        /*------------------------------------------------------------*/

        [NotMapped]
        [Display(ResourceType = typeof (GlobalResource), Name = "ProductRetailPrice")]
        public string RetailPrice
        {
            get { return MarkupId == null ? "" : (ShoppingPrice + (ShoppingPrice*Markup.RetailMarkup/100)).ToString("F2"); }
        }

        [NotMapped]
        [Display(ResourceType = typeof (GlobalResource), Name = "ProductTradePrice")]
        public string TradePrice
        {
            get { return MarkupId == null ? "" : (ShoppingPrice + (ShoppingPrice*Markup.TradeMarkup/100)).ToString("F2"); }
        }

        [NotMapped]
        [ScaffoldColumn(false)]
        public string FullName
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(Id);
                builder.Append(" " + Name);
                return builder.ToString();
            }
        }
        
        [Display(ResourceType = typeof(GlobalResource), Name = "ProductDateOfPublishing")]
        public string DateOfPublishString{ 
            get { return DateOfPublishing.ToString("dd'.'MM'.'yyyy", CultureInfo.InvariantCulture); }
            set
            {
                try
                {
                    DateOfPublishing= DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    DateOfPublishing = DateTime.Now;
                }
            }
        }


        [NotMapped]
        [ScaffoldColumn(false)]
        public static string DateFormat
        {
            get { return "dd'.'MM'.'yyyy"; }
        }



    }
    public class Material
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredJewelMaterial",
            ErrorMessageResourceType = typeof (GlobalResource))]
        [Display(ResourceType = typeof (GlobalResource), Name = "ProductName")]
        public string Name { get; set; }
    }
    public class Cover
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredJewelName",
            ErrorMessageResourceType = typeof (GlobalResource))]
        [Display(ResourceType = typeof (GlobalResource), Name = "ProductCover")]
        public string Name { get; set; }
    }
    public class Stone //камень
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredStoneName",
            ErrorMessageResourceType = typeof (GlobalResource))]
        [Display(ResourceType = typeof (GlobalResource), Name = "ProductName")]
        public string Name { get; set; }

        public string TranslitName()
        {
            string str = Name.ToLower();
            string[] lat_low =
            {
                "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p",
                "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", "\"", "y", "'", "e", "yu", "ya"
            };
            string[] rus_low =
            {
                "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р",
                "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я"
            };
            for (int i = 0; i <= 32; i++)
            {
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return char.ToUpper(str[0]) + str.Substring(1);
        }
        public virtual ICollection<Product> Products { get; set; }
        
    }
    public class JewelPHoto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredJewelPhotoCaption",
            ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "JewelPhotoCaption")]
        public string Caption { get; set; }

        [NotMapped]
        public string PathToSmall
        {
            get { return PathToFolder + "/" + "small/" + Name; }
        }

        [NotMapped]
        public string PathToBig
        {
            get
            {
                return PathToFolder + "/" + "big/" + Name;
                ;
            }
        }

        [NotMapped]
        private static string PathToFolder
        {
            get { return "/Content/images/jewelPHoto"; }
        }
    }
    public partial class Cart
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public DateTime DateCreated { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public string TotalPrice { get { return String.Format("{0:0.00}", (Count*Price)); } }
        public virtual Product Product { get; set; }
      
    }


}
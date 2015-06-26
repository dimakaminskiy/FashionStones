using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FashionStones.App_LocalResources;


namespace FashionStones.ViewModel
{
    public class InformPaymentViewModel
    {
      //  [Required(ErrorMessage = "Введите номер заказа")]


        [Display(ResourceType = typeof(GlobalResource), Name = "InformPaymentViewModelOrderId")]
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredJInformPaymentViewModelOrderId",
            ErrorMessageResourceType = typeof(GlobalResource))]
        public string OrderId { get; set; }


        [Display(ResourceType = typeof(GlobalResource), Name = "InformPaymentViewModelPhoneNumber")]
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredJInformPaymentViewModelPhoneNumber",
            ErrorMessageResourceType = typeof(GlobalResource))]
        public string PhoneNumber { get; set; }


        [Display(ResourceType = typeof(GlobalResource), Name = "InformPaymentViewModelTotal")]
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredJInformPaymentViewModelTotal",
            ErrorMessageResourceType = typeof(GlobalResource))]
        
        public string Total { get; set; }


        [Display(ResourceType = typeof(GlobalResource), Name = "InformPaymentViewModelTimePay")]
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredJInformPaymentViewModelTimePay",
            ErrorMessageResourceType = typeof(GlobalResource))]

      
        public string TimePay { get; set; }
    }
}
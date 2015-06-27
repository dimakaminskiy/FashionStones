using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FashionStones.Models.Domain.Entities;

namespace FashionStones.ViewModel
{
    public class HelpViewModel
    {
        [Required(ErrorMessage = "Введите Ф.И.О")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Введите E-mail")]
        [EmailAddress(ErrorMessage = "Неверный E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите текст сообщения")]
        public string Text { get; set; }
    }

    public class OrderViewModel
    {
        public Order order { get; set; }
        public List<OrderDetail>  orderDetails { get; set; }
    }   
}
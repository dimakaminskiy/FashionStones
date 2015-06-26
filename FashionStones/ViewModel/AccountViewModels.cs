using System.ComponentModel.DataAnnotations;
using FashionStones.App_LocalResources;

namespace FashionStones.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserLastName", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserLastName")]
        public string LastName { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserFirstName", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserFirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserMiddleName", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserMiddleName")]
        public string MiddleName { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserCoutry", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserCoutrry")]
        public string CountryId { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserCity", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserCity")]
        public string City { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserPhone", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserPhone")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserEmail", ErrorMessageResourceType = typeof(GlobalResource))]
        [EmailAddress]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserEmail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserPassword", ErrorMessageResourceType = typeof(GlobalResource))]
        [StringLength(100, ErrorMessage = "Минимальная длина пароля {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserPassword")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserPasswordRepeat")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Введенные пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel

    {
      public  LoginViewModel()
        {
            RememberMe = true;
        }
        [Required(ErrorMessage = "Введите Ник (логин)")]
        [Display(Name = "Введите логин или e-mail")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    public class ManageUserEmailViewModel
    {
        [Display(Name = "Текущий E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Новый E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string NewEmail { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
    public class ManageUserViewModel
    {
        [Required(ErrorMessage = "Введите текущий пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль")]
        [StringLength(100, ErrorMessage = "Минимальная длина пароля {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("NewPassword", ErrorMessage = "Введенные пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
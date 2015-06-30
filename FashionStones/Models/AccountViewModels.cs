using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FashionStones.App_LocalResources;


namespace FashionStones.Models
{

    public class ForgotViewModel
    {
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserEmail", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserEmail")]
        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "ErrorMessageRegularExpressionEmail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        public LoginViewModel()
        {
            RememberMe = true;
        }
        [Required(ErrorMessage = "Введите телефон или e-mail")]
        [Display(Name = "Телефон или e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserPassword", ErrorMessageResourceType = typeof(GlobalResource))]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserPassword")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }


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
        public int CountryId { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserCity", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserCity")]
        public string City { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserPhone", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserPhone")]
        [RegularExpression(@"^\+380[0-9]{9}$", ErrorMessage = "Неверный формат")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserEmail", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserEmail")]
        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "ErrorMessageRegularExpressionEmail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserPassword", ErrorMessageResourceType = typeof(GlobalResource))]
        [StringLength(100, ErrorMessage = "Минимальная длина пароля {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserPassword")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserPasswordRepeat")]
        [Compare("Password", ErrorMessage = "Введенные пароли не совпадают")]
        public string ConfirmPassword { get; set; }
        [Display(ResourceType = typeof(GlobalResource), Name = "UserKindOfActivity")]
        public string KindOfActivity { get; set; }
    }


    public class ResetPasswordViewModel
    {
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserEmail", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserEmail")]
        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "ErrorMessageRegularExpressionEmail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserPassword", ErrorMessageResourceType = typeof(GlobalResource))]
        [StringLength(100, ErrorMessage = "Минимальная длина пароля {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserPassword")]
       
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserPasswordRepeat")]
        [Compare("Password", ErrorMessage = "Введенные пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessageResourceName = "ErrorMessageRequiredUserEmail", ErrorMessageResourceType = typeof(GlobalResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "UserEmail")]
        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "ErrorMessageRegularExpressionEmail")]
        public string Email { get; set; }
    }
}

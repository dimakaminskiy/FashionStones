using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FashionStones.App_LocalResources;


namespace FashionStones.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ввидите телефон или e-mail")]
        [Display(Name = "Телефон или e-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
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


    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

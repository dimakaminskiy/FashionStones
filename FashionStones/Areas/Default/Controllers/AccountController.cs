﻿using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FashionStones.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FashionStones.Models;
using FashionStones.Utils;

namespace FashionStones.Areas.Default.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {

        private string GetRegisterMessge(string link,string code)
        {
                // var settings = new FashionStones.Utils.EmailSettings();
                    var tb = new TagBuilder("a");
                    tb.MergeAttribute("href", link);
                    tb.SetInnerText(link);
                    tb.ToString(TagRenderMode.SelfClosing);

                    var callBack = new TagBuilder("a");
                    callBack.MergeAttribute("href", code);
                    callBack.SetInnerText("ссылке");
                    callBack.ToString(TagRenderMode.SelfClosing);


           return string.Format("Регистрация на сайте {0}" + "{2}" +
                          "Здравствуйте! {2}" +
                           "Поздравляем, Вы зарегистрировались на сайте {0} {2}" +
                           "Для завершения регистрации, перейдите, пожалуйста, по этой {1}.{2}" +
                            "С уважением, команда {3}", link, callBack, "<br/>", tb);
        }

        #region ctor
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public AccountController(DataManager dataManager) :base (dataManager)
        {
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set
            {
                _userManager = value;
            }
        }
        #endregion
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        #region Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailOrPhoneAsync(model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Неверное имя пользователя или пароль");
                return (View(model));
            }
            if (user.LockoutEnabled)
            {
                ModelState.AddModelError("", "Ваш аккаунт заблокирован");
                return View(model);
            }
            if (user.EmailConfirmed == false)
            {
                ModelState.AddModelError("", "Пожалуйста, подтвердите Ваш E-mail");
                return View(model);
            }

            var result =
                await
                    SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe,
                        shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        #endregion



 

        [ChildActionOnly]
        public ActionResult UserProfile()
        {
          var id = CurrentUserId;
          var user = UserManager.FindById(id);
            return View(user);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.CountryId = new SelectList(DataManager.Coutries.GetAll().ToList(), "Id", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (UserManager.Users.Any(t => t.Email == model.Email))
                {
                    ModelState.AddModelError("", "Пользователь с таким email уже зарегистрирован");
                    ViewBag.CountryId = new SelectList(DataManager.Coutries.GetAll().ToList(), "Id", "Name", model.CountryId);
                    return View(model);
                }
                if (UserManager.Users.Any(t => t.PhoneNumber == model.Phone))
                {
                    ModelState.AddModelError("", "Пользователь с таким телефоном уже зарегистрирован");
                    ViewBag.CountryId = new SelectList(DataManager.Coutries.GetAll().ToList(), "Id", "Name", model.CountryId);
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    City = model.City,
                    CountryId = model.CountryId,
                    PhoneNumber = model.Phone
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    EmailSettings settings = new EmailSettings();
//                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                     var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                     await UserManager.SendEmailAsync(user.Id, "Регистрация " + settings.Link, GetRegisterMessge(settings.Link, callbackUrl));
                    return View("MustConfirmEmail", model);
                }
                AddErrors(result);
            }
            ViewBag.CountryId = new SelectList(DataManager.Coutries.GetAll().ToList(), "Id", "Name",model.CountryId);
          return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailOrPhoneAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Пользователь с таким e-mail не зарегистрирован.");
                    return View(model);
                }

                EmailSettings settings= new EmailSettings();
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                 await UserManager.SendEmailAsync(user.Id, "Восстановление пароля",
                    "Здравствуйте! <br/>Вы отправили запрос на восстановление пароля от аккаунта " + user.Email +
                    " .<br/>" +
                    "Для того чтобы задать новый пароль, перейдите по  <a href=\"" + callbackUrl + "\">ссылке</a>" +
                    "С уважением, команда <a href=\"" + settings.Link + "\">"+settings.Link+"</a>");
                 return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

  

   

   

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "About");
        }

      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "About");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
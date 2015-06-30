using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using FashionStones.Models;
using FashionStones.Utils;
using FashionStones.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FashionStones.Areas.Default.Controllers
{
    public class AboutController : Controller
    {

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        private ApplicationUserManager _userManager;



        // GET: Default/About
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult Warranty()
        //{
        //    return View();
        //}

        public ActionResult Delivery()
        {
            return View();
        }

        public ActionResult Discounts()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Contacts()
        {

            HelpViewModel model;
            if (User.Identity.IsAuthenticated)
            {
                var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
                model = new HelpViewModel
                {
                 //   FullName = user.LastName + " " + user.FirstName + " " + user.MiddleName,
                    Email = user.Email,
                    Text = ""
                };
            }
            else
            {
                model = new HelpViewModel();
            }
            return View(model);
        }



        [HttpPost]
        public ActionResult Contacts(HelpViewModel model)
        {
            if (ModelState.IsValid)
            {
                EmailSettings settings = new EmailSettings();
                SmtpClient smtp = new SmtpClient();
                smtp.Host = settings.ServerName;
                smtp.Port = settings.ServerPort;
                smtp.EnableSsl = settings.UseSsl;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(settings.MailFromAddress, settings.password);

                using (var msg = new MailMessage(settings.MailFromAddress, settings.MailFromAddress))
                {
                    string message = string.Format("От {0}<br>E-mail {1}<br>Сообщение {2}<br>",
                        model.FullName, model.Email, model.Text);
                    msg.Subject = "Вопрос-Ответ"; //message;
                    msg.IsBodyHtml = true;
                    msg.Body = message;
                    try
                    {
                        smtp.Send(msg);
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                }
                return View("HelpConfirm", "", model);
            }
            return View(model);
        }
    }
}
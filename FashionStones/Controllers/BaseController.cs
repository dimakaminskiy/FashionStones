using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionStones.Models;
using Microsoft.AspNet.Identity;

namespace FashionStones.Controllers
{
    public class BaseController : Controller
    {
        protected DataManager DataManager { get; set; }

        public BaseController(DataManager dataManager)
        {
            DataManager = dataManager;
        }

        protected string CurrentUserId
        {
            get { return User.Identity.GetUserId(); }
        }
    }



}
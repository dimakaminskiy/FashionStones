using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FashionStones.Models;
using Microsoft.AspNet.Identity;

namespace FashionStones.Utils
{
    public static class IdentityExtensions
    {
        public static async Task<ApplicationUser> FindByEmailOrPhoneAsync
            (this UserManager<ApplicationUser> userManager, string userEmailOrPhone, string password)
        {
            var username = userEmailOrPhone;
            if (userEmailOrPhone.Contains("@"))
            {
               return  await userManager.FindByEmailAsync(username);
            }
           return await userManager.FindAsync(username, password);
         }
    }
}
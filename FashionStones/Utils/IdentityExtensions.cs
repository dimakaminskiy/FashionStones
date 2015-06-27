using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FashionStones.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace FashionStones.Utils
{
    public static class IdentityExtensions
    {
        public static async Task<ApplicationUser> FindByEmailOrPhoneAsync
            (this UserManager<ApplicationUser> userManager, string userEmailOrPhone, string password)
        {
            if (userEmailOrPhone.Contains("@"))
            {
                return await userManager.FindAsync(userEmailOrPhone, password);
            }
            userEmailOrPhone = userManager.FindUserNameByPhoneAsync(userEmailOrPhone);
            if (string.IsNullOrEmpty(userEmailOrPhone)) return null;
            return await userManager.FindAsync(userEmailOrPhone, password);
        }

        public static string FindUserNameByPhoneAsync
            (this UserManager<ApplicationUser> userManager, string userPhone)
        {
            var user = userManager.Users.SingleOrDefault(t => t.PhoneNumber == userPhone);
            if (user != null)
            {
                return user.UserName;
            }
            return null;
        }

        public static async Task<ApplicationUser> FindByEmailOrPhoneAsync
           (this UserManager<ApplicationUser> userManager, string userEmailOrPhone)
        {
            if (userEmailOrPhone.Contains("@"))
            {
                return await userManager.FindByEmailAsync(userEmailOrPhone);
            }
            userEmailOrPhone = userManager.FindUserNameByPhoneAsync(userEmailOrPhone);
            return await userManager.FindByNameAsync(userEmailOrPhone);
        }



    }
}
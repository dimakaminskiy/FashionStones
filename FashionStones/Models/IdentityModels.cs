using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using FashionStones.Models.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FashionStones.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaim(new Claim(ClaimTypes.Email, this.Email));
         //   userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, this.Id));

            return userIdentity;
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<MethodOfPayment> MethodOfPayments { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<MethodOfDelivery> MethodOfDeliveries { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Markup> Markups { get; set; }
        public virtual DbSet<Cover> Covers { get; set; }
        public virtual DbSet<JewelPHoto> JewelPHotos { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Coutry> Coutries { get; set; }
        public virtual DbSet<Stone> Stones { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; } 
    }
}
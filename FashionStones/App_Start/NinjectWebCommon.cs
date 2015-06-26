using FashionStones.Models;
using FashionStones.Models.Domain.Entities;
using FashionStones.Models.Domain.Implementations;
using FashionStones.Models.Domain.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FashionStones.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FashionStones.App_Start.NinjectWebCommon), "Stop")]

namespace FashionStones.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IUserStore<>)).To(typeof(UserStore<>));
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            kernel.Bind(typeof(UserManager<>)).ToSelf(); // add scoping as necessary
            kernel.Bind(typeof(UserStore<>)).ToSelf(); // add scoping as necessary


            


            kernel.Bind<IGenericRepository<MethodOfPayment>>().To<GenericRepository<MethodOfPayment>>();
            kernel.Bind<IGenericRepository<MethodOfDelivery>>().To<GenericRepository<MethodOfDelivery>>();
            kernel.Bind<IGenericRepository<Cart>>().To<GenericRepository<Cart>>();
            kernel.Bind<IGenericRepository<OrderDetail>>().To<GenericRepository<OrderDetail>>();
            kernel.Bind<IGenericRepository<Order>>().To<GenericRepository<Order>>();
            kernel.Bind<IGenericRepository<Product>>().To<GenericRepository<Product>>();
            kernel.Bind<IGenericRepository<Category>>().To<GenericRepository<Category>>();
            kernel.Bind<IGenericRepository<Discount>>().To<GenericRepository<Discount>>();
            kernel.Bind<IGenericRepository<Markup>>().To<GenericRepository<Markup>>();
            kernel.Bind<IGenericRepository<Cover>>().To<GenericRepository<Cover>>();
            kernel.Bind<IGenericRepository<JewelPHoto>>().To<GenericRepository<JewelPHoto>>();
            kernel.Bind<IGenericRepository<Material>>().To<GenericRepository<Material>>();
            kernel.Bind<IGenericRepository<Coutry>>().To<GenericRepository<Coutry>>();
            kernel.Bind<IGenericRepository<Stone>>().To<GenericRepository<Stone>>();
        }        
    }
}

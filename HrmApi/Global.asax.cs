using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Lifetime;


namespace HrmApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //public static void Register(HttpConfiguration config)
        //{
        //    // Create Unity container
        //    var container = new UnityContainer();

        //    // Register your dependencies
        //    container.RegisterType<INotificationService, NotificationService>(new HierarchicalLifetimeManager());
        //    //container.RegisterType<BookingsMasterDAL>(new HierarchicalLifetimeManager());

        //    // Set the Dependency Resolver for Web API to use Unity
        //    //config.DependencyResolver = new UnityDependencyResolver(container);

        //    // Other Web API configuration...
        //}
    }
}

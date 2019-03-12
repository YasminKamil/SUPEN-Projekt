using SUPEN_Projekt.Models;
using SUPEN_Projekt.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace SUPEN_Projekt
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<BookingRepository>(new InjectionConstructor(typeof(ApplicationDbContext)));
            container.RegisterType<IBookingRepository, BookingRepository>();
            container.RegisterType<BookingSystemRepository>(new InjectionConstructor(typeof(ApplicationDbContext)));
            container.RegisterType<IBookingSystemRepository, BookingSystemRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
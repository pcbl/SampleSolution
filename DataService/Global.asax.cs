using System;
using Autofac;
using Autofac.Integration.Wcf;
using DataRepository;

namespace DataService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            // Register your service implementations.
            builder.RegisterType<ProductService>();

            //Registering all implementations of IRepository<>
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(IRepository<>)).AsImplementedInterfaces();

            // Set the dependency resolver.
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }
    }
}
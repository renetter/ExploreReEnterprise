using System;
using System.IO;
using System.Web;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using Moq;
using ReEnterprise.Infrastructure.NHibernate;
using ReEnterprise.Infrastructure.NHibernate.Interface;
using ReEnterprise.Infrastructure.Service.Interceptor;
using ReEnterprise.Infrastructure.Service.UserManagement;
using ReEnterprise.Core.Interface;

namespace ReEnterprise.Infrastructure.Service
{
    public class Global : HttpApplication
    {
        private const string Bin = "Bin";

        protected void Application_Start(object sender, EventArgs e)
        {
            IWindsorContainer container = new WindsorContainer();

            // Initialize the assembly directory for automatic register
            var filter = new AssemblyFilter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Bin));

            container.Install(FromAssembly.InDirectory(filter));

            container.Register(
                Component.For<ISessionFactoryManager>().ImplementedBy<SessionFactoryManager>().LifeStyle.PerWcfOperation
                    ());

            container.Register(Component.For<ILogger>().Instance(new Mock<ILogger>().Object));
            container.Register(Component.For<ServiceInterceptor>().LifeStyle.Transient);

            container.AddFacility<WcfFacility>().Register(
                Component.For<ISecurityService>().ImplementedBy<SecurityService>());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}
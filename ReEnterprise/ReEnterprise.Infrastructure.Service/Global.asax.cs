using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Castle.Windsor;
using ReEnterprise.Core;
using Microsoft.Practices.ServiceLocation;
using CommonServiceLocator.WindsorAdapter;
using Castle.MicroKernel.Registration;
using System.IO;
using Castle.MicroKernel.Releasers;
using Castle.Facilities.WcfIntegration;
using ReEnterprise.Infrastructure.Service.UserManagement;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using Moq;
using System.Reflection;
using System.ServiceModel;
using ReEnterprise.Infrastructure.NHibernate.Interface;
using ReEnterprise.Infrastructure.NHibernate;
using Castle.Windsor.Installer;

namespace ReEnterprise.Infrastructure.Service
{
    public class Global : System.Web.HttpApplication
    {
        private const string Bin = "Bin";
        private const string Service = "Service";
        private const string Repository = "Repository";
        private const string Validator = "Validator";
        private const string Castle = "Castle";

        protected void Application_Start(object sender, EventArgs e)
        {
            IWindsorContainer container = new WindsorContainer();

            // Initialize the assembly directory for automatic register
            AssemblyFilter filter = new AssemblyFilter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Bin));

            container.Install(FromAssembly.InDirectory(filter));

            container.Register(Component.For<ISessionFactoryManager>().ImplementedBy<SessionFactoryManager>().LifeStyle.PerWcfOperation());

            // Register all dependecy
            //container.Register(
            //    AllTypes.FromAssemblyInDirectory(filter)
            //        .Where(c => c.Assembly.FullName != Assembly.GetExecutingAssembly().FullName && !c.Assembly.FullName.Contains(Castle)
            //                    && (c.Name.Contains(Service) || c.Name.Contains(Repository) || (c.Name.Contains(Validator)))
            //        )
            //        .WithService.AllInterfaces()
            //        .LifestyleTransient()
            //        );

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
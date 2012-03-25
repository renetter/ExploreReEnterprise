using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using ReEnterprise.Domain.UserManagement.Contract.Repository;

namespace ReEnterprise.Infrastructure.UserManagement.NHibernate
{
    public class UserManagementHibernateInstaller: IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifeStyle.Transient);
            container.Register(Component.For<IPasswordPolicyRepository>().ImplementedBy<PasswordPolicyRepository>());
        }
    }
}

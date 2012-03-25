using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ReEnterprise.Domain.UserManagement.Contract.Repository;

namespace ReEnterprise.Infrastructure.UserManagement.NHibernate
{
    public class UserManagementHibernateInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifeStyle.Transient);
            container.Register(Component.For<IPasswordPolicyRepository>().ImplementedBy<PasswordPolicyRepository>());
        }

        #endregion
    }
}
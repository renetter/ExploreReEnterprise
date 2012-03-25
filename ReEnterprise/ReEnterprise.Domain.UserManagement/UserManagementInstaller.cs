using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ReEnterprise.Domain.UserManagement.Contract.Service;
using ReEnterprise.Domain.UserManagement.Contract.Validator;
using ReEnterprise.Domain.UserManagement.Service;
using ReEnterprise.Domain.UserManagement.Validator;

namespace ReEnterprise.Domain.UserManagement
{
    public class UserManagementInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUserManagementService>().ImplementedBy<UserManagementService>().LifeStyle.Transient);
            container.Register(
                Component.For<IPasswordPolicyRuleValidator>().ImplementedBy<PasswordPolicyRuleValidator>().LifeStyle.
                    Transient);
        }

        #endregion
    }
}
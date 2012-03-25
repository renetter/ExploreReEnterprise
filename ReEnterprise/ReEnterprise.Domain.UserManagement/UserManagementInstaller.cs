using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using ReEnterprise.Domain.UserManagement.Contract.Service;
using ReEnterprise.Domain.UserManagement.Contract.Validator;
using ReEnterprise.Domain.UserManagement.Validator;

namespace ReEnterprise.Domain.UserManagement
{
    public class UserManagementInstaller: IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IUserManagementService>().ImplementedBy<UserManagementService>().LifeStyle.Transient);
            container.Register(Component.For<IPasswordPolicyRuleValidator>().ImplementedBy<PasswordPolicyRuleValidator>().LifeStyle.Transient);
        }
    }
}

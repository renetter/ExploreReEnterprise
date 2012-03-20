using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using ReEnterprise.Core.Interface;
using FluentValidation;
using FluentValidation.Attributes;
using ReEnterprise.Core.Resources;

namespace ReEnterprise.Core
{
    /// <summary>
    /// Installer class to register all the core compoenent to windwsor container.
    /// </summary>
    public class CoreWindsorInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For(typeof(ModelValidator<>)).LifeStyle.Transient);
            container.Register(Component.For<IBusinessRulesValidator>().ImplementedBy<BusinessRulesValidator>().LifeStyle.Transient);
            container.Register(Component.For<IValidatorFactory>().ImplementedBy<AttributedValidatorFactory>().LifeStyle.Singleton);

            ValidatorOptions.ResourceProviderType = typeof(CoreResources);
        }
    }
}

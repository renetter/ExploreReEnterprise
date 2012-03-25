using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;
using FluentValidation.Attributes;
using ReEnterprise.Core.Generic;
using ReEnterprise.Core.Interface;
using ReEnterprise.Core.Resources;

namespace ReEnterprise.Core
{
    /// <summary>
    /// Installer class to register all the core compoenent to windwsor container.
    /// </summary>
    public class CoreInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof (IRuleValidator<>)).ImplementedBy(typeof (ModelValidator<>)).LifeStyle.Transient);
            container.Register(
                Component.For<IBusinessRulesValidator>().ImplementedBy<BusinessRulesValidator>().LifeStyle.Transient);
            container.Register(
                Component.For<IValidatorFactory>().ImplementedBy<AttributedValidatorFactory>().LifeStyle.Singleton);

            ValidatorOptions.ResourceProviderType = typeof (CoreResources);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReEnterprise.Core.Tests.Resources;
using ReEnterprise.Core.Resources;
using ReEnterprise.Core.Interface;
using ReEnterprise.Core.Generic;
using FluentValidation;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Microsoft.Practices.ServiceLocation;
using CommonServiceLocator.WindsorAdapter;
using FluentValidation.Attributes;

namespace ReEnterprise.Core.Tests
{
    [TestClass]
    public class BusinessRuleValidatorTest
    {
        private class TestModelValidator : AbstractValidator<TestModel>
        {
            public TestModelValidator()
            {
                RuleFor(c => c.Id).NotEmpty().Length(36).WithLocalizedName(() => TestModelResources.Id);
                RuleFor(c => c.Name).NotEmpty().WithLocalizedName(() => TestModelResources.Name);
                RuleFor(c => c.Age).InclusiveBetween(20, 50).WithLocalizedName(() => TestModelResources.Age);
            }
        }

        [Validator(typeof(TestModelValidator))]
        private class TestModel
        {            
            public string Id { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }
        }

        [TestInitialize]
        public void Setup()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(new CoreWindsorInstaller());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));            
        }

        [TestMethod]
        public void Invalid_Data_Should_Return_Validation_Messages()
        {
            TestModel model = new TestModel();

            IBusinessRulesValidator businessRuleValidator = new BusinessRulesValidator();

            businessRuleValidator.CreateValidator<ModelValidator<TestModel>, TestModel>(model);

            var validationResult = businessRuleValidator.Validate();

            Assert.IsTrue(validationResult.Count() > 0);
        }
    }
}

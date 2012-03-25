using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReEnterprise.Core.Generic;
using ReEnterprise.Core.Interface;
using ReEnterprise.Core.Tests.Resources;

namespace ReEnterprise.Core.Tests
{
    [TestClass]
    public class BusinessRuleValidatorTest
    {
        [TestInitialize]
        public void Setup()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(new CoreInstaller());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        [TestMethod]
        public void Invalid_Data_Should_Return_Validation_Messages()
        {
            var model = new TestModel();

            IBusinessRulesValidator businessRuleValidator = new BusinessRulesValidator();

            businessRuleValidator.Add(ServiceLocator.Current.GetInstance<IRuleValidator<TestModel>>(), model);

            IEnumerable<ValidationMessage> validationResult = businessRuleValidator.Validate();

            Assert.IsTrue(validationResult.Any());
        }

        #region Nested type: TestModel

        [Validator(typeof (TestModelValidator))]
        private class TestModel
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }
        }

        #endregion

        #region Nested type: TestModelValidator

        private class TestModelValidator : AbstractValidator<TestModel>
        {
            public TestModelValidator()
            {
                RuleFor(c => c.Id).NotEmpty().Length(36).WithLocalizedName(() => TestModelResources.Id);
                RuleFor(c => c.Name).NotEmpty().WithLocalizedName(() => TestModelResources.Name);
                RuleFor(c => c.Age).InclusiveBetween(20, 50).WithLocalizedName(() => TestModelResources.Age);
            }
        }

        #endregion
    }
}
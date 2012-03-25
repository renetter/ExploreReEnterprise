using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReEnterprise.Core.Generic;
using ReEnterprise.Core.Tests.Resources;

namespace ReEnterprise.Core.Tests
{
    [TestClass]
    public class ModelValidatorTest
    {
        [TestInitialize]
        public void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();

            container.Install(new CoreInstaller());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        [TestMethod]
        public void Valid_Model_Should_Not_Return_Validation_Messages()
        {
            var model = new TestModel
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "Name",
                                Age = 25
                            };

            var validator = ServiceLocator.Current.GetInstance<IRuleValidator<TestModel>>();

            validator.SetValidationTarget(model);

            Assert.IsFalse(validator.Validate().Any());
        }

        [TestMethod]
        public void Invalid_Model_Should_Return_Validation_Messages()
        {
            var model = new TestModel
                            {
                                Age = 10
                            };

            var validator = ServiceLocator.Current.GetInstance<IRuleValidator<TestModel>>();
            validator.SetValidationTarget(model);

            IEnumerable<ValidationMessage> message = validator.Validate();

            Assert.AreEqual(4, message.Count());
            Assert.IsTrue(message.Any(c => c.Field == "Id" && c.MessageType == ValidationMessageType.Error));
            Assert.IsTrue(message.Any(c => c.Field == "Name" && c.MessageType == ValidationMessageType.Error));
            Assert.IsTrue(message.Any(c => c.Field == "Age" && c.MessageType == ValidationMessageType.Error));
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Exception_Should_Be_Thrown_If_The_Target_Not_Set()
        {
            var validator = ServiceLocator.Current.GetInstance<IRuleValidator<TestModel>>();
            validator.Validate();
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
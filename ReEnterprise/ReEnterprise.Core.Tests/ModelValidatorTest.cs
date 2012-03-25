using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReEnterprise.Core.Interface;
using Microsoft.Practices.ServiceLocation;
using CommonServiceLocator.WindsorAdapter;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using ReEnterprise.Core.Tests.Resources;
using ReEnterprise.Core.Resources;
using ReEnterprise.Core.Generic;
using FluentValidation;
using FluentValidation.Attributes;

namespace ReEnterprise.Core.Tests
{    
    [TestClass]
    public class ModelValidatorTest
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
        public void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();

            container.Install(new CoreInstaller());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        [TestMethod]
        public void Valid_Model_Should_Not_Return_Validation_Messages()
        {
            TestModel model = new TestModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Name",
                Age = 25
            };

            IRuleValidator<TestModel> validator = ServiceLocator.Current.GetInstance<IRuleValidator<TestModel>>();

            validator.SetValidationTarget(model);

            Assert.IsFalse(validator.Validate().Any());
        }

        [TestMethod]
        public void Invalid_Model_Should_Return_Validation_Messages()
        {
            TestModel model = new TestModel() 
            { 
                Age = 10 
            };

            IRuleValidator<TestModel> validator = ServiceLocator.Current.GetInstance<IRuleValidator<TestModel>>();
            validator.SetValidationTarget(model);

            var message = validator.Validate();

            Assert.AreEqual(4, message.Count());
            Assert.IsTrue(message.Where(c => c.Field == "Id" && c.MessageType == ValidationMessageType.Error).Any());
            Assert.IsTrue(message.Where(c => c.Field == "Name" && c.MessageType == ValidationMessageType.Error).Any());
            Assert.IsTrue(message.Where(c => c.Field == "Age" && c.MessageType == ValidationMessageType.Error).Any());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Exception_Should_Be_Thrown_If_The_Target_Not_Set()
        {
            IRuleValidator<TestModel> validator = ServiceLocator.Current.GetInstance<IRuleValidator<TestModel>>();
            validator.Validate();
        }
    }
}

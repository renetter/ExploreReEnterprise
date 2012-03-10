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
using System.ComponentModel.DataAnnotations;
using ReEnterprise.Core.Tests.Resources;
using ReEnterprise.Core.Resources;

namespace ReEnterprise.Core.Tests
{    
    [TestClass]
    public class ModelValidatorTest
    {
        private class TestModel
        {
            [Display(Name = "Id", ResourceType = typeof(TestModelResources))]
            [Required(ErrorMessageResourceType = typeof(CoreResources), ErrorMessageResourceName = "Required")]
            [StringLength(36)]
            public string Id { get; set; }

            [Display(Name = "Name", ResourceType = typeof(TestModelResources))]
            [Required(ErrorMessageResourceType = typeof(CoreResources), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(Name = "Age", ResourceType = typeof(TestModelResources))]
            [Range(20, 50, ErrorMessageResourceType = typeof(CoreResources), ErrorMessageResourceName = "Range")]
            public int Age { get; set; }
        }

        [TestInitialize]
        public void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();

            container.Register(
                Component.For(typeof(IValidator<>)).ImplementedBy(typeof(ModelValidator<>)));

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

            IValidator<TestModel> validator = ServiceLocator.Current.GetInstance<IValidator<TestModel>>();

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

            IValidator<TestModel> validator = ServiceLocator.Current.GetInstance<IValidator<TestModel>>();
            validator.SetValidationTarget(model);

            IList<ValidationMessage> message = validator.Validate();

            Assert.AreEqual(3, message.Count);
            Assert.IsTrue(message.Where(c => c.Field == "Id" && c.MessageType == ValidationMessageType.Error).Any());
            Assert.IsTrue(message.Where(c => c.Field == "Name" && c.MessageType == ValidationMessageType.Error).Any());
            Assert.IsTrue(message.Where(c => c.Field == "Age" && c.MessageType == ValidationMessageType.Error).Any());
        }
    }
}

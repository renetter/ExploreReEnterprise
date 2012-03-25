using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.Windsor;
using ReEnterprise.Core;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Core.Interface;
using Microsoft.Practices.ServiceLocation;
using CommonServiceLocator.WindsorAdapter;
using ReEnterprise.Core.Generic;

namespace ReEnterprise.Domain.UserManagement.Tests
{
    [TestClass]
    public class UserValidatorTest
    {
        [TestInitialize]
        public void Setup()
        {
            IWindsorContainer container = new WindsorContainer();

            container.Install(new CoreInstaller());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        [TestMethod]
        public void Valid_User_Not_Produce_Validation_Error()
        {
            User user = new User
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = "User 1",
                LastName = "Last Name",
                Email = "test@test.com",
                PhoneNumber = "1245232323",
                Password = "A12@#ddd",
                ApplyPasswordPolicy = true,
                SecurityQuestion = "Test",
                SecurityQuestionAnswer = "Test"
            };

            IBusinessRulesValidator validator = ServiceLocator.Current.GetInstance<IBusinessRulesValidator>();

            validator.Add(ServiceLocator.Current.GetInstance<IRuleValidator<User>>(), user);

            var validationResult = validator.Validate();

            Assert.IsFalse(validationResult.HasError());
        }

        [TestMethod]
        public void Mandatory_Validation_Not_Satisfied_Will_Produce_Validation_Error()
        {
            User user = new User();

            IBusinessRulesValidator validator = ServiceLocator.Current.GetInstance<IBusinessRulesValidator>();

            validator.Add(ServiceLocator.Current.GetInstance<IRuleValidator<User>>(), user);

            var result = validator.Validate();

            Assert.IsTrue(result.HasError());

            Assert.AreEqual(4, result.Count());
        }
    }
}

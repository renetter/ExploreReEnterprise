using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.Windsor;
using ReEnterprise.Core;
using Microsoft.Practices.ServiceLocation;
using CommonServiceLocator.WindsorAdapter;
using Castle.MicroKernel.Registration;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using Moq;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Domain.UserManagement.Contract.Validator;
using ReEnterprise.Core.Interface;
using ReEnterprise.Domain.UserManagement.Validator;

namespace ReEnterprise.Domain.UserManagement.Tests
{
    [TestClass]
    public class PasswordPolicyRuleValidatorTest
    {
        [TestInitialize]
        public void Setup()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(new CoreInstaller());
            container.Register(Component.For<IPasswordPolicyRepository>().Instance(CreatePasswordPolicyRepositoryMock()));
            container.Register(Component.For<IPasswordPolicyRuleValidator>().ImplementedBy<PasswordPolicyRuleValidator>());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        private IPasswordPolicyRepository CreatePasswordPolicyRepositoryMock()
        {
            Mock<IPasswordPolicyRepository> passwordPolicyRepository = new Mock<IPasswordPolicyRepository>();
            passwordPolicyRepository.Setup(c => c.GetPasswordPolicy()).Returns(new PasswordPolicy { MinimumLength = 5, StrongPassword = true });

            return passwordPolicyRepository.Object;
        }

        [TestMethod]
        public void Validate_Valid_Password_Should_Not_Produce_Error_Messages()
        {
            // Arrange
            User user = new User
            {
                Password = "a1@wasfg",
                ApplyPasswordPolicy = true
            };

            IBusinessRulesValidator validator = ServiceLocator.Current.GetInstance<IBusinessRulesValidator>();
            validator.Add(ServiceLocator.Current.GetInstance<IPasswordPolicyRuleValidator>(), user);

            // Act
            IEnumerable<ValidationMessage> result = validator.Validate();

            // Assert
            Assert.IsFalse(result.HasError());
        }

        [TestMethod]
        public void Validate_Password_With_Minimum_Length_Less_Than_Password_Policy_Min_Length_Should_Produce_Error_Message()
        {
            // Arrange
            User user = new User
            {
                Password = "a1@",
                ApplyPasswordPolicy = true
            };

            IBusinessRulesValidator validator = ServiceLocator.Current.GetInstance<IBusinessRulesValidator>();
            validator.Add(ServiceLocator.Current.GetInstance<IPasswordPolicyRuleValidator>(), user);

            // Act
            IEnumerable<ValidationMessage> result = validator.Validate();

            // Assert
            Assert.IsTrue(result.HasError());
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void Validate_Password_Without_Special_Character_Should_Produce_Error_Message()
        {
            // Arrange
            User user = new User
            {
                Password = "invalid password",
                ApplyPasswordPolicy = true
            };

            IBusinessRulesValidator validator = ServiceLocator.Current.GetInstance<IBusinessRulesValidator>();
            validator.Add(ServiceLocator.Current.GetInstance<IPasswordPolicyRuleValidator>(), user);

            // Act
            IEnumerable<ValidationMessage> result = validator.Validate();

            // Assert
            Assert.IsTrue(result.HasError());
            Assert.AreEqual(1, result.Count());
        }
    }
}

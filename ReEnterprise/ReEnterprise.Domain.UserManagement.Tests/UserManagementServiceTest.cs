using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReEnterprise.Core;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using ReEnterprise.Domain.UserManagement.Contract.Resources;
using ReEnterprise.Domain.UserManagement.Contract.Service;
using ReEnterprise.Domain.UserManagement.Resources;
using System;
using ReEnterprise.Domain.UserManagement.Contract.Validator;
using ReEnterprise.Domain.UserManagement.Validator;

namespace ReEnterprise.Domain.UserManagement.Tests
{
    [TestClass]
    public class UserManagementServiceTest
    {
        [TestInitialize]
        public void Setup()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(new CoreInstaller());

            container.Register(Component.For<IUserManagementService>().ImplementedBy<UserManagementService>().LifeStyle.Transient);
            
            
            Mock<IPasswordPolicyRepository> passwordPolicyRepository = new Mock<IPasswordPolicyRepository>();
            passwordPolicyRepository.Setup(c => c.GetPasswordPolicy()).Returns(new PasswordPolicy { MinimumLength = 5, StrongPassword = true });

            container.Register(Component.For<IPasswordPolicyRepository>().Instance(passwordPolicyRepository.Object));
            container.Register(Component.For<IUserRepository>().Instance(ConfigureUserRepository()));
            
            container.Register(Component.For<IPasswordPolicyRuleValidator>().ImplementedBy<PasswordPolicyRuleValidator>().LifeStyle.Transient);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        private IUserRepository ConfigureUserRepository()
        {
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();

            return userRepository.Object;
        }

        [TestMethod]
        public void Save_Password_Policy_With_Valid_Data_Should_Be_Performed_Successfully()
        {
            PasswordPolicy passwordPolicy = new PasswordPolicy
            {
                MinimumLength = 5,
                StrongPassword = false
            };

            IUserManagementService userService = ServiceLocator.Current.GetInstance<IUserManagementService>();

            SavePasswordPolicyResponse result = userService.SavePasswordPolicy(passwordPolicy);

            Assert.IsFalse(result.ValidationMessages.HasError());
        }

        [TestMethod]
        public void Save_Password_Policy_With_Mixed_Character_Is_Set_When_The_Minimum_Length_Less_Than_Three_Should_Raise_Validation_Message()
        {
            PasswordPolicy passwordPolicy = new PasswordPolicy
            {
                MinimumLength = 1,
                StrongPassword = true,
            };

            IUserManagementService userService = ServiceLocator.Current.GetInstance<IUserManagementService>();

            SavePasswordPolicyResponse result = userService.SavePasswordPolicy(passwordPolicy);

            Assert.IsTrue(result.ValidationMessages.HasError());
            Assert.AreEqual(1, result.ValidationMessages.Count);
            Assert.AreEqual(string.Format(PasswordPolicyResources.MixedCharacterValidationError,
                UserManagementResources.PasswordPolicyMixedCharacter,
                UserManagementResources.PasswordPolicyMinLength,
                3),
                result.ValidationMessages[0].MessageValue);
        }

        [TestMethod]
        public void Retrieve_Password_Policy_Should_Have_Return_Password_Entity_Record()
        {
            IUserManagementService userService = ServiceLocator.Current.GetInstance<IUserManagementService>();

            RetrievePasswordPolicyResponse result = userService.RetrievePasswordPolicy();

            Assert.IsNotNull(result.PasswordPolicy);
        }

        [TestMethod]
        public void Create_User_With_Valid_Data_Sould_Not_Produce_Validation_Error()
        {
            IUserManagementService userService = ServiceLocator.Current.GetInstance<IUserManagementService>();

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

            CreateUserResponse result = userService.CreateUser(user);

            Assert.IsFalse(result.EntityHasError());
        }

        [TestMethod]
        public void Create_User_With_Invalid_Data_Produce_Validation_Error()
        {
            IUserManagementService userService = ServiceLocator.Current.GetInstance<IUserManagementService>();

            User user = new User
            {
                FirstName = "User 1",
                LastName = "Last Name",
                PhoneNumber = "1245232323",
                Password = "A1",
                ApplyPasswordPolicy = true,
                SecurityQuestionAnswer = "Test"
            };

            CreateUserResponse result = userService.CreateUser(user);

            Assert.IsTrue(result.EntityHasError());
        }
    }
}

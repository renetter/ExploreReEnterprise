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

namespace ReEnterprise.Domain.UserManagement.Tests
{
    [TestClass]
    public class UserManagementServiceTest
    {
        [TestInitialize]
        public void Setup()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(new CoreWindsorInstaller());

            container.Register(Component.For<IUserManagementService>().ImplementedBy<UserManagementService>().LifeStyle.Transient);
            
            
            Mock<IPasswordPolicyRepository> passwordPolicyRepository = new Mock<IPasswordPolicyRepository>();
            passwordPolicyRepository.Setup(c => c.GetPasswordPolicy()).Returns(new PasswordPolicy());

            container.Register(Component.For<IPasswordPolicyRepository>().Instance(passwordPolicyRepository.Object));

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        [TestMethod]
        public void Save_Password_Policy_With_Valid_Data_Should_Be_Performed_Successfully()
        {
            PasswordPolicy passwordPolicy = new PasswordPolicy
            {
                MinimumLength = 5,
                IsMixedCharacter = false
            };

            IUserManagementService userService = ServiceLocator.Current.GetInstance<IUserManagementService>();

            PasswordPolicy result = userService.SavePasswordPolicy(passwordPolicy);

            Assert.IsFalse(result.ValidationMessages.HasError());
        }

        [TestMethod]
        public void Save_Password_Policy_With_Mixed_Character_Is_Set_When_The_Minimum_Length_Less_Than_Three_Should_Raise_Validation_Message()
        {
            PasswordPolicy passwordPolicy = new PasswordPolicy
            {
                MinimumLength = 1,
                IsMixedCharacter = true,
            };

            IUserManagementService userService = ServiceLocator.Current.GetInstance<IUserManagementService>();

            PasswordPolicy result = userService.SavePasswordPolicy(passwordPolicy);

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

            PasswordPolicy passwordPolicy = userService.RetrievePasswordPolicy();

            Assert.IsNotNull(passwordPolicy);
        }
    }
}

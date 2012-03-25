using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReEnterprise.Core.Tests
{
    /// <summary>
    ///This is a test class for ValidatorExtensionTests and is intended
    ///to contain all ValidatorExtensionTests Unit Tests
    ///</summary>
    [TestClass]
    public class ValidatorExtensionTests
    {
        /// <summary>
        ///A test for IsContainErrors
        ///</summary>
        [TestMethod]
        public void Validation_Message_With_Error_Messages_Should_True()
        {
            IEnumerable<ValidationMessage> messages = new List<ValidationMessage>
                                                          {
                                                              new ValidationMessage
                                                                  {MessageType = ValidationMessageType.Error}
                                                          };
            const bool expected = true;
            bool actual = messages.HasError();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsContainInformation
        ///</summary>
        [TestMethod]
        public void Validation_Message_With_Information_Messages_Should_True()
        {
            IEnumerable<ValidationMessage> messages = new List<ValidationMessage>
                                                          {
                                                              new ValidationMessage
                                                                  {MessageType = ValidationMessageType.Information}
                                                          };
            const bool expected = true;
            bool actual = messages.HasInformation();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsContainWarning
        ///</summary>
        [TestMethod]
        public void Validation_Message_With_Warning_Messages_Should_True()
        {
            IEnumerable<ValidationMessage> messages = new List<ValidationMessage>
                                                          {
                                                              new ValidationMessage
                                                                  {MessageType = ValidationMessageType.Warning}
                                                          };
            const bool expected = true;
            bool actual = messages.HasWarning();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Create_Validation_Message()
        {
            var entity = new ValidatorEntity {Id = "Test"};
            ValidationMessage actual = entity.CreateValidationMessage(c => c.Id, "Test", ValidationMessageType.Error);

            Assert.AreEqual("Id", actual.Field);
            Assert.AreEqual("Test", actual.MessageValue);
            Assert.AreEqual(ValidationMessageType.Error, actual.MessageType);
        }

        [TestMethod]
        public void Add_Validation_Message()
        {
            IList<ValidationMessage> addedMessages = new List<ValidationMessage>
                                                         {new ValidationMessage {Field = "Test"}};
            IList<ValidationMessage> messages = new List<ValidationMessage>();

            messages.AddValidationMessages(addedMessages);

            Assert.AreEqual(1, messages.Count);
        }

        #region Nested type: ValidatorEntity

        private class ValidatorEntity : ResponseBase
        {
            public string Id { get; set; }
        }

        #endregion
    }
}
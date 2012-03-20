using ReEnterprise.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ReEnterprise.Core.Tests
{
    
    
    /// <summary>
    ///This is a test class for ValidatorExtensionTests and is intended
    ///to contain all ValidatorExtensionTests Unit Tests
    ///</summary>
    [TestClass()]
    public class ValidatorExtensionTests
    {
        private class ValidatorEntity : EntityBase
        {
            public string Id { get; set; }
        }

        /// <summary>
        ///A test for IsContainErrors
        ///</summary>
        [TestMethod()]
        public void Validation_Message_With_Error_Messages_Should_True()
        {
            IEnumerable<ValidationMessage> messages = new List<ValidationMessage> { new ValidationMessage { MessageType = ValidationMessageType.Error } };
            bool expected = true;
            bool actual;
            actual = ValidatorExtension.HasError(messages);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsContainInformation
        ///</summary>
        [TestMethod()]
        public void Validation_Message_With_Information_Messages_Should_True()
        {
            IEnumerable<ValidationMessage> messages = new List<ValidationMessage> { new ValidationMessage { MessageType = ValidationMessageType.Information } };
            bool expected = true;
            bool actual;
            actual = ValidatorExtension.HasInformation(messages);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsContainWarning
        ///</summary>
        [TestMethod()]
        public void Validation_Message_With_Warning_Messages_Should_True()
        {
            IEnumerable<ValidationMessage> messages = new List<ValidationMessage> { new ValidationMessage { MessageType = ValidationMessageType.Warning } };
            bool expected = true;
            bool actual;
            actual = ValidatorExtension.HasWarning(messages);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Create_Validation_Message()
        {
            ValidatorEntity entity = new ValidatorEntity { Id = "Test"};
            ValidationMessage actual = entity.CreateValidationMessage(c => c.Id, "Test", ValidationMessageType.Error);

            Assert.AreEqual("Id", actual.Field);
            Assert.AreEqual("Test", actual.MessageValue);
            Assert.AreEqual(ValidationMessageType.Error, actual.MessageType);
        }

        [TestMethod()]
        public void Add_Validation_Message()
        {
            IList<ValidationMessage> addedMessages = new List<ValidationMessage> { new ValidationMessage { Field = "Test" } };
            IList<ValidationMessage> messages = new List<ValidationMessage>();

            messages.AddValidationMessages(addedMessages);

            Assert.AreEqual(1, messages.Count);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;

namespace ModernStore.Domain.Tests
{
    [TestClass]
    public class CustomerTests
    {

        private readonly User _user = new User("victorialeite", "victoria123", "victoria123");
        private readonly Document _document = new Document("10653606737");

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GiveAnInvalidFirstNameShouldReturnANotification()
        {
            var name = new Name("", "Leite");
            var email = new Email("victoria@email.com");
            var customer = new Customer(name, email, _document, _user);

            Assert.IsFalse(customer.Valid);
        }

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GiveAnInvalidLastNameShouldReturnANotification()
        {
            var name = new Name("Victoria", "");
            var email = new Email("victoria@email.com");
            var customer = new Customer(name, email, _document, _user);

            Assert.IsFalse(customer.Valid);
        }

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GiveAnInvalidEmailShouldReturnANotification()
        {
            var name = new Name("Victoria", "Leite");
            var email = new Email("");
            var customer = new Customer(name, email, _document, _user);
            Assert.IsFalse(customer.Valid);
        }
    }
}

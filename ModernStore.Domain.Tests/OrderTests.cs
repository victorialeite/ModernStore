using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;

namespace ModernStore.Domain.Tests
{
    [TestClass]
    public class OrderTests
    {
        private static readonly Name _name = new Name("Victoria", "Leite");
        private static readonly Email _email = new Email("victoria@email.com");
        private static readonly Document _document = new Document("10653606737");
        private static readonly User _user = new User("victorialeite", "senha123", "senha123");
        private readonly Customer _customer = new Customer(_name, _email, _document, _user);

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAnOutOfStockProductItShouldReturnAnError()
        {


            var mouse = new Product("Mouse", 299, "mouse.jpg", 0);

            var order = new Order(_customer, 8, 10);
            order.AddItem(new OrderItem(mouse, 2));

            Assert.IsFalse(order.Valid);
        }

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAnInOfStockProductItShouldUpdateQuantityOnHand()
        {
            var mouse = new Product("Mouse", 299, "mouse.jpg", 20);

            var order = new Order(_customer, 8, 10);
            order.AddItem(new OrderItem(mouse, 2));

            Assert.IsTrue(mouse.QuantityOnHand == 18);
        }

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAnValidOrderTheTotalShouldBe610()
        {
            var mouse = new Product("Mouse", 300, "mouse.jpg", 20);

            var order = new Order(_customer, 12, 2);
            order.AddItem(new OrderItem(mouse, 2));

            Assert.IsTrue(order.Total() == 610);
        }
    }
}

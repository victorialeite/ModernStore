using Flunt.Validations;
using ModernStore.Shared.Entities;

namespace ModernStore.Domain.Entities
{
    public class OrderItem : Entity
    {
        protected OrderItem() { }

        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = Product.Price;

            AddNotifications(new Contract()
                .IsLowerOrEqualsThan(Quantity, Product.QuantityOnHand, "Quantity", "Não há em estoque a quantidade solicitada.")
                .IsGreaterThan(Quantity, 0, "Quantity", "Quantidade informada deve ser maior que zero."));

            Product.DecreaseQuantity(quantity);
        }

        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public decimal Total() => Price * Quantity;
    }
}

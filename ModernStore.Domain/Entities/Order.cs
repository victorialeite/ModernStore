using ModernStore.Domain.Enums;
using ModernStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;

namespace ModernStore.Domain.Entities
{
    public class Order : Entity
    {
        private readonly IList<OrderItem> _items;

        protected Order() { }

        public Order(Customer customer, decimal deliveryFee, decimal discount)
        {
            Customer = customer;
            DeliveryFee = deliveryFee;
            Discount = discount;
            CreateDate = DateTime.Now;
            Number = Guid.NewGuid().ToString().Replace("-","").Substring(0, 8).ToUpper();
            Status = EOrderStatus.Created;
            _items = new List<OrderItem>();

            AddNotifications(new Contract()
                .IsGreaterThan(DeliveryFee, 0, "DeliveryFee", "A taxa de entrega deve ser maior que zero")
                .IsGreaterThan(Discount, -1, "Discount", "O desconto não pode ser abaixo de zero"));
        }

        public Customer Customer { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string Number { get; private set; }
        public EOrderStatus Status { get; private set; }
        public ICollection<OrderItem> Items => _items.ToArray();
        public decimal DeliveryFee { get; private set; }
        public decimal Discount { get; private set; }

        public decimal SubTotal() => Items.Sum(x => x.Total());
        public decimal Total() => SubTotal() + DeliveryFee - Discount;

        public void AddItem(OrderItem item)
        {
            AddNotifications(item.Notifications);
            if (item.Valid)
                _items.Add(item);
        }
    }
}

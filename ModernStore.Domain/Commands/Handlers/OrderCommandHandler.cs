using Flunt.Notifications;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Commands.Handlers
{
    public class OrderCommandHandler : Notifiable, 
        ICommandHandler<RegisterOrderCommand>
        //ICommandHandler<UpdateOrderCommand>
        //ICommandHandler<DeleteOrderCommand>
        
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(ICustomerRepository customerRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public ICommandResult Handle(RegisterOrderCommand command)
        {
            // Instancia o cliente
            var customer = _customerRepository.GetByUserId(command.Customer);

            // Cria o pedido
            var order = new Order(customer, command.DeliveryFee, command.Discount);

            // Adiciona itens ao pedido
            foreach (var item in command.Items)
            {
                var product = _productRepository.Get(item.Product);
                order.AddItem(new OrderItem(product, item.Quantity));
            }

            // Adiciona as notificações do pedido no handler
            AddNotifications(order);

            // Persiste no banco
            if (Valid)
                _orderRepository.Save(order);

            return new RegisterOrderCommandResult(order.Number);
        }
    }
}

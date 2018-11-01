using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flunt.Notifications;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.Resources;
using ModernStore.Domain.Services;
using ModernStore.Domain.ValueObjects;
using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Commands.Handlers
{
    public class CustomerCommandHandler : Notifiable, 
        ICommandHandler<UpdateCustomerCommand>,
        ICommandHandler<RegisterCustomerCommand>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;

        public CustomerCommandHandler(ICustomerRepository customerRepository, IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(RegisterCustomerCommand command)
        {
            // Verificar se CPF já existe no banco
            if (_customerRepository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso!");
                return null;
            }

            // Gerar o novo cliente
            var name = new Name(command.FirstName, command.LastName);
            var email = new Email(command.Email);
            var document = new Document(command.Document);
            var user = new User(command.UserName, command.Password, command.ConfirmPassword);
            var customer = new Customer(name, email, document, user);

            // Adicionar as notificações
            AddNotifications(name, email, document, user, customer);
            
            if (Invalid)
                return null;

            // Inserir no banco
            _customerRepository.Save(customer);

            // Enviar E-mail de boas vindas
            _emailService.Send(
                customer.Name.ToString(), 
                customer.Email.EmailAddress, 
                string.Format(EmailTemplates.WelcomeEmailTitle, customer.Name),
                string.Format(EmailTemplates.WelcomeEmailBody, customer.Name));

            // Retornar algo
            return new RegisterCustomerCommandResult(customer.Id, customer.Name.ToString());
        }

        public ICommandResult Handle(UpdateCustomerCommand command)
        {
            // Recuperar o cliente
            var customer = _customerRepository.Get(command.Id);

            // Caso o cliente não exista
            if (customer == null)
            {
                AddNotification("Customer", "Cliente não encontrado");
                return null;
            }

            // Atualizar a entidade
            var name = new Name(command.FirstName, command.LastName);
            customer.Update(name, command.BirthDate);

            // Adicionar as notificações
            AddNotifications(name, customer);

            // Persistir no banco
            if (Valid)
                _customerRepository.Update(customer);

            return new UpdateCustomerCommandResult(customer.Name.ToString(), customer.Email.EmailAddress);
        }
    }
}

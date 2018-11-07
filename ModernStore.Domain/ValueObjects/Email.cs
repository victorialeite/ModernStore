using Flunt.Validations;
using ModernStore.Shared.ValueObjects;

namespace ModernStore.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        protected Email() { }

        public Email(string emailAddress)
        {
            EmailAddress = emailAddress;
            
            AddNotifications(new Contract()
                .Requires()
                .IsEmail(EmailAddress, "Email", "E-mail inválido."));
        }

        public string EmailAddress { get; private set; }
    }
}

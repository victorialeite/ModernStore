using Flunt.Validations;
using ModernStore.Shared.ValueObjects;

namespace ModernStore.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        protected Name() { }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            //Validations - Notification Pattern
            AddNotifications(new Contract()
                .Requires()
                .HasMaxLen(FirstName, 40, "FirstName", "O nome deve ter no máximo 40 caracteres.")
                .HasMinLen(FirstName, 3, "FirstName", "O nome deve ter no mínimo 3 caracteres.")
                .HasMaxLen(LastName, 40, "LastName", "O sobrenome deve ter no máximo 40 caracteres.")
                .HasMinLen(FirstName, 3, "LastName", "O sobrenome deve ter no mínimo 3 caracteres."));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}

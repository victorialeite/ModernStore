using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Commands.Results
{
    public class UpdateCustomerCommandResult : ICommandResult
    {
        public UpdateCustomerCommandResult(){}

        public UpdateCustomerCommandResult(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}

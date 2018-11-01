using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Queries.Result
{
    public class GetCustomerQueryResult : ICommandResult
    {
        public GetCustomerQueryResult(){}

        public GetCustomerQueryResult(string name, string document, string email, string username, string password, bool active)
        {
            Name = name;
            Document = document;
            Email = email;
            Username = username;
            Password = password;
            Active = active;
        }

        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}

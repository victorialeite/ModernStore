using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Queries.Result;

namespace ModernStore.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Customer Get(Guid id);
        Customer GetByUsername(string username);
        Customer GetByUserId(Guid id);
        Customer GetByEmail(string email);
        Customer GetByDocument(string document);

        GetCustomerQueryResult Get(string username);

        void Save(Customer customer);

        void Update(Customer customer);

        bool DocumentExists(string document);
    }
}

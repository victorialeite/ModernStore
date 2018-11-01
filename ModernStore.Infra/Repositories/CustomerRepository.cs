using Dapper;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Queries.Result;
using ModernStore.Domain.Repositories;
using ModernStore.Infra.DataContexts;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using ModernStore.Shared;

namespace ModernStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ModernStoreDataContext _context;

        public CustomerRepository(ModernStoreDataContext context)
        {
            _context = context;
        }

        public Customer Get(Guid id)
        {
            return _context // contexto
                .Customers // lista de customer
                .Include(x => x.User) // para o select trazer o user
                .FirstOrDefault(x => x.Id == id);
        }

        public Customer GetByUsername(string username)
        {
            return _context
                .Customers
                .Include(x => x.User)
                .AsNoTracking()
                .FirstOrDefault(x => x.User.Username == username);
        }

        public Customer GetByDocument(string document)
        {
            return _context // contexto
                .Customers // lista de customer
                .Include(x => x.User) // para o select trazer o user
                .FirstOrDefault(x => x.Document.Number == document);
        }

        public GetCustomerQueryResult Get(string username)
        {
            // Usando Dapper

            var query = "select * from [GetCustomerInfoView] where [Active] = 1 and [Username]=@username";
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                conn.Open();
                return conn
                    .Query<GetCustomerQueryResult>(query, 
                    new {username = username})
                    .FirstOrDefault();
            }

            // Usando Entity Framework

            //return _context
            //    .Customers
            //    .Include(x => x.User)
            //    .AsNoTracking()
            //    .Select(x => new GetCustomerQueryResult
            //    (
            //        x.Name.ToString(),
            //        x.Document.Number,
            //        x.Email.EmailAddress,
            //        x.User.Username,
            //        x.User.Password,
            //        x.User.Active
            //    ))
            //    .FirstOrDefault(x => x.Username == username);

        }

        public Customer GetByEmail(string email)
        {
            return _context // contexto
                .Customers // lista de customer
                .Include(x => x.User) // para o select trazer o user
                .FirstOrDefault(x => x.Email.EmailAddress == email);
        }

        public Customer GetByUserId(Guid id)
        {
            return _context // contexto
                .Customers // lista de customer
                .Include(x => x.User) // para o select trazer o user
                .FirstOrDefault(x => x.User.Id == id);
        }

        public void Save(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }

        public bool DocumentExists(string document)
        {
            return _context.Customers.Any(x => x.Document.Number == document);
        }
    }
}

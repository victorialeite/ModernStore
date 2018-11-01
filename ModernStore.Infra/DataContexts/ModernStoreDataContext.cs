using ModernStore.Domain.Entities;
using ModernStore.Infra.Mappings;
using System.Data.Entity;
using ModernStore.Shared;

namespace ModernStore.Infra.DataContexts
{
    public class ModernStoreDataContext : DbContext
    {
        public ModernStoreDataContext() : base(Runtime.ConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            // Como funciona o lazy loading:
            // cliente = repositorio.obterCliente(id) - faz um select no banco e traz apenas o cliente
            // cliente.enderecos.FirstOrDefault - faz OUTRO select no banco e traz o endereço
            // Bom para cenários onde não se sabe em que momento as informações serão necessárias

            // Como funciona o eager loading (obrigado a especificar o que deseja trazer no select)
            // return customer.FirstOrDefault(x => x.Id == id) - faz um select e retorna apenas o cliente. Os endereços virão vazios
            // return customer.Include(x => x.Enderecos).FirstOrDefault(x => x.Id == id) - faz apenas um select retornando o cliente + os endereços

            // Proxy Creation
            // Traz informações a mais como metadata, data annotation.. 
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new UserMap());

            base.OnModelCreating(modelBuilder);
        }

        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}

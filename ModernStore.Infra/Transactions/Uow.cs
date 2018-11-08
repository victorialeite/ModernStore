using ModernStore.Domain.Repositories;
using ModernStore.Infra.DataContexts;

namespace ModernStore.Infra.Transactions
{
    public class Uow : IUow
    {
        private readonly ModernStoreDataContext _context;
        
        public Uow(ModernStoreDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            // Do nothing :)
            // o próprio entity framework não commita, e
            // deixa a transação morrer no fim do ciclo de vida do request.
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernStore.Domain.Repositories;

namespace ModernStore.Infra.Transactions
{
    public interface IUow
    {
        void Commit();
        void Rollback();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernStore.Domain.Enums
{
    public enum EOrderStatus
    {
        Created,
        InProgress,
        OutForDelivery,
        Delivery,
        Canceled
    }
}

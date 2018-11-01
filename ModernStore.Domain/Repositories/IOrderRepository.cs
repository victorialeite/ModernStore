﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernStore.Domain.Entities;

namespace ModernStore.Domain.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}

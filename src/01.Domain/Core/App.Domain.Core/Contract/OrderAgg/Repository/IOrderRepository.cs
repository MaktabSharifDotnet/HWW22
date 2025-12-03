using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.OrderAgg.Repository
{
    public interface IOrderRepository
    {

        public  Task AddAsync(Order order, CancellationToken cancellationToken);

        public Task<int> SaveAsync(CancellationToken cancellationToken);


    }
}

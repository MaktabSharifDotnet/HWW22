using App.Domain.Core.Dtos.OrderItemAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.OrderItemAgg.Repository
{
    public interface IOrderItemRepository
    {
        public Task<OrderItemDto?> GetbyOrderId(int orderId,CancellationToken cancellationToken);
    }
}

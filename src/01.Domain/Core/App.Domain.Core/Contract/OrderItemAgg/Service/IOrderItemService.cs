using App.Domain.Core.Dtos.OrderItemAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.OrderItemAgg.Service
{
    public interface IOrderItemService
    {
       
       // public Task<OrderItemDto?> GetbyOrderId(int orderId, CancellationToken cancellationToken);
        public Task<List<OrderItemDto>> GetOrderItemDtos(int orderId, CancellationToken cancellationToken);
    }
}

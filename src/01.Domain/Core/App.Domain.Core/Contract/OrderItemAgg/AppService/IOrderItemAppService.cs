using App.Domain.Core._common;
using App.Domain.Core.Dtos.OrderItemAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.OrderItemAgg.AppService
{
    public interface IOrderItemAppService
    {
        public Task<Result<OrderItemDto>> GetbyOrderId(int orderId, CancellationToken cancellationToken);
    }
}

using App.Domain.Core.Contract.OrderItemAgg.Repository;
using App.Domain.Core.Contract.OrderItemAgg.Service;
using App.Domain.Core.Dtos.OrderItemAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.OrderItemAgg
{
    public class OrderItemService(IOrderItemRepository _orderItemRepository) : IOrderItemService
    {
        //public async Task<OrderItemDto?> GetbyOrderId(int orderId, CancellationToken cancellationToken)
        //{
        //  return await  _orderItemRepository.GetbyOrderId(orderId, cancellationToken);

        //}
        public async Task<List<OrderItemDto>> GetOrderItemDtos(int orderId, CancellationToken cancellationToken)
        {
          return await  _orderItemRepository.GetOrderItemDtos(orderId , cancellationToken);
        }
    }
}

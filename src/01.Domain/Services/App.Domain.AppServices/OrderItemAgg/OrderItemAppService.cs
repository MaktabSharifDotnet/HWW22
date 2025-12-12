using App.Domain.Core._common;
using App.Domain.Core.Contract.OrderItemAgg.AppService;
using App.Domain.Core.Contract.OrderItemAgg.Service;
using App.Domain.Core.Dtos.OrderItemAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.OrderItemAgg
{
    public class OrderItemAppService(IOrderItemService _orderItemService) : IOrderItemAppService
    {
        //public async Task<Result<OrderItemDto>> GetbyOrderId(int orderId, CancellationToken cancellationToken)
        //{
        //    OrderItemDto? orderItemDto =await  _orderItemService.GetbyOrderId(orderId, cancellationToken);
        //    if (orderItemDto==null)
        //    {
        //      return Result<OrderItemDto>.Failure("No OrderItem Found");
        //    }
        //    return Result<OrderItemDto>.Success(orderItemDto);
        //}
        public async Task<Result<List<OrderItemDto>>> GetOrderItemDtos(int orderId, CancellationToken cancellationToken)
        {
            List<OrderItemDto> orderItemDtos = await _orderItemService.GetOrderItemDtos(orderId, cancellationToken);
            if (!orderItemDtos.Any())
            {
                return  Result<List<OrderItemDto>>.Failure("No OrderItem Found");

            }

            return  Result<List<OrderItemDto>>.Success(orderItemDtos);

        }
    }
}

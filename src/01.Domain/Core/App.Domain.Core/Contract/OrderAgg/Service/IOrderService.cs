using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.OrderAgg.Service
{
    public interface IOrderService
    {
        public Order CreateOrder(List<OrderItem> orderItems, OrderDto orderDto);

        public List<OrderItem> CreateOrderItems(List<CartProduct> cartProducts);

        public  Task AddAsync(Order order, CancellationToken cancellationToken);

        public  Task<int> SaveAsync(CancellationToken cancellationToken);

        public Task<List<OrderDto>> GetOrderDtos(CancellationToken cancellationToken);
    }
}

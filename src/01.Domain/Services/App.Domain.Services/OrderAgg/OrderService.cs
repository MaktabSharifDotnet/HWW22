using App.Domain.Core.Contract.OrderAgg.Repository;
using App.Domain.Core.Contract.OrderAgg.Service;
using App.Domain.Core.Contract.UserAgg.Service;
using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.OrderAgg
{
    public class OrderService(IOrderRepository _orderRepository) : IOrderService
    {
        public List<OrderItem> CreateOrderItems(List<CartProduct> cartProducts)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var cartItem in cartProducts)
            {
                    OrderItem orderItem = new OrderItem();
                    orderItem.ProductId = cartItem.ProductId;
                    orderItem.Count = cartItem.Count;
                    orderItem.UnitPrice = (cartItem.Count * cartItem.Product.Price);
                    orderItems.Add(orderItem);
                
            }

            return orderItems;
        }

        public Order CreateOrder(List<OrderItem> orderItems, OrderDto orderDto)
        {
            Order order = new Order()
            {
                UserId = orderDto.UserId,
                CreatedAt = DateTime.Now,
                IsPaid = true,
                TotalAmount = orderDto.TotalAmount,
                OrderItems = orderItems

            };

            return order;

        }

        public async Task AddAsync(Order order,CancellationToken cancellationToken)
        {
            await _orderRepository.AddAsync(order,cancellationToken);
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken) 
        {
           return  await _orderRepository.SaveAsync(cancellationToken);
        }
    }
}

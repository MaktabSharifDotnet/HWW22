using App.Domain.Core.Contract.CartAgg.Service;
using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Contract.OrderAgg.Service;
using App.Domain.Core.Contract.UserAgg.Service;
using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.OrderAgg
{
    public class OrderAppService(IUserService _userService , ICartService _cartService
        ,IOrderService _orderService) : IOrderAppService
    {
        public async Task Create(int userId, int cartId, CancellationToken cancellationToken)
        {
            var user = await _userService.GetById(userId, cancellationToken);
            if (user == null)
            {
                throw new Exception("همچین کاربری موجود نیست.");
            }


            var cart = _userService.GetCart(user, cartId);
            if (cart == null)
            {
                throw new Exception("کاربر سبد خریدی با این مشخصات ندارد.");
            }

            decimal totalAmount = _userService.CalculateTotalPrice(cart.CartProducts);

           
           
             var orderItems = _orderService.CreateOrderItems(cart.CartProducts);

              OrderDto orderDto = new OrderDto()
              {
                  UserId = userId,
                  TotalAmount = totalAmount,
              
              };

            Order order=_orderService.CreateOrder(orderItems, orderDto);


            await _orderService.AddAsync(order , cancellationToken);


        }

        public async Task<int> CheckOut(int userId, int cartId, CancellationToken cancellationToken)
        {

            var user = await _userService.GetById(userId, cancellationToken);
            if (user == null)
            {
                throw new Exception("همچین کاربری موجود نیست.");
            }

            var cart = _userService.GetCart(user, cartId);
            if (cart == null)
            {
                throw new Exception("شما همچین سبد خریدی ندارید.");
            }

            _userService.CaheckInventory(cart.CartProducts);

            decimal totalPrice = _userService.CalculateTotalPrice(cart.CartProducts);
            _userService.Withdraw(user, totalPrice, cart.CartProducts);

            _userService.DecreaseInventory(cartId, cart.CartProducts);

            await Create(userId, cartId  ,cancellationToken);

            await _cartService.RemoveCart(cart.Id, cancellationToken);

             return  await _orderService.SaveAsync(cancellationToken);
           
        }
    }
}

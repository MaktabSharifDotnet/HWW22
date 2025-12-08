using App.Domain.Core.Contract.UserAgg.Repository;
using App.Domain.Core.Contract.UserAgg.Service;
using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.UserAgg
{
    public class UserService(IUserRepository _userRepository) : IUserService
    {
      
        public async Task<User?> GetById(int userId, CancellationToken cancellationToken)
        {
           return await _userRepository.GetById(userId , cancellationToken);
        }

        public async Task<int> Login(string username, string password, CancellationToken cancellationToken)
        {
            User? user=await _userRepository.GetByUsername(username, cancellationToken);
            if (user==null || user.Password!=password)
            {
                return 0;
            }

            LocalStorage.LoginUser=user;
            return  user.Id;

        }

        public void LogOut()
        {
            LocalStorage.LoginUser = null;
        }

        public Cart? GetCart(User user, int cartId)
        {
            return user.Carts.FirstOrDefault(c => c.Id == cartId);


        }

        public void CaheckInventory(List<CartProduct> cartProducts) 
        {

            foreach (var cartItemDb in cartProducts)
            {
                if (cartItemDb.Count > cartItemDb.Product.Inventory)
                {
                    throw new Exception("بیشتر از موجودی کالا در خواست کرده اید.");
                }

            }
        }


        public decimal CalculateTotalPrice(List<CartProduct> cartProducts)
        {
            decimal total = 0m;


            foreach (var cartItemDb in cartProducts)
            {
                total += (cartItemDb.Product.Price) * cartItemDb.Count;
            }
            return total;
        }

        public void Withdraw(User user, decimal totalPrice, List<CartProduct> cartProducts) 
        {
 
            if (user.Balance < totalPrice)
            {
                throw new Exception("موجودی کافی نمیباشد.");
            }

            user.Balance = user.Balance - totalPrice;
        }

        public void DecreaseInventory(int cartId,List<CartProduct> cartProducts) 
        {
            var cartItem = cartProducts.FirstOrDefault(c => c.CartId == cartId);
            cartItem!.Product.Inventory = cartItem!.Product.Inventory - cartItem.Count;
        }

       
    }
}

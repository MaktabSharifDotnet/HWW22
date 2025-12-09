using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Dtos.UserAgg;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.UserAgg.Service
{
    public interface IUserService
    {
        public Task<int> Login(string username, string password , CancellationToken cancellationToken);
        public Task<User?> GetById(int userId, CancellationToken cancellationToken);
        public void LogOut();
        public Cart? GetCart(User user, int cartId);
        public void CaheckInventory(List<CartProduct> cartProducts);
        public decimal CalculateTotalPrice(List<CartProduct> cartProducts);
        public void Withdraw(User user, decimal totalPrice, List<CartProduct> cartProducts);
        public void DecreaseInventory(int cartId, List<CartProduct> cartProducts);

        public Task<List<UserDto>> GetAllById(CancellationToken cancellationToken);


    }
}

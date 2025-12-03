using App.Domain.Core.Contract.CartAgg.AddService;
using App.Domain.Core.Contract.CartAgg.Service;
using App.Domain.Core.Dtos.CartAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.CartAgg
{
    public class CartAppService(ICartService _cartService) : ICartAppService
    {
        public Task<int> Add(int userId, int productId, CancellationToken cancellationToken)
        {
          return _cartService.Add(userId, productId, cancellationToken);
        }

        public async Task<int> DecreaseItem(int userId, int productId, CancellationToken cancellationToken)
        {
           return await _cartService.DecreaseItem(userId, productId, cancellationToken);
        }

        public async Task<List<CartItemDto>> GetUserCartItems(int userId, CancellationToken cancellationToken)
        {
          return await _cartService.GetUserCartItems(userId, cancellationToken);
        }

        public async Task<int> MergeCart(int userId, List<CartItemDto> sessionItems, CancellationToken cancellationToken)
        {
          return await _cartService.MergeCart(userId, sessionItems, cancellationToken);
        }

        public Task<int> Remove(int userId, int productId, CancellationToken cancellationToken)
        {
          return _cartService.Remove(userId, productId, cancellationToken);
        }


    }
}

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
        public async Task<int> MergeCart(int userId, List<CartItemDto> sessionItems, CancellationToken cancellationToken)
        {
          return await _cartService.MergeCart(userId, sessionItems, cancellationToken);
        }
    }
}

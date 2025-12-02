using App.Domain.Core.Dtos.CartAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CartAgg.AddService
{
    public interface ICartAppService
    {
        public Task<int> MergeCart(int userId, List<CartItemDto> sessionItems, CancellationToken cancellationToken);
        public Task<List<CartItemDto>> GetUserCartItems(int userId, CancellationToken cancellationToken);
        public Task<int> Add(int userId, int productId, CancellationToken cancellationToken);
    }

}

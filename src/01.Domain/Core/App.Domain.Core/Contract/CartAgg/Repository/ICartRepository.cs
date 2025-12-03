using App.Domain.Core.Dtos.CartAgg;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CartAgg.Repository
{
    public interface ICartRepository
    {
      
        public  Task<Cart?> GetByUserId(int userId, CancellationToken cancellationToken);


        public Task<int> Add(Cart cart, CancellationToken cancellationToken);


        public  Task<int> Save(CancellationToken cancellationToken);
        public Task<CartProduct?> GetCartProductIncludingDeleted(int cartId, int productId, CancellationToken cancellationToken);
        public Task Remove(int cartId, CancellationToken cancellationToken);



    }
}

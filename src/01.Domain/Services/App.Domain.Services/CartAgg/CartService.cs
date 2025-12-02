using App.Domain.Core.Contract.CartAgg.Repository;
using App.Domain.Core.Contract.CartAgg.Service;
using App.Domain.Core.Dtos.CartAgg;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.CartAgg
{
    public class CartService(ICartRepository _cartRepository) : ICartService
    {
        public async Task<int> MergeCart(int userId, List<CartItemDto> sessionItems, CancellationToken cancellationToken)
        {
         
            var userCartDb = await _cartRepository.GetByUserId(userId, cancellationToken);

          
            if (userCartDb == null)
            {
                userCartDb = new Cart
                {
                    UserId = userId,
                };
                await _cartRepository.Add(userCartDb, cancellationToken);
              
            }

         
            foreach (var sessionItem in sessionItems)
            {
                var existingProduct = userCartDb.CartProducts
                    .FirstOrDefault(cp => cp.ProductId == sessionItem.ProductId);

                if (existingProduct != null)
                {
                  
                    existingProduct.Count += sessionItem.Count;
                }
                else
                {
                  
                    userCartDb.CartProducts.Add(new CartProduct
                    {
                        ProductId = sessionItem.ProductId,
                        Count = sessionItem.Count,
                        IsDeleted = false,
                        CartId = userCartDb.Id 
                    });
                }
            }

    
           return await _cartRepository.Save(cancellationToken);
        }
    }
}

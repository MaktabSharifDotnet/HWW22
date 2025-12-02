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
        public async Task<int> Add(int userId, int productId, CancellationToken cancellationToken)
        {
           Cart? usercartDb= await _cartRepository.GetByUserId(userId , cancellationToken);

            if (usercartDb==null)
            {
                usercartDb  = new Cart() { UserId = userId };
                int cartId=await _cartRepository.Add(usercartDb , cancellationToken);
                
            }

            CartProduct? cartProduct= usercartDb.CartProducts.FirstOrDefault(c=>c.ProductId==productId);
            if (cartProduct != null) 
            {
                cartProduct.Count++;
            }
            else 
            {
                usercartDb.CartProducts.Add(new CartProduct
                {
                    ProductId = productId,
                    CartId = usercartDb.Id,
                    Count = 1
                });
                
            }
          return  await _cartRepository.Save(cancellationToken);
        }

        public async Task<List<CartItemDto>> GetUserCartItems(int userId, CancellationToken cancellationToken)
        {
            Cart? userCartDb=await  _cartRepository.GetByUserId(userId, cancellationToken);
            if (userCartDb==null)
            {
                return new List<CartItemDto>();
            }
          return userCartDb!.CartProducts.Select(c=>new CartItemDto 
                  {
                       ProductId = c.ProductId,
                       Title = c.Product.Title,
                       Image = c.Product.Image,
                       Price = c.Product.Price,
                       Count = c.Count  
                  
                  }).ToList();
        }

        public async Task<int> MergeCart(int userId, List<CartItemDto> sessionItems, CancellationToken cancellationToken)
        {
         
            var cartDb = await _cartRepository.GetByUserId(userId, cancellationToken);

          
            if (cartDb == null)
            {
                cartDb = new Cart
                {
                   UserId = userId,
                };
                await _cartRepository.Add(cartDb, cancellationToken);
              
            }

         
            foreach (var sessionItem in sessionItems)
            {
                var existingProduct = cartDb.CartProducts
                    .FirstOrDefault(cp => cp.ProductId == sessionItem.ProductId);

                if (existingProduct != null)
                {
                  
                    existingProduct.Count += sessionItem.Count;
                }
                else
                {
                  
                    cartDb.CartProducts.Add(new CartProduct
                    {
                        ProductId = sessionItem.ProductId,
                        Count = sessionItem.Count,
                        IsDeleted = false,
                        CartId = cartDb.Id 
                    });
                }
            }

    
           return await _cartRepository.Save(cancellationToken);
        }
    }
}

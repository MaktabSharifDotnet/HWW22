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
           Cart? userCartDb= await _cartRepository.GetByUserId(userId , cancellationToken);

            if (userCartDb==null)
            {
                userCartDb  = new Cart() { UserId = userId  };
                int cartId=await _cartRepository.Add(userCartDb , cancellationToken);
              
            }

            CartProduct? cartProduct= userCartDb.CartProducts.FirstOrDefault(c=>c.ProductId==productId);
            if (cartProduct != null) 
            {
                cartProduct.Count++;
            }
            else 
            {
                var deletedItem = await _cartRepository.GetCartProductIncludingDeleted(userCartDb.Id, productId, cancellationToken);

                if (deletedItem != null)
                {
                    
                    deletedItem.IsDeleted = false; 
                    deletedItem.Count = 1;         

              
                }
                else
                {
                    userCartDb.CartProducts.Add(new CartProduct
                    {
                        ProductId = productId,
                        Count = 1,
                        IsDeleted = false
                    });
                }

            }
          return  await _cartRepository.Save(cancellationToken);
        }

        public async Task<int> DecreaseItem(int userId, int productId, CancellationToken cancellationToken)
        {
            var userCartDb= await _cartRepository.GetByUserId(userId , cancellationToken);
            if (userCartDb == null)
            {
                userCartDb = new Cart() { UserId = userId };
                int cartId = await _cartRepository.Add(userCartDb, cancellationToken);

            }

            var cartProduct=  userCartDb.CartProducts.FirstOrDefault(cp => cp.ProductId == productId);
            if (cartProduct==null)
            {
                throw new Exception("همچین کالایی در سبد خریدتان موجود نمیباشد.");
            }

            if (cartProduct.Count == 1)
            {
                cartProduct.IsDeleted = true;
            }

            else 
            {
                cartProduct.Count--;
            
            }

          return await _cartRepository.Save(cancellationToken);
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

             
                await _cartRepository.Save(cancellationToken);
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
                   
                    var deletedProduct = await _cartRepository.GetCartProductIncludingDeleted(cartDb.Id, sessionItem.ProductId, cancellationToken);

                    if (deletedProduct != null)
                    {
                       
                        deletedProduct.IsDeleted = false;


                        deletedProduct.Count = sessionItem.Count;


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
            }

         
            return await _cartRepository.Save(cancellationToken);
        }

        public async Task<int> Remove(int userId, int productId, CancellationToken cancellationToken)
        {
            var userCartDb=await _cartRepository.GetByUserId(userId, cancellationToken);
            if (userCartDb==null)
            {
                userCartDb = new Cart() { UserId = userId };
                int cartId = await _cartRepository.Add(userCartDb, cancellationToken);
            }

            var cartProduct= userCartDb.CartProducts.FirstOrDefault(cp => cp.ProductId == productId);
            if (cartProduct == null) 
            {
                throw new Exception("در سبد خرید همیجن کالایی موجود نیست.");
            }

            
            cartProduct.IsDeleted= true;
            return await _cartRepository.Save(cancellationToken);

        }
    }
}

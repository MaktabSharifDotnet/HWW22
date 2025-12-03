using App.Domain.Core.Contract.CartAgg.Repository;
using App.Domain.Core.Dtos.CartAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.CartAgg
{
    public class CartRepository(AppDbContext _context) : ICartRepository
    {
    

        public async Task<Cart?> GetByUserId(int userId, CancellationToken cancellationToken)
        {
           
            return await _context.Carts
                         .Include(c => c.CartProducts)
                         .ThenInclude(cp => cp.Product)
                         .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        }
        public async Task<CartProduct?> GetCartProductIncludingDeleted(int cartId, int productId, CancellationToken cancellationToken)
        {
            
            return await _context.CartProducts
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(cp => cp.CartId == cartId && cp.ProductId == productId, cancellationToken);
        }

        public async Task<int> Add(Cart cart, CancellationToken cancellationToken)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
          
            return cart.Id;
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
          return  await _context.SaveChangesAsync(cancellationToken);
        }

     

       


    }
}

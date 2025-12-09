using App.Domain.Core.Contract.OrderAgg.Repository;
using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.OrderAgg
{
    public class OrderRepository(AppDbContext _context) : IOrderRepository
    {
        public async Task AddAsync(Order order, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
           
        }

        public async Task<List<OrderDto>> GetOrderDtos(CancellationToken cancellationToken)
        {
               return  await _context.Orders
                       .Select(o=>new OrderDto 
                        { 
                            OrderId = o.Id,
                            UserId = o.UserId,
                            TotalAmount = o.TotalAmount,
                            CreatedAt = o.CreatedAt,
                            IsPaid = o.IsPaid
                        })
                       .ToListAsync(cancellationToken);
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }



    }
}

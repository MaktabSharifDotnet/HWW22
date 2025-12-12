using App.Domain.Core.Contract.OrderItemAgg.Repository;
using App.Domain.Core.Dtos.OrderItemAgg;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.OrderItemAgg
{
    public class OrderItemRepository(AppDbContext _context) : IOrderItemRepository
    {
       

        //public async Task<OrderItemDto?> GetbyOrderId(int orderId, CancellationToken cancellationToken)
        //{
        //    return await _context.OrderItems.Where(oi=>oi.OrderId==orderId)
        //                  .Select(oi => new OrderItemDto
        //                  {
        //                      OrderId = oi.OrderId,
        //                      ProductId = oi.ProductId,
        //                      Name = oi.Product.Title,
        //                      Count = oi.Count,
        //                      UnitPrice = oi.UnitPrice
        //                  }).FirstOrDefaultAsync(cancellationToken);
        //}

        public async Task<List<OrderItemDto>> GetOrderItemDtos(int orderId, CancellationToken cancellationToken)
        {
            return await _context.OrderItems.Where(oi => oi.OrderId == orderId)
                          .Select(oi => new OrderItemDto
                          {
                              OrderId = oi.OrderId,
                              ProductId = oi.ProductId,
                              Name = oi.Product.Title,
                              Count = oi.Count,
                              UnitPrice = oi.UnitPrice,
                              TotalPrice = oi.TotalPrice,
                          }).ToListAsync(cancellationToken);
        }
    }
}

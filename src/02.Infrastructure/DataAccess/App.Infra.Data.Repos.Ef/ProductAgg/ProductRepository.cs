using App.Domain.Core.Contract.ProductAgg.Repository;
using App.Domain.Core.Dtos.ProductAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace App.Infra.Data.Repos.Ef.ProductAgg
{
    public class ProductRepository(AppDbContext _context) : IProductRepository
    {

        public async Task<ProductListDto> GetAll(int pageNumber,int pageSize  ,int? categoryId = null, CancellationToken cancellationToken = default )
        {
            var query = _context.Products.AsQueryable();

            if (categoryId > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            query = query.OrderByDescending(p => p.CreatedAt);

            var totalCount = await query.CountAsync(cancellationToken);
            var products = await query
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize)
                .Select(p => new ProductDto()
                {
                    Id = p.Id,
                    Title = p.Title,
                    CategoryName = p.Category.Name,
                    Description = p.Description,
                    Image = p.Image,
                    CreatedAt = p.CreatedAt,
                    Price = p.Price,
                    Inventory = p.Inventory,
                })
                .ToListAsync(cancellationToken);

            return new ProductListDto
            {
                Products = products,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<ProductDto?> GetById(int productId , CancellationToken cancellationToken)
        {
           return await _context.Products
                .Where(p=>p.Id== productId)
                .Select(p=> new ProductDto() 
                {
                   Id = p.Id,
                   Title = p.Title,
                   CategoryName = p.Category.Name,
                   Description = p.Description,
                   Image = p.Image,
                   Price = p.Price,
                   CreatedAt= p.CreatedAt,
                   Inventory = p.Inventory,
                   
                  
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

       
    }
}

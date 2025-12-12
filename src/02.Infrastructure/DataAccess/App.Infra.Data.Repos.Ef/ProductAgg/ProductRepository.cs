using App.Domain.Core._common;
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
        public async Task<int> Create(ProductDto productDto, CancellationToken cancellationToken)
        {
            Product product = new Product()
            {
                Id = productDto.Id,
                Title= productDto.Title,
                Description= productDto.Description,
                Image= productDto.Image,
                Price= productDto.Price,
                Inventory = productDto.Inventory,
                CreatedAt= productDto.CreatedAt,
                CategoryId= productDto.CategoryId,

            };
            await _context.Products.AddAsync(product);
             await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Edit(ProductDto productDto, CancellationToken cancellationToken)
        {
            Product? product=await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productDto.Id , cancellationToken);

            product!.Title = productDto.Title;
            product.Description = productDto.Description;
            product.Image=productDto.Image;
            product.Price=productDto.Price;
            product.CreatedAt=productDto.CreatedAt;
            product.Inventory=productDto.Inventory;
            product.CategoryId=productDto.CategoryId;

            return await _context.SaveChangesAsync(cancellationToken);

        }

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
                   CategoryId = p.Category.Id


                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> GetCount(CancellationToken cancellationToken)
        {
            return await _context.Products.CountAsync(cancellationToken);
        }

      
    }
}

using App.Domain.Core.Contract.ProductAgg.Repository;
using App.Domain.Core.Dtos.ProductAgg;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
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
      
        public List<ProductDto> GetAll(int? categoryId=null)
        {
            var query = _context.Products.AsQueryable();
            if (categoryId>0)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            query = query.OrderByDescending(p => p.CreatedAt);

            return query
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

                }).ToList();
        }
    }
}

using App.Domain.Core._common;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.CategoryAgg
{
    public class CategoryRepository(AppDbContext _context) : ICategoryRepository
    {
        public async Task<int> Add(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            Category category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };
           await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return  category.Id;
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
          return await _context.Categories
                .Select(c => new CategoryDto
                {
                      Id = c.Id,
                      Name = c.Name,
                      Description = c.Description

                })
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsExistCategoryByName(string name, CancellationToken cancellationToken)
        {
          return await  _context.Categories.AnyAsync(c=>c.Name.ToLower()==name.ToLower());
        }
    }
}

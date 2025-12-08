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

        public async Task<Result<int>> Delete(int categryId, CancellationToken cancellationToken)
        {
            Category? category= await _context.Categories.FirstOrDefaultAsync(c => c.Id == categryId, cancellationToken);
            if (category ==null)
            {
                return Result<int>.Failure("همچین کتگوری ای موجود نیست.");
            }
            category.IsDeleted = true;
            int result= await _context.SaveChangesAsync();
            if (result<=0)
            {
                return Result<int>.Failure("تغییری رخ نداد.");
            }
            return Result<int>.Success(result);
        }

        public async Task<Result<int>> Edit(CategoryDto categoryDto, CancellationToken cancellationToken)
        {

            Category? category=await _context.Categories.FirstOrDefaultAsync(c=>c.Id== categoryDto.Id , cancellationToken);

            if (category==null)
            {
                return Result<int>.Failure("همچین کتگوری ای موجود نمیباشد.");
            }
           
            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            await _context.SaveChangesAsync();
            return Result<int>.Success(category.Id);
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

        public async Task<Result<CategoryDto>> GetById(int categryId, CancellationToken cancellationToken)
        {
            Category? category=  await  _context.Categories.FirstOrDefaultAsync(c=>c.Id== categryId , cancellationToken);
            if (category==null)
            {
                return Result<CategoryDto>.Failure("همچین دسته بندی ای موجود نیست.");
            }
            CategoryDto categoryDto = new CategoryDto 
            {
                Id= category.Id,
                Name = category.Name,
                Description = category.Description           
            };
            return Result<CategoryDto>.Success(categoryDto);
        }

        public async Task<bool> IsExistCategoryByName(string name, CancellationToken cancellationToken)
        {
          return await  _context.Categories.AnyAsync(c=>c.Name.ToLower()==name.ToLower());
        }


    }
}

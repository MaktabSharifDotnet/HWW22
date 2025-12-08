using App.Domain.Core._common;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Contract.CategoryAgg.Service;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.CategoryAgg
{
    public class CategoryService(ICategoryRepository _categoryRepository) : ICategoryService
    {
       
     

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
          return await _categoryRepository.GetAll(cancellationToken);
        }

       

        public async Task<Result<int>> Create(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            bool isExist = await _categoryRepository.IsExistCategoryByName(categoryDto.Name, cancellationToken);
            if (isExist)
            {
                return Result<int>.Failure("دسته بندی با این نام قبلا ثبت شده است.");
            }

            int categoryId= await _categoryRepository.Add(categoryDto, cancellationToken);
            if (categoryId<=0)
            {
                return Result<int>.Failure("خطایی رخ داده است دوباره تلاش کنید.");
            }
            return Result<int>.Success(categoryId);

        }
    }
}

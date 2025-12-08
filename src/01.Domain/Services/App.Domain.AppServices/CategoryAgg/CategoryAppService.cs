using App.Domain.Core._common;
using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Contract.CategoryAgg.Service;
using App.Domain.Core.Dtos.CategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.CategoryAgg
{
    public class CategoryAppService(ICategoryService _categoryService) : ICategoryAppService
    {
        public async Task<Result<int>> Create(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            return  await _categoryService.Create(categoryDto, cancellationToken);
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _categoryService.GetAll(cancellationToken);
        }
    }
}

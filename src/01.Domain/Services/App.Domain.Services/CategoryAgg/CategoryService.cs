using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Contract.CategoryAgg.Service;
using App.Domain.Core.Dtos.CategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.CategoryAgg
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
          return await categoryRepository.GetAll(cancellationToken);
        }
    }
}

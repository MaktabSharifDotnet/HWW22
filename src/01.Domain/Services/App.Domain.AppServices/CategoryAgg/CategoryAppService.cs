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
    public class CategoryAppService(ICategoryService categoryService) : ICategoryAppService
    {
        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await categoryService.GetAll(cancellationToken);
        }
    }
}

using App.Domain.Core._common;
using App.Domain.Core.Dtos.CategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.Service
{
    public interface ICategoryService
    {
        public Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);

        public  Task<Result<int>> Create(CategoryDto categoryDto, CancellationToken cancellationToken);

    }
}

using App.Domain.Core._common;
using App.Domain.Core.Dtos.CategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.AppService
{
    public interface ICategoryAppService
    {
        public Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);
        public Task<Result<int>> Create(CategoryDto categoryDto, CancellationToken cancellationToken);
        public Task<Result<CategoryDto>> GetById(int categryId, CancellationToken cancellationToken);
        public Task<Result<int>> Edit(CategoryDto categoryDto, CancellationToken cancellationToken);
        public Task<Result<int>> Delete(int categryId, CancellationToken cancellationToken);
    }
}

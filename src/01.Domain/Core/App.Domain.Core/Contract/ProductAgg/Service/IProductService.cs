using App.Domain.Core._common;
using App.Domain.Core.Contract.ProductAgg.Repository;
using App.Domain.Core.Dtos.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ProductAgg.Service
{
    public interface IProductService
    {
        public Task<ProductListDto> GetAll(int pageNumber, int pageSize, int? categoryId = null , CancellationToken cancellationToken=default);

        public Task<ProductDto?> GetById(int productId, CancellationToken cancellationToken);
        public Task<int> GetCountProduct(CancellationToken cancellationToken);
        public Task<Result<int>> Edit(ProductDto productDto, CancellationToken cancellationToken);


    }
}

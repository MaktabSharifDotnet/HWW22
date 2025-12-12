using App.Domain.Core._common;
using App.Domain.Core.Contract.ProductAgg.AppService;
using App.Domain.Core.Contract.ProductAgg.Service;
using App.Domain.Core.Dtos.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.ProductAgg
{
    public class ProductAppService(IProductService productService) : IProductAppService
    {
        public async Task<Result<int>> Create(ProductDto productDto, CancellationToken cancellationToken)
        {
           return await productService.Create(productDto, cancellationToken);
        }

        public async Task<Result<int>> Delete(int productId, CancellationToken cancellationToken)
        {
          return await  productService.Delete(productId, cancellationToken);
        }

        public async Task<Result<int>> Edit(ProductDto productDto, CancellationToken cancellationToken)
        {
          return await productService.Edit(productDto, cancellationToken);
        }

        public async Task<ProductListDto> GetAll(int pageNumber, int pageSize, int? categoryId = null, CancellationToken cancellationToken = default)
        {
            return await productService.GetAll(pageNumber, pageSize, categoryId, cancellationToken);
        }

        public async Task<ProductDto?> GetById(int productId  , CancellationToken cancellationToken)
        {
            return await productService.GetById(productId , cancellationToken);
        }
    }
}

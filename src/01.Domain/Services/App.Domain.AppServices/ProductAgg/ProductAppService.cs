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

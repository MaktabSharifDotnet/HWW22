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
        public List<ProductDto> GetAll(int? categoryId = null)
        {
            return  productService.GetAll(categoryId);
        }

        public ProductDto? GetById(int productId)
        {
            return productService.GetById(productId);
        }
    }
}

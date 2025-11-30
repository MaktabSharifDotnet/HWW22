using App.Domain.Core.Contract.ProductAgg.Repository;
using App.Domain.Core.Contract.ProductAgg.Service;
using App.Domain.Core.Dtos.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.ProductAgg
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public List<ProductDto> GetAll(int? categoryId = null)
        {
           return  productRepository.GetAll(categoryId);
        }
    }
}

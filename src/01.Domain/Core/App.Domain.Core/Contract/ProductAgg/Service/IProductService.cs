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
        public List<ProductDto> GetAll(int? categoryId = null);

        public ProductDto? GetById(int productId);
    }
}

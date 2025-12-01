using App.Domain.Core.Dtos.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ProductAgg.AppService
{
    public interface IProductAppService
    {
        public Task<List<ProductDto>> GetAll(int? categoryId = null , CancellationToken cancellationToken = default);
        public Task<ProductDto?> GetById(int productId , CancellationToken cancellationToken);
    }
}

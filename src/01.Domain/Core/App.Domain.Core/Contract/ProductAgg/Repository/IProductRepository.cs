using App.Domain.Core.Dtos.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.ProductAgg.Repository
{
    public interface IProductRepository
    {
        public  Task<ProductListDto> GetAll(int pageNumber, int pageSize, int? categoryId = null, CancellationToken cancellationToken = default);
        Task<ProductDto?> GetById(int productId, CancellationToken cancellationToken );
    }
}

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
        public List<ProductDto> GetAll(int? categoryId = null);
    }
}

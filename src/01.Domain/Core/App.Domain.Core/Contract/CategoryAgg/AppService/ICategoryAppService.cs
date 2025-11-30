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
        public List<CategoryDto> GetAll();
    }
}

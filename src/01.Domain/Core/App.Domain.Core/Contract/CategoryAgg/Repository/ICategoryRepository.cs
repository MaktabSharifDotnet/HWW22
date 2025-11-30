using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.Repository
{
    public interface ICategoryRepository
    {
         public List<CategoryDto> GetAll();
    }
}

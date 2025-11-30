using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.CategoryAgg
{
    public class CategoryRepository(AppDbContext _context) : ICategoryRepository
    {
        public List<CategoryDto> GetAll()
        {
          return  _context.Categories
                .Select(c => new CategoryDto
                {
                      Id = c.Id,
                      Name = c.Name,

                })
                .ToList();
        }
    }
}

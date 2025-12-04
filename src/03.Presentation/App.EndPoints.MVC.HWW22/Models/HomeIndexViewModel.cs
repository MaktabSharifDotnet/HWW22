using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Dtos.ProductAgg;
using App.Domain.Core.Entities;

namespace App.EndPoints.MVC.HWW22.Models
{
    public class HomeIndexViewModel
    {
        public List<CategoryDto> CategoryDtos { get; set; }

        
        public ProductListDto ProductResult { get; set; }

        public int? CurrentCategoryId { get; set; }
    }
}

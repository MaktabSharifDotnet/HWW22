using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Dtos.ProductAgg;
using App.Domain.Core.Entities;

namespace App.EndPoints.MVC.HWW22.Models
{
    public class HomeIndexViewModel
    {
        public List<CategoryDto> CategoryDtos { get; set; } = [];
        public List<ProductDto> ProductDtos { get; set; } = [];
    }
}

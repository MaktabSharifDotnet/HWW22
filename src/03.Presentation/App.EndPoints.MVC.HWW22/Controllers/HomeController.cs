using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Contract.ProductAgg.AppService;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Dtos.ProductAgg;
using App.EndPoints.MVC.HWW22.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    public class HomeController(IProductAppService productAppService
        ,ICategoryAppService categoryAppService) : Controller
    {
      

        public IActionResult Index(int categoryId)
        {

            List<ProductDto> productDtos= productAppService.GetAll(categoryId);
            List<CategoryDto> categoryDtos= categoryAppService.GetAll();
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel() 
            {
               CategoryDtos = categoryDtos , 
               ProductDtos = productDtos
            };

            return View(homeIndexViewModel);
        }

       
    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

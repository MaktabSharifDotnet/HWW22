using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Contract.ProductAgg.AppService;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Dtos.ProductAgg;
using App.EndPoints.MVC.HWW22.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    public class HomeController(IProductAppService productAppService
        ,ICategoryAppService categoryAppService) : Controller
    {


        public async Task<IActionResult> Index(int categoryId, int pageNumber = 1, CancellationToken cancellationToken = default)
        {
            
            int pageSize = 6;
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            ProductListDto productResult = await productAppService.GetAll(pageNumber, pageSize, categoryId, cancellationToken);

            List<CategoryDto> categoryDtos = await categoryAppService.GetAll(cancellationToken);

            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                CategoryDtos = categoryDtos,
                ProductResult = productResult,
                CurrentCategoryId = (categoryId == 0) ? null : categoryId
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

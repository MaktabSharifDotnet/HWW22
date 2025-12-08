using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.UserAgg;
using App.EndPoints.MVC.HWW22.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    public class CategoryController(ICategoryAppService _categoryAppService , ILogger<CategoryController> _logger) : Controller
    {

        
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            if (LocalStorage.LoginUser==null || LocalStorage.LoginUser.RoleEnum!=RoleEnum.Admin)
            {
                TempData["Error"]= "فقط کاربر ادمین به این صفحه  امکان دسترسی دارد.";
                return RedirectToAction("Index", "Account");
            }
            List<CategoryDto> categoryDtos= await _categoryAppService.GetAll(cancellationToken);

            return View(categoryDtos);
        }

        public IActionResult Create(CancellationToken cancellationToken) 
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryDto categoryDto,CancellationToken cancellationToken)
        {


            return RedirectToAction("Index");
        }



    }
}

using App.Domain.Core._common;
using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.UserAgg;
using App.EndPoints.MVC.HWW22.Constants;
using App.EndPoints.MVC.HWW22.Models;
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
                _logger.LogWarning("Unauthorized access attempt to Category Index page.");
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
        public async Task<IActionResult> Create(CategoryDto categoryDto,CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                return View("Create", categoryDto);
            }

            Result<int> result = await _categoryAppService.Create(categoryDto, cancellationToken);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed Create attempt for Category: {Name}. Reason: {Message}",
                           categoryDto.Name,
                           result.Message);
                ModelState.AddModelError(string.Empty, result.Message);
                return View(categoryDto);
            }
            _logger.LogInformation("Category {Name} created successfully with New ID: {CategoryId}",
                          categoryDto.Name,
                          result.Data);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int categoryId , CancellationToken cancellationToken) 
        {
            Result<CategoryDto> result=await _categoryAppService.GetById(categoryId , cancellationToken);
            if (!result.IsSuccess) 
            {

                _logger.LogWarning("Category edit failed. CategoryId: {CategoryId} not found. Error: {Message}", categoryId, result.Message);

                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            return View(result.Data);
        
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDto categoryDto, CancellationToken cancellationToken) 
        {

            if (!ModelState.IsValid)
            {
                return View("Edit", categoryDto);
            }

            Result<int> result=await _categoryAppService.Edit(categoryDto, cancellationToken);

            if (!result.IsSuccess) 
            {
                _logger.LogWarning("Failed Edit attempt for Category: {Name}. Reason: {Message}",
                        categoryDto.Name,
                        result.Message);
                ModelState.AddModelError(string.Empty, result.Message);
                return View(categoryDto);
            }

            _logger.LogInformation("Category {Name} updated successfully with ID: {CategoryId}",
                        categoryDto.Name,
                        result.Data);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int categoryId, CancellationToken cancellationToken) 
        {

            Result<int> result=await _categoryAppService.Delete(categoryId, cancellationToken);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to delete Category with ID: {CategoryId}. Reason: {Message}",
                            categoryId,
                            result.Message);
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }
            _logger.LogInformation("Category with ID: {CategoryId} was soft-deleted successfully.", categoryId);
            TempData["Success"] = "دسته‌بندی با موفقیت حذف شد.";
            return RedirectToAction("Index");
          
        }



    }
}

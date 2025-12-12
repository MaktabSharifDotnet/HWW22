using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Contract.ProductAgg.AppService;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Dtos.ProductAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.UserAgg;
using App.EndPoints.MVC.HWW22.Areas.Admin.Models;
using App.EndPoints.MVC.HWW22.Constants;
using App.EndPoints.MVC.HWW22.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    public class ProductController(ILogger<ProductController> _logger
        , IProductAppService _productAppService , ICategoryAppService _categoryAppService) : Controller
    {
       
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, int? categoryId = null, CancellationToken cancellationToken = default)
        {
            if (LocalStorage.LoginUser == null || LocalStorage.LoginUser.RoleEnum != RoleEnum.Admin)
            {
                TempData["Error"] = "فقط کاربر ادمین به این صفحه  امکان دسترسی دارد.";
                _logger.LogWarning("Unauthorized access attempt to Product Index page.");
                return RedirectToAction("Index", "Account");
            }

            ProductListDto productListDto = await _productAppService.GetAll(pageNumber ,pageSize , categoryId,cancellationToken);
            return View(productListDto);


        }


        public async Task<IActionResult> Edit(int productId , CancellationToken cancellationToken)
        {
            if (LocalStorage.LoginUser == null || LocalStorage.LoginUser.RoleEnum != RoleEnum.Admin)
            {
                TempData["Error"] = "فقط کاربر ادمین به این صفحه  امکان دسترسی دارد.";
                _logger.LogWarning("Unauthorized access attempt to Product Edit page.");
                return RedirectToAction("Index", "Account");
            }
            try
            {
                ProductDto? productDto = await _productAppService.GetById(productId, cancellationToken);
                ProductViewModel productViewModel = new ProductViewModel
                {
                    Id = productDto!.Id,
                    Title = productDto.Title,
                    CategoryName = productDto.CategoryName,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    ImagePath=productDto.Image,
                    CreatedAt=productDto.CreatedAt,
                    Inventory=productDto.Inventory,
                    CategoryId=productDto.CategoryId
                };
                List<CategoryDto> categoryDtos = await _categoryAppService.GetAll(cancellationToken);
                ViewBag.Categories = new SelectList(categoryDtos, "Id", "Name", productDto.CategoryId);
                return View(productViewModel);
            }
            catch (Exception ex) 
            {
               _logger.LogError(ex.Message, "Error occurred while accessing Product Edit page.");
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            

        }

        [HttpPost]
       
        public async Task<IActionResult> Edit(ProductViewModel model, CancellationToken cancellationToken)
        {
          
           

            if (!ModelState.IsValid)
            {
              
                var categoryDtos = await _categoryAppService.GetAll(cancellationToken);
                ViewBag.Categories = new SelectList(categoryDtos, "Id", "Name", model.CategoryId);
                return View(model);
            }

            try
            {
                
                string finalImageName = model.ImagePath; 

                if (model.Image != null)
                {
                   
                    var uploadedName = model.Image.UploadFile("ProductsAgg");
                    if (uploadedName != null)
                    {
                        finalImageName = uploadedName;
                       
                    }
                }

               
                var productDto = new ProductDto
                {
                    Id = model.Id,
                    Title = model.Title,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    Price = model.Price,
                    Inventory = model.Inventory,
                    Image = finalImageName, 
                    CreatedAt=DateTime.Now.Date
                };

               var result=await _productAppService.Edit(productDto, cancellationToken);
                if (!result.IsSuccess)
                {
                    _logger.LogError(result.Message, "Error in Edit POST");
                    TempData["Error"] = "خطا در ویرایش محصول";
                    return RedirectToAction("Index");
                }

           
                _logger.LogInformation("Product with ID {ProductId} edited successfully.", model.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Edit POST");
                TempData["Error"] = "خطا در ویرایش محصول";
                return RedirectToAction("Index");
            }
        }
    }
}

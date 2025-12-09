using App.Domain.Core._common;
using App.Domain.Core.Contract.UserAgg.AppService;
using App.Domain.Core.Dtos.UserAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.UserAgg;
using App.EndPoints.MVC.HWW22.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    public class CustomerController(ILogger<CustomerController> _logger , IUserAppService _userAppService) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            if (LocalStorage.LoginUser == null || LocalStorage.LoginUser.RoleEnum != RoleEnum.Admin)
            {
                TempData["Error"] = "فقط کاربر ادمین به این صفحه  امکان دسترسی دارد.";
                _logger.LogWarning("Unauthorized access attempt to Customer Index page.");
                return RedirectToAction("Index", "Account");
            }

            List<UserDto> userDtos = await _userAppService.GetAll(cancellationToken);
            _logger.LogInformation("Accessed Customer Index page successfully.");
            return View(userDtos);
        }

        public async Task<IActionResult> Detail(int userId, CancellationToken cancellationToken) 
        {
            if (LocalStorage.LoginUser == null || LocalStorage.LoginUser.RoleEnum != RoleEnum.Admin)
            {
                TempData["Error"] = "فقط کاربر ادمین به این صفحه  امکان دسترسی دارد.";
                _logger.LogWarning("Unauthorized access attempt to Customer Detail page.");
                return RedirectToAction("Index", "Account");
            }
            Result<UserDetailDto> result=await _userAppService.GetDetailById(userId, cancellationToken);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                _logger.LogWarning("Failed to access Customer Detail page: {Message}", result.Message);
                return RedirectToAction("Index");
            }
            _logger.LogInformation("Accessed Customer Detail page successfully for UserId: {UserId}", userId);
            return View(result.Data);
        
        }
    }
}

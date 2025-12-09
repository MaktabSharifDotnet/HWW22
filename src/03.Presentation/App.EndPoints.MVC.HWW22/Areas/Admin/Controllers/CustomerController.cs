using App.Domain.Core.Contract.UserAgg.AppService;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.UserAgg;
using App.EndPoints.MVC.HWW22.Constants;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HWW22.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    public class CustomerController(ILogger<CustomerController> _logger , IUserAppService _userAppService) : Controller
    {
        public IActionResult Index(CancellationToken cancellationToken)
        {
            if (LocalStorage.LoginUser == null || LocalStorage.LoginUser.RoleEnum != RoleEnum.Admin)
            {
                TempData["Error"] = "فقط کاربر ادمین به این صفحه  امکان دسترسی دارد.";
                _logger.LogWarning("Unauthorized access attempt to Customer Index page.");
                return RedirectToAction("Index", "Account");
            }



            return View();
        }
    }
}

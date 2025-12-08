using App.Domain.Core.Contract.UserAgg.AppService;
using App.EndPoints.MVC.HWW22.Constants;
using App.EndPoints.MVC.HWW22.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    public class AccountController(IUserAppService _userAppService , ILogger<AccountController> _logger) : Controller
    {
        
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", adminLoginViewModel);
            }

            
            int userId = await _userAppService.Login(adminLoginViewModel.Username, adminLoginViewModel.Password, cancellationToken);
            if (userId==0)
            {

                _logger.LogWarning("Failed login attempt for username: {Username}", adminLoginViewModel.Username);
                TempData["Error"] ="نام کاربری یا رمز عبور اشتباه است.";
                return View("Index", adminLoginViewModel);
                
            }
            _logger.LogInformation("Admin user {Username} logged in successfully with UserID: {UserId}",
                                       adminLoginViewModel.Username,
                                       userId);

                return RedirectToAction("Index", "Category");
        

            
        }


    }
}

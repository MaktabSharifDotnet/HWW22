using App.Domain.Core.Contract.UserAgg.AppService;
using App.EndPoints.MVC.HWW22.Constants;
using App.EndPoints.MVC.HWW22.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    public class AccountController(IUserAppService _userAppService , ILogger<AccountController> _logger
        , SignInManager<IdentityUser<int>> _signInManager
        , UserManager<IdentityUser<int>> _userManager


        ) : Controller
    {
        
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Category"); 
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", adminLoginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(adminLoginViewModel.Username, adminLoginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                _logger.LogInformation("Admin user {Username} logged in.", adminLoginViewModel.Username);

                return RedirectToAction("Index", "Category");
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                TempData["Error"] = "حساب کاربری شما به دلیل تلاش‌های ناموفق قفل شده است.";
                return View("Index", adminLoginViewModel);
            }


            TempData["Error"] = "نام کاربری یا رمز عبور اشتباه است.";
            return View("Index", adminLoginViewModel);
            
            
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index");
        }


    }
}

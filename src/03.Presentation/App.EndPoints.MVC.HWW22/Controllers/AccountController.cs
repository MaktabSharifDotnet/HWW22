using App.Domain.AppServices.CartAgg;
using App.Domain.AppServices.UserAgg;
using App.Domain.Core.Contract.CartAgg.AddService;
using App.Domain.Core.Contract.UserAgg.AppService;
using App.Domain.Core.Dtos.CartAgg;
using App.Domain.Core.Entities;
using App.EndPoints.MVC.HWW22.Extensions;
using App.EndPoints.MVC.HWW22.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    public class AccountController(
        IUserAppService userAppService 
        , ICartAppService cartAppService,
         ILogger <AccountController > _logger
        ,
         SignInManager<IdentityUser<int>> _signInManager
        , UserManager<IdentityUser<int>> _userManager) : Controller

    {
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel loginViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            try
            {
                
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, isPersistent: true, lockoutOnFailure: false);

              
                if (result.Succeeded)
                {
                    var identityUser = await _userManager.FindByNameAsync(loginViewModel.Username);

                    
                    if (identityUser == null)
                    {
                        TempData["Error"] = "کاربر یافت نشد.";
                        return View(loginViewModel);
                    }

                    var domainUserId = await userAppService.GetUserIdByIdentityId(identityUser.Id, cancellationToken);

                    if (domainUserId > 0)
                    {
                   
                        List<CartItemDto>? sessionCartItems = HttpContext.Session.GetObject<List<CartItemDto>>("UserCart");
                        if (sessionCartItems != null && sessionCartItems.Any())
                        {
                            await cartAppService.MergeCart(domainUserId, sessionCartItems, cancellationToken);
                            HttpContext.Session.Remove("UserCart");
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }

                TempData["Error"] = "نام کاربری یا رمز عبور اشتباه است.";
                return View(loginViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(loginViewModel);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("UserCart"); 
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated==true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            var identityUser = new IdentityUser<int>
            {
                UserName = model.Username,
                PhoneNumber = model.Mobile,
                EmailConfirmed = true 
            };

            
            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                try
                {
                  
                    await userAppService.RegisterUser(model.Username, identityUser.Id, cancellationToken);

                    
                    await _signInManager.SignInAsync(identityUser, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    
                    await _userManager.DeleteAsync(identityUser);

                    ModelState.AddModelError("", "خطایی در ثبت اطلاعات کاربری رخ داد. لطفا مجدد تلاش کنید.");
                   _logger.LogError(ex, "Error in syncing user to domain");
                    return View(model);
                }
            }

           
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}

using App.Domain.AppServices.CartAgg;
using App.Domain.AppServices.UserAgg;
using App.Domain.Core.Contract.CartAgg.AddService;
using App.Domain.Core.Contract.UserAgg.AppService;
using App.Domain.Core.Dtos.CartAgg;
using App.Domain.Core.Dtos.UserAgg;
using App.Domain.Core.Entities;
using App.EndPoints.MVC.HWW22.Extensions;
using App.EndPoints.MVC.HWW22.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    [Authorize]
    public class AccountController(
        IUserAppService userAppService 
        , ICartAppService cartAppService,
         ILogger <AccountController > _logger
        , UserManager<IdentityUser<int>> _userManager
        , SignInManager<IdentityUser<int>> _signInManager
         ) : Controller

    {

    
        public IActionResult AccessDenied(string returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }


            if (!string.IsNullOrEmpty(returnUrl) && returnUrl.ToLower().Contains("admin"))
            {
                return RedirectToAction("Index", "Account", new { area = "Admin", ReturnUrl = returnUrl });
            }

          
            return RedirectToAction("Login", "Account", new { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel loginViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            try
            {

                var result = await userAppService.PasswordSignIn(loginViewModel.Username, loginViewModel.Password, isPersistent: true, lockoutOnFailure: false);


                if (result.Succeeded)
                {
                    var identityUser = await userAppService.FindByName(loginViewModel.Username);

                    var domainUserId = await userAppService.GetUserIdByIdentityId(identityUser!.Id, cancellationToken);

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
            await userAppService.SignOut();
            HttpContext.Session.Remove("UserCart");
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
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


            var result = await userAppService.Create(identityUser, model.Password);

            if (result.Succeeded)
            {

                await userAppService.AddToRole(identityUser, "Customer");
                try
                {

                    await userAppService.RegisterUser(model.Username, identityUser.Id, cancellationToken);
                    await userAppService.SignIn(identityUser);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {

                    await userAppService.Delete(identityUser);
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




        public async Task<IActionResult> Profile()
        {


            var user = await userAppService.GetUser(User);

            if (user == null) return RedirectToAction("Login");


            var model = new EditProfileViewModel

            {

                Username = user.UserName,

                PhoneNumber = user.PhoneNumber,

                Email = user.Email

            };


            return View(model);

        }
        public async Task<IActionResult> EditProfile()
        {


            var user = await userAppService.GetUser(User);

            if (user == null) return RedirectToAction("Login");


            var model = new EditProfileViewModel

            {

                Username = user.UserName,

                PhoneNumber = user.PhoneNumber,

                Email = user.Email

            };


            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await userAppService.GetUser(User);
            if (user == null) return RedirectToAction("Login");


            bool usernameChanged = model.Username != user.UserName;


            user.PhoneNumber = model.PhoneNumber;

            user.UserName = model.Username;
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                user.Email = null;
            }
            else
            {
                user.Email = model.Email;
            }

            var result = await userAppService.Update(user);

            if (result.Succeeded)
            {

                if (usernameChanged)
                {
                    try
                    {

                        await userAppService.ChangeDatabaseUsername(user.Id, model.Username, cancellationToken);
                    }
                    catch (Exception ex)
                    {

                        _logger.LogError(ex, "Identity updated but Domain User update failed.");
                        TempData["Warning"] = "اطلاعات لاگین بروز شد اما در پروفایل ممکن است نام قدیم نمایش داده شود.";
                    }
                }

                await userAppService.RefreshSignIn(user);

                TempData["Success"] = "پروفایل با موفقیت بروزرسانی شد.";
                return RedirectToAction("Profile");
            }


            foreach (var error in result.Errors)
            {

                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
     

       
        public IActionResult ChangePassword()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

 
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
               
                await _signInManager.RefreshSignInAsync(user);

                TempData["Success"] = "رمز عبور شما با موفقیت تغییر کرد.";
                return RedirectToAction("Profile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

    }
}

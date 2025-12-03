using App.Domain.Core.Contract.CartAgg.AddService;
using App.Domain.Core.Contract.UserAgg.AppService;
using App.Domain.Core.Dtos.CartAgg;
using App.Domain.Core.Entities;
using App.EndPoints.MVC.HWW22.Extensions;
using App.EndPoints.MVC.HWW22.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    public class AccountController(IUserAppService userAppService , ICartAppService cartAppService ) : Controller
    {
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel , CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            try 
            {
                
                int userId = await userAppService.Login(loginViewModel.Username, loginViewModel.Password, cancellationToken);
                if (userId <= 0) 
                {
                    TempData["Error"] = "خطایی رخ داد دوباره تلاش کنید.";
                    return View(loginViewModel);
                }


                List<CartItemDto>? sessionCartItems = HttpContext.Session.GetObject<List<CartItemDto>>("UserCart");

                
                if (sessionCartItems != null && sessionCartItems.Any())
                {
                   
                    await cartAppService.MergeCart(userId, sessionCartItems, cancellationToken);


                    HttpContext.Session.Remove("UserCart");
                }
                return RedirectToAction("index", "Home");

            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(loginViewModel);
            }

        }

        public IActionResult LogOut() 
        {
            LocalStorage.LoginUser= null;
            return RedirectToAction("Login");
        }
    }
}

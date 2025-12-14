using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Contract.UserAgg.AppService;
using App.Domain.Core.Contract.UserAgg.Service;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    [Authorize]
    public class CheckOutController(IOrderAppService _orderAppService , UserManager<IdentityUser<int>> _userManager
        , IUserAppService _userAppService) : Controller
    {
        public async Task<IActionResult> CheckOut(int cartId, CancellationToken cancellationToken)
        {

            if (User.Identity is null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
             
                var identityUser = await _userManager.GetUserAsync(User);
                if (identityUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                
                var userId = await _userAppService.GetUserIdByIdentityId(identityUser.Id, cancellationToken);

                int result = await _orderAppService.CheckOut(userId, cartId, cancellationToken);

                if (result <= 0)
                {
                    TempData["Error"] = "خطایی رخ داد دوباره تلاش کنید.";
                    
                    return RedirectToAction("Index", "Cart");
                }

                TempData["Success"] = "سفارش شما با موفقیت ثبت شد";

                return View();
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}

using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Contract.UserAgg.Service;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    public class CheckOutController(IOrderAppService _orderAppService) : Controller
    {
        public async Task<IActionResult> CheckOut(int userId , int cartId , CancellationToken cancellationToken)
        {
            if (LocalStorage.LoginUser==null || LocalStorage.LoginUser.Id<=0 ||LocalStorage.LoginUser.Id!=userId)
            {
                return RedirectToAction("Login","Account");
            }

            try
            {
                int result = await _orderAppService.CheckOut(userId, cartId, cancellationToken);
                if (result <= 0) 
                {
                    TempData["Error"] = "خطایی رخ داد دوباره تلاش کنید.";
                    return RedirectToAction("Index","Cart");
                }

                TempData["Success"] = "سفارش شما با موفقیت ثبت شد";
                ViewBag.OrderId = result;
                return View();
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("",ex.Message);
                return View();
            }
            
        }
    }
}

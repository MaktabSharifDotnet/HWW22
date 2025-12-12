using App.Domain.Core._common;
using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Contract.OrderItemAgg.AppService;
using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Dtos.OrderItemAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.UserAgg;
using App.EndPoints.MVC.HWW22.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Areas.Admin.Controllers
{

    [Area(AreaConstants.Admin)]
    public class OrderController(ILogger<OrderController> _logger , IOrderAppService _orderAppService,
        IOrderItemAppService _orderItemAppService) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken )
        {

            if (LocalStorage.LoginUser == null || LocalStorage.LoginUser.RoleEnum != RoleEnum.Admin)
            {
                TempData["Error"] = "فقط کاربر ادمین به این صفحه  امکان دسترسی دارد.";
                _logger.LogWarning("Unauthorized access attempt to Order Index page.");
                return RedirectToAction("Index", "Account");
            }

             List<OrderDto> orderDtos=await _orderAppService.GetOrderDtos(cancellationToken);
            _logger.LogInformation("Retrieved {OrderCount} orders for display in Order Index page.", orderDtos.Count);

            return View(orderDtos);
        }

        public async Task<IActionResult> Detail(int orderId,CancellationToken cancellationToken) 
        {

            if (LocalStorage.LoginUser == null || LocalStorage.LoginUser.RoleEnum != RoleEnum.Admin)
            {
                TempData["Error"] = "فقط کاربر ادمین به این صفحه  امکان دسترسی دارد.";
                _logger.LogWarning("Unauthorized access attempt to OrderItem Detail page.");
                return RedirectToAction("Index", "Account");
            }

            var  result= await _orderItemAppService.GetOrderItemDtos(orderId,cancellationToken);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
               _logger.LogWarning("Failed to retrieve order items for OrderId {OrderId}: {WarningMessage}", orderId, result.Message);
                return RedirectToAction("Index", "Order");
            }
            _logger.LogInformation("Retrieved order items for OrderId {OrderId} successfully.", orderId);
            return View(result.Data);
        }
    }
}

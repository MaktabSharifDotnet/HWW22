using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.UserAgg;
using App.EndPoints.MVC.HWW22.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace App.EndPoints.MVC.HWW22.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    [Authorize(Roles = "Admin")]
    public class DashboardController(ILogger<DashboardController> _logger , IOrderAppService _orderAppService) : Controller
    {
      
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {


            DashboardDataDto dashboardDataDto=await _orderAppService.GetDashboardData(cancellationToken);
            _logger.LogInformation("Admin accessed the Dashboard page successfully.");
            return View(dashboardDataDto);
        }
    }
}

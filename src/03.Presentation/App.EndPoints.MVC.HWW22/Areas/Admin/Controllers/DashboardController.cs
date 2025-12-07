using Microsoft.AspNetCore.Mvc;
using App.EndPoints.MVC.HWW22.Constants;


namespace App.EndPoints.MVC.HWW22.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Area(AreaConstants.Admin)]
        public IActionResult Index()
        {
            return View();
        }
    }
}

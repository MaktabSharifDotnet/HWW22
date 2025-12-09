using App.EndPoints.MVC.HWW22.Constants;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HWW22.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    public class CustomerController(ILogger<CustomerController> _logger) : Controller
    {
        public IActionResult Index()
        {


            return View();
        }
    }
}

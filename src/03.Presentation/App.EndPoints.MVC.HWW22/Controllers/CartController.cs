using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Add(int productId)
        {

            return View();
        }
    }
}

using App.Domain.Core.Contract.ProductAgg.AppService;
using App.Domain.Core.Dtos.ProductAgg;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    public class ProductController(IProductAppService productAppService) : Controller
    {
        public IActionResult Detail(int productId)
        {
            try
            {
                ProductDto? productDto = productAppService.GetById(productId);
                return View(productDto);
            }
            catch (Exception ex) 
            {
                TempData["Warning"]=ex.Message;
                return RedirectToAction("index" , "Home");
            }
          

          
        }
    }
}

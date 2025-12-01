using App.Domain.Core.Contract.ProductAgg.AppService;
using App.Domain.Core.Dtos.ProductAgg;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    public class ProductController(IProductAppService productAppService) : Controller
    {
        public async Task<IActionResult> Detail(int productId , CancellationToken cancellationToken)
        {
            try
            {
                ProductDto? productDto = await productAppService.GetById(productId , cancellationToken);
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

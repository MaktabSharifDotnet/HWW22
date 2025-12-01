using App.Domain.Core.Contract.ProductAgg.AppService;
using App.Domain.Core.Dtos.CartAgg;
using App.EndPoints.MVC.HWW22.Extensions;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    public class CartController(IProductAppService productAppService) : Controller
    {
        private const string CartSessionKey = "UserCart";

        public async Task<IActionResult> AddToCart(int productId ,CancellationToken cancellationToken)
        {
            try
            {
                var product = await productAppService.GetById(productId, cancellationToken);
                if (product.Inventory <= 0)
                {
                    TempData["Error"] = "این کالا ناموجود است.";

                    return RedirectToAction("Index", "Home");

                }
                List<CartItemDto> cart = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey)
                                   ?? new List<CartItemDto>();

                CartItemDto? existingItem = cart.FirstOrDefault(c => c.ProductId == productId);

                if (existingItem != null)
                {
                   
                    if (existingItem.Count >= product.Inventory)
                    {
                        TempData["Warning"] = "تعداد درخواستی بیشتر از موجودی انبار است.";
                        return RedirectToAction("Index", "Home");
                    }

                    existingItem.Count++;
                }
                else
                {
                    cart.Add(new CartItemDto
                    {
                        ProductId = product.Id,
                        Title = product.Title,
                        Image = product.Image,
                        Price = product.Price,
                        Count = 1
                    });
                }

                HttpContext.Session.SetObject(CartSessionKey, cart);

                
                TempData["Success"] = "کالا به سبد اضافه شد";
                return RedirectToAction("Index");

            }
            catch (Exception ex) 
            {
                TempData["Warning"] = ex.Message;
                return RedirectToAction("Index" , "Home");
            }
          
        }

        public async Task<IActionResult> Increase(int productId , CancellationToken cancellationToken)
        {

            try
            {
                
                var product = await productAppService.GetById(productId, cancellationToken);


                var cart = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey);
                if (cart != null)
                {
                    var item = cart.FirstOrDefault(c => c.ProductId == productId);

                    if (item != null)
                    {
                        if (item.Count < product.Inventory)
                        {
                            item.Count++;
                            HttpContext.Session.SetObject(CartSessionKey, cart);
                        }
                        else
                        {
                            TempData["Warning"] = "موجودی انبار کافی نیست.";
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                TempData["Warning"] =ex.Message;
                return RedirectToAction("Index" , "Home");
            }

         
        }

        public IActionResult Decrease(int productId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey);
            if (cart != null)
            {
                var item = cart.FirstOrDefault(c => c.ProductId == productId);
                if (item != null)
                {
                    item.Count--;
                    if (item.Count == 0) 
                    { cart.Remove(item); }
                    HttpContext.Session.SetObject(CartSessionKey, cart);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey);
            if (cart != null)
            {
                var item = cart.FirstOrDefault(c => c.ProductId == productId);
                if (item != null)
                {
                    cart.Remove(item);
                    HttpContext.Session.SetObject(CartSessionKey, cart);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey)
                       ?? new List<CartItemDto>();
            return View(cart);
        }
    }
}
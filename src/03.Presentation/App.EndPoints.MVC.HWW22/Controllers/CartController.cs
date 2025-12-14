using App.Domain.AppServices.CartAgg;
using App.Domain.Core.Contract.CartAgg.AddService;
using App.Domain.Core.Contract.ProductAgg.AppService;
using App.Domain.Core.Contract.UserAgg.AppService; 
using App.Domain.Core.Dtos.CartAgg;
using App.EndPoints.MVC.HWW22.Extensions;
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HWW22.Controllers
{
    public class CartController(
        IProductAppService productAppService,
        ICartAppService cartAppService,
        UserManager<IdentityUser<int>> _userManager, 
        IUserAppService _userAppService 
        ) : Controller
    {
        private const string CartSessionKey = "UserCart";

      
        private async Task<int> GetDomainUserIdAsync(CancellationToken cancellationToken)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var identityUser = await _userManager.GetUserAsync(User);
                if (identityUser != null)
                {
                    return await _userAppService.GetUserIdByIdentityId(identityUser.Id, cancellationToken);
                }
            }
            return 0; 
        }
      
        public async Task<IActionResult> AddToCart(int productId, CancellationToken cancellationToken)
        {
            try
            {
                var product = await productAppService.GetById(productId, cancellationToken);
                if (product!.Inventory <= 0)
                {
                    TempData["Error"] = "این کالا ناموجود است.";
                    return RedirectToAction("Index", "Home");
                }

              
                int currentUserId = await GetDomainUserIdAsync(cancellationToken);

                
                if (currentUserId > 0)
                {
                    int cartId = await cartAppService.Add(currentUserId, productId, cancellationToken);
                    if (cartId <= 0)
                    {
                        TempData["Error"] = "خطایی رخ داد دوباره تلاش کنید";
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["Success"] = "کالا به سبد اضافه شد";
                    return RedirectToAction("Index");
                }
            
                else
                {
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
            }
            catch (Exception ex)
            {
                TempData["Warning"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Increase(int productId, CancellationToken cancellationToken)
        {
            try
            {
               
                var product = await productAppService.GetById(productId, cancellationToken);
                if (product == null)
                {
                    TempData["Error"] = "محصول یافت نشد.";
                    return RedirectToAction("Index");
                }

            
                int currentUserId = await GetDomainUserIdAsync(cancellationToken);

                if (currentUserId > 0)
                {
                  
                    int result = await cartAppService.Add(currentUserId, productId, cancellationToken);

                   
                    if (result <= 0)
                    {
                        TempData["Warning"] = "موجودی انبار کافی نیست یا خطایی رخ داد.";
                    }
                }
             
                else
                {
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
                }

            
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Warning"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Decrease(int productId, CancellationToken cancellationToken)
        {
            int currentUserId = await GetDomainUserIdAsync(cancellationToken);

            
            if (currentUserId > 0)
            {
                int result = await cartAppService.DecreaseItem(currentUserId, productId, cancellationToken);
                if (result < 0)
                {
                    TempData["Error"] = "خطایی رخ داد دوباره تلاش کنید.";
                    return RedirectToAction("Index");
                }
            }
           
            else
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
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int productId, CancellationToken cancellationToken)
        {
            int currentUserId = await GetDomainUserIdAsync(cancellationToken);

            if (currentUserId > 0)
            {
                try
                {
                    int result = await cartAppService.Remove(currentUserId, productId, cancellationToken);
                    if (result < 0)
                    {
                        TempData["Warning"] = "خطایی رخ داد دوباره تلاش کنید.";
                        return RedirectToAction("Index");
                    }

                    TempData["Success"] = "با موفقیت حذف شد";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Warning"] = ex.Message;
                    return RedirectToAction("Index");
                }
            }
            else
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
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            List<CartItemDto> cartItems = new();
            ViewBag.UserId = 0;
            ViewBag.CartId = 0;

            int currentUserId = await GetDomainUserIdAsync(cancellationToken);

            if (currentUserId > 0)
            {
                ViewBag.UserId = currentUserId;
              
                cartItems = await cartAppService.GetUserCartItems(currentUserId, cancellationToken);

                if (cartItems.Any())
                {
                    ViewBag.CartId = cartItems.First().CartId;
                }
            }
            else
            {
               
                cartItems = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey)
                            ?? new List<CartItemDto>();
            }

            return View(cartItems);
        }
    }
}
using Business.Restaurant;
using Core.Constants;
using Core.Models;
using Core.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    [Authorize(Roles = $"{Constants.ROLE.ADMIN},{Constants.ROLE.MEMBER}")]
    public class CartController : Controller
    {
        private readonly ICartBusiness _cartBusiness;
        public CartController(ICartBusiness cartBusiness)
        {
            _cartBusiness = cartBusiness;
        }

        public IActionResult Index()
        {
            var model = _cartBusiness.GetCart();

            if (model is null)
            {
                model = new Cart();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] AddToCartRequest request)
        {
            var isSuccess = _cartBusiness.AddToCart(request.id, request.amount);

            return Json(new { isSuccess });
        }

        [HttpPost]
        public IActionResult DeleteCartItem([FromBody] AddToCartRequest request)
        {
            var isSuccess = _cartBusiness.DeleteCartItem(request.id);

            return Json(new { isSuccess });
        }
    }
}

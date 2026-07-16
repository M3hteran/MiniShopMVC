using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShopMVC.Services.Interfaces;
using System.Security.Claims;

namespace MiniShopMVC.Controllers
{
	[Authorize]
	public class CartController : BaseController
	{
		private readonly ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		public async Task<IActionResult> Index()
		{
			var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (!int.TryParse(userIdValue, out var userId))
			{
				return RedirectToAction("Login", "Account");
			}

			var cart = await _cartService.GetCartByUserIdAsync(userId);

			return View(cart);

		}

		//-----------------------------------------------------POST--------------------------------------------------

		[HttpPost]
		public async Task<IActionResult> AddToCart(int productId)
		{
			var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!int.TryParse(userIdValue, out var userId))
			{
				return RedirectToAction("Login", "Account");
			}

			var result = await _cartService.AddToCartAsync(userId, productId);

			if (!result)
			{
				TempData["Error"] = "Ürün Bulunamadı";
				return RedirectToAction("Index", "Product");
			}

			TempData["Success"] = "Ürün Sepete eklendi";

			return RedirectToAction("Index", "Product");
		}

		[HttpPost]

		public async Task<IActionResult> Increase(int productId)
		{
			var userId = GetCurrentUserId();

			if (userId == null)
			{
				return RedirectToAction("Login", "Account");
			}

			await _cartService.IncreaseQuantityAsync(userId.Value, productId);

			return RedirectToAction("Index");
		}

		[HttpPost]

		public async Task<IActionResult> Decrease(int productId)
		{
			var userId = GetCurrentUserId();

			if (userId == null)
			{
				return RedirectToAction("Login", "Account");
			}

			await _cartService.DecreaseQuantityAsync(userId.Value, productId);

			return RedirectToAction("Index");

		}

		[HttpPost]
		public async Task<IActionResult> Remove(int productId)
		{
			var userId = GetCurrentUserId();

			if (userId == null)
			{
				return RedirectToAction("Login", "Account");

			}

			await _cartService.RemoveFromCartAsync(userId.Value, productId);

			return RedirectToAction("Index");

		}
	}
}

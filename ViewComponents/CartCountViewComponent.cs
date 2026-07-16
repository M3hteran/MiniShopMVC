using Microsoft.AspNetCore.Mvc;
using MiniShopMVC.Services.Interfaces;
using System.Security.Claims;

namespace MiniShopMVC.ViewComponents
{
	public class CartCountViewComponent : ViewComponent
	{
		private readonly ICartService _cartService;

		public CartCountViewComponent(ICartService cartService)
		{
			_cartService = cartService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			if (UserClaimsPrincipal.Identity?.IsAuthenticated != true)
			{
				return Content("0");
			}

			var userIdValue = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

			if (!int.TryParse(userIdValue, out var userId))
			{
				return Content("0");
			}

			var count = await _cartService.GetCartItemCountAsync(userId);
			return Content(count.ToString());
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniShopMVC.Controllers
{
	public abstract class BaseController : Controller
	{
		protected int? GetCurrentUserId()
		{
			var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (int.TryParse(userIdValue, out var userId))
			{
				return userId;
			}

			return null;
		}
	}
}

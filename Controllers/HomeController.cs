using Microsoft.AspNetCore.Mvc;


namespace MiniShopMVC.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{

			return RedirectToAction("Index", "Product");
		}
	}
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShopMVC.Models;
using MiniShopMVC.Repositories.Interfaces;
using MiniShopMVC.ViewModels;

namespace MiniShopMVC.Controllers
{
	[Authorize(Roles = "Admin")]
	public class DashboardController : Controller
	{
		private readonly IRepository<Product> _productRepository;
		private readonly IRepository<AppUser> _appUserRepository;
		private readonly IRepository<Order> _orderRepository;

		public DashboardController(IRepository<Product> productRepository, IRepository<AppUser> appUserRepository, IRepository<Order> orderRepository)
		{
			_productRepository = productRepository;
			_appUserRepository = appUserRepository;
			_orderRepository = orderRepository;
		}

		public async Task<IActionResult> Index()
		{
			var products = await _productRepository.GetAllAsync();
			var orders = await _orderRepository.GetAllAsync();
			var users = await _appUserRepository.GetAllAsync();

			var model = new DashboradViewModel
			{
				ProductCount = products.Count,
				UserCount = users.Count,
				OrderCount = orders.Count,
				TotalRevenue = orders.Sum(x => x.TotalPrice),
				LowStockProductCount = products.Count(x => x.Stock <= 5),
				TodayOrderCount = orders.Count(x => x.OrderDate.Date == DateTime.Today)
			};

			return View(model);
		}
	}
}

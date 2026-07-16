using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShopMVC.Models.Enums;
using MiniShopMVC.Services.Interfaces;

namespace MiniShopMVC.Controllers
{
	public class OrderController : BaseController
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		public async Task<IActionResult> Index()
		{
			var userId = GetCurrentUserId();

			if (userId == null)
				return RedirectToAction("Login", "Account");

			var orders = await _orderService.GetOrdersByUserIdAsync(userId.Value);
			return View(orders);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AdminIndex()
		{
			var orders = await _orderService.GetAllOrdersAsync();

			return View(orders);
		}

		public async Task<IActionResult> Details(int id)
		{

			var order = await _orderService.GetOrderDetailsAsync(id);

			if (order == null)
				return NotFound();

			return View(order);

		}



		//-----------------------------------Post-----------------------------------------------------

		[HttpPost]
		public async Task<IActionResult> Create()
		{
			var userId = GetCurrentUserId();
			if (userId == null)
			{
				return RedirectToAction("Login", "Account");
			}

			var result = await _orderService.CreateOrderAsync(userId.Value);

			if (!result)
			{
				TempData["Error"] = "Sepet boş veya stok yetersiz";
				return RedirectToAction("Index", "Cart");
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateStatus(int orderId, OrderStatus status)
		{
			await _orderService.UpdateOrderStatusAsync(orderId, status);

			return RedirectToAction(nameof(AdminIndex));
		}
	}
}

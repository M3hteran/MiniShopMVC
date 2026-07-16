using MiniShopMVC.Models;
using MiniShopMVC.Models.Enums;
using MiniShopMVC.Repositories.Interfaces;
using MiniShopMVC.Services.Interfaces;

namespace MiniShopMVC.Services.Concrete
{
	public class OrderService : IOrderService
	{

		private readonly IRepository<Order> _orderRepository;
		private readonly IRepository<OrderItem> _orderItemRepository;
		private readonly IRepository<Cart> _cartRepository;
		private readonly IRepository<CartItem> _cartItemRepository;
		private readonly IRepository<Product> _productRepository;

		public OrderService(IRepository<Order> orderRepository, IRepository<OrderItem> orderItemRepository, IRepository<Cart> cartRepository, IRepository<CartItem> cartItemRepository, IRepository<Product> productRepository)
		{
			_orderRepository = orderRepository;
			_orderItemRepository = orderItemRepository;
			_cartRepository = cartRepository;
			_cartItemRepository = cartItemRepository;
			_productRepository = productRepository;
		}

		public async Task AddAsync(Order order)
		{
			await _orderRepository.AddAsync(order);
		}

		public async Task<bool> CreateOrderAsync(int userId)
		{
			var cart = await _cartRepository.FirstOrDefaultWithIncludesAsync(x => x.AppUserId == userId, "CartItems", "CartItems.Product");

			if (cart == null || cart.CartItems.Count == 0)
				return false;

			foreach (var item in cart.CartItems)
			{
				if (item.Product.Stock < item.Quantity)
				{
					return false;
				}
			}

			var order = new Order
			{
				AppUserId = userId,
				OrderDate = DateTime.Now,
				Status = OrderStatus.Pending,
				TotalPrice = cart.TotalPrice
			};


			await _orderRepository.AddAsync(order);

			foreach (var item in cart.CartItems)
			{
				var orderItem = new OrderItem
				{
					OrderId = order.Id,
					ProductId = item.ProductId,
					Quantity = item.Quantity,
					UnitPrice = item.UnitPrice,
				};

				await _orderItemRepository.AddAsync(orderItem);

				item.Product.Stock -= item.Quantity;
				await _productRepository.UpdateAsync(item.Product);
			}

			foreach (var item in cart.CartItems.ToList())
			{
				await _cartItemRepository.DeleteAsync(item.Id);
			}

			return true;
		}

		public async Task DeleteAsync(int id)
		{
			await _orderRepository.DeleteAsync(id);
		}

		public async Task<List<Order>> GetAllAsync()
		{
			return await _orderRepository.GetAllAsync();
		}

		public async Task<List<Order>> GetAllOrdersAsync()
		{
			return await _orderRepository.GetAllWithIncludeAsync("AppUser", "OrderItems", "OrderItems.Product");
		}

		public async Task<Order?> GetByIdAsync(int id)
		{
			return await _orderRepository.GetByIdAsync(id);
		}

		public async Task<Order?> GetOrderDetailsAsync(int orderId)
		{
			return await _orderRepository.GetByIdWithIncludesAsync(orderId, "AppUser", "OrderItems", "OrderItems.Product");
		}

		public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
		{
			var orders = await _orderRepository.GetAllWithIncludeAsync("OrderItems", "OrderItems.Product");

			return orders.Where(x => x.AppUserId == userId).ToList();
		}

		public async Task UpdateAsync(Order order)
		{
			await _orderRepository.UpdateAsync(order);
		}

		public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
		{
			var order = await _orderRepository.GetByIdAsync(orderId);

			if (order == null)
				return;
			order.Status = status;

			await _orderRepository.UpdateAsync(order);
		}
	}
}

using MiniShopMVC.Models;
using MiniShopMVC.Models.Enums;

namespace MiniShopMVC.Services.Interfaces
{
	public interface IOrderService
	{
		Task<List<Order>> GetAllAsync();
		Task<Order?> GetByIdAsync(int id);
		Task AddAsync(Order order);
		Task UpdateAsync(Order order);
		Task DeleteAsync(int id);
		Task<bool> CreateOrderAsync(int userId);
		Task<List<Order>> GetOrdersByUserIdAsync(int userId);
		Task<List<Order>> GetAllOrdersAsync();
		Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
		Task<Order?> GetOrderDetailsAsync(int orderId);
	}
}

using MiniShopMVC.Models;

namespace MiniShopMVC.Services.Interfaces
{
	public interface IOrderItemServices
	{
		Task<List<OrderItem>> GetAllAsync();
		Task<OrderItem?> GetByIdAsync(int id);
		Task AddAsync(OrderItem OrederItem);
		Task UpdateAsync(OrderItem orderItem);
		Task DeleteAsync(int id);


	}
}

using MiniShopMVC.Models;
using MiniShopMVC.Repositories.Interfaces;
using MiniShopMVC.Services.Interfaces;

namespace MiniShopMVC.Services.Concrete
{
	public class OrderItemService : IOrderItemServices
	{

		private readonly IRepository<OrderItem> _orderItemRepository;

		public OrderItemService(IRepository<OrderItem> orderItemRepository)
		{
			_orderItemRepository = orderItemRepository;
		}

		public async Task AddAsync(OrderItem orderItem)
		{
			await _orderItemRepository.AddAsync(orderItem);
		}

		public async Task DeleteAsync(int id)
		{
			await _orderItemRepository.DeleteAsync(id);
		}

		public async Task<List<OrderItem>> GetAllAsync()
		{
			return await _orderItemRepository.GetAllAsync();
		}

		public async Task<OrderItem?> GetByIdAsync(int id)
		{
			return await _orderItemRepository.GetByIdAsync(id);
		}

		public async Task UpdateAsync(OrderItem orderItem)
		{
			await _orderItemRepository.UpdateAsync(orderItem);
		}
	}
}

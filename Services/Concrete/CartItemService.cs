using MiniShopMVC.Models;
using MiniShopMVC.Repositories.Interfaces;
using MiniShopMVC.Services.Interfaces;

namespace MiniShopMVC.Services.Concrete
{
	public class CartItemService : ICartItemService
	{

		private readonly IRepository<CartItem> _cartItemRepository;

		public CartItemService(IRepository<CartItem> cartItemRepository)
		{
			_cartItemRepository = cartItemRepository;
		}

		public async Task AddAsync(CartItem cartItem)
		{
			await _cartItemRepository.AddAsync(cartItem);
		}

		public async Task DeleteAsync(int id)
		{
			await _cartItemRepository.DeleteAsync(id);
		}

		public async Task<List<CartItem>> GetAllAsync()
		{
			return await _cartItemRepository.GetAllAsync();
		}

		public async Task<CartItem?> GetByIdAsync(int id)
		{
			return await _cartItemRepository.GetByIdAsync(id);
		}

		public async Task UpdateAsync(CartItem cartItem)
		{
			await _cartItemRepository.UpdateAsync(cartItem);
		}
	}
}

using MiniShopMVC.Models;

namespace MiniShopMVC.Services.Interfaces
{
	public interface ICartService
	{
		Task<bool> AddToCartAsync(int userId, int productId);

		Task<Cart?> GetCartByUserIdAsync(int userId);

		Task IncreaseQuantityAsync(int userId, int productId);

		Task DecreaseQuantityAsync(int userId, int productId);

		Task RemoveFromCartAsync(int userId, int productId);

		Task<int> GetCartItemCountAsync(int userId);
	}
}

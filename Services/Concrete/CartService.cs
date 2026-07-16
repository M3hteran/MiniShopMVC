using MiniShopMVC.Models;
using MiniShopMVC.Repositories.Interfaces;
using MiniShopMVC.Services.Interfaces;

namespace MiniShopMVC.Services.Concrete
{
	public class CartService : ICartService
	{
		private readonly IRepository<Cart> _cartRepository;

		private readonly IRepository<CartItem> _cartItemRepository;

		private readonly IRepository<Product> _productRepository;

		public CartService(IRepository<Cart> cartRepository, IRepository<CartItem> cartItemRepository, IRepository<Product> productRepository)
		{
			_cartRepository = cartRepository;
			_cartItemRepository = cartItemRepository;
			_productRepository = productRepository;
		}
		public async Task<bool> AddToCartAsync(int userId, int productId)
		{
			var cart = await _cartRepository.FirstOrDefaultAsync(x => x.AppUserId == userId);

			if (cart == null)
			{
				cart = new Cart();

				cart.AppUserId = userId;

				await _cartRepository.AddAsync(cart);
			}

			var cartItem = await _cartItemRepository.FirstOrDefaultAsync(x => x.CartId == cart.Id && x.ProductId == productId);

			if (cartItem != null)
			{
				cartItem.Quantity++;
				await _cartItemRepository.UpdateAsync(cartItem);
			}
			else
			{
				var product = await _productRepository.GetByIdAsync(productId);

				if (product == null)
				{
					return false;
				}

				cartItem = new CartItem();
				cartItem.CartId = cart.Id;
				cartItem.ProductId = productId;
				cartItem.Quantity = 1;
				cartItem.UnitPrice = product.Price;

				await _cartItemRepository.AddAsync(cartItem);
			}

			return true;
		}

		public async Task DecreaseQuantityAsync(int userId, int productId)
		{
			var cart = await _cartRepository.FirstOrDefaultAsync(x => x.AppUserId == userId);

			if (cart == null)
				return;
			var cartItem = await _cartItemRepository.FirstOrDefaultAsync(x => x.CartId == cart.Id && x.ProductId == productId);

			if (cartItem == null)
			{
				return;
			}

			if (cartItem.Quantity > 1)
			{
				cartItem.Quantity--;

				await _cartItemRepository.UpdateAsync(cartItem);
			}
			else
			{
				await _cartItemRepository.DeleteAsync(cartItem.Id);
			}
		}

		public async Task<Cart?> GetCartByUserIdAsync(int userId)
		{
			return await _cartRepository.FirstOrDefaultWithIncludesAsync(x => x.AppUserId == userId, "CartItems", "CartItems.Product");
		}

		public async Task<int> GetCartItemCountAsync(int userId)
		{
			var cart = await _cartRepository.FirstOrDefaultWithIncludesAsync(x => x.AppUserId == userId, "CartItems");
			if (cart == null)
				return 0;
			return cart.CartItems.Sum(x => x.Quantity);
		}

		public async Task IncreaseQuantityAsync(int userId, int productId)
		{
			var cart = await _cartRepository.FirstOrDefaultAsync(x => x.AppUserId == userId);

			if (cart == null)
				return;
			var cartItem = await _cartItemRepository.FirstOrDefaultAsync(x => x.CartId == cart.Id && x.ProductId == productId);

			if (cartItem == null)
				return;
			cartItem.Quantity++;

			await _cartItemRepository.UpdateAsync(cartItem);
		}

		public async Task RemoveFromCartAsync(int userId, int productId)
		{
			var cart = await _cartRepository.FirstOrDefaultAsync(x => x.AppUserId == userId);

			if (cart == null)
				return;

			var cartItem = await _cartItemRepository.FirstOrDefaultAsync(x => x.CartId == cart.Id && x.ProductId == productId);

			if (cartItem == null)
				return;

			await _cartItemRepository.DeleteAsync(cartItem.Id);

		}
	}
}

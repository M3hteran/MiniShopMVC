using MiniShopMVC.Models;

namespace MiniShopMVC.Services.Interfaces
{
	public interface IProductService
	{
		Task<List<Product>> GetAllWithCategoryAsync();
		Task<Product?> GetByIdWithCategoryAsync(int id);
		Task<List<Product>> GetAllAsync();
		Task<Product?> GetByIdAsync(int id);
		Task AddAsync(Product product);
		Task UpdateAsync(Product product);
		Task DeleteAsync(int id);

	}
}

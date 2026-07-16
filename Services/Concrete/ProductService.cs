using MiniShopMVC.Models;
using MiniShopMVC.Repositories.Interfaces;
using MiniShopMVC.Services.Interfaces;

namespace MiniShopMVC.Services.Concrete
{
	public class ProductService : IProductService
	{

		private readonly IRepository<Product> _productRepository;

		public ProductService(IRepository<Product> productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task AddAsync(Product product)
		{
			await _productRepository.AddAsync(product);
		}

		public async Task DeleteAsync(int id)
		{
			await _productRepository.DeleteAsync(id);
		}

		public async Task<List<Product>> GetAllAsync()
		{
			return await _productRepository.GetAllAsync();
		}

		public async Task<List<Product>> GetAllWithCategoryAsync()
		{
			return await _productRepository.GetAllWithIncludeAsync("Category");
		}

		public async Task<Product?> GetByIdAsync(int id)
		{
			return await _productRepository.GetByIdAsync(id);
		}

		public async Task<Product?> GetByIdWithCategoryAsync(int id)
		{
			return await _productRepository.GetByIdWithIncludesAsync(id, "Category");
		}

		public async Task UpdateAsync(Product product)
		{
			await _productRepository.UpdateAsync(product);
		}
	}
}

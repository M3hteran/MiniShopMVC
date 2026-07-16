using MiniShopMVC.Models;
using MiniShopMVC.Repositories.Interfaces;
using MiniShopMVC.Services.Interfaces;

namespace MiniShopMVC.Services.Concrete
{
	public class CategoryService : ICategoryService
	{

		private readonly IRepository<Category> _categoryRepository;

		public CategoryService(IRepository<Category> categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task AddAsync(Category category)
		{
			await _categoryRepository.AddAsync(category);
		}

		public async Task DeleteAsync(int id)
		{
			await _categoryRepository.DeleteAsync(id);
		}

		public async Task<List<Category>> GetAllAsync()
		{
			return await _categoryRepository.GetAllAsync();
		}

		public async Task<Category?> GetByIdAsync(int id)
		{
			return await _categoryRepository.GetByIdAsync(id);
		}

		public async Task UpdateAsync(Category category)
		{
			await _categoryRepository.UpdateAsync(category);
		}
	}
}

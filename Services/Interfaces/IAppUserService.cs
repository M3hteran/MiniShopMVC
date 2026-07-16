using MiniShopMVC.Models;

namespace MiniShopMVC.Services.Interfaces
{
	public interface IAppUserService
	{
		Task<AppUser?> GetByEmailAsync(string email);
		Task<AppUser?> GetByUserNameAsync(string userName);
		Task<List<AppUser>> GetAllAsync();
		Task<AppUser?> GetByIdAsync(int id);
		Task AddAsync(AppUser appUser);
		Task Update(AppUser appUser);
		Task DeleteAsync(int id);

	}
}

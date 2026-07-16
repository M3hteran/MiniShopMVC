using MiniShopMVC.Models;
using MiniShopMVC.Repositories.Interfaces;
using MiniShopMVC.Services.Interfaces;

namespace MiniShopMVC.Services.Concrete
{
	public class AppUserService : IAppUserService
	{

		private readonly IRepository<AppUser> _appUserRepository;

		public AppUserService(IRepository<AppUser> appUserRepository)
		{
			_appUserRepository = appUserRepository;
		}

		public async Task AddAsync(AppUser appUser)
		{
			await _appUserRepository.AddAsync(appUser);
		}

		public async Task DeleteAsync(int id)
		{
			await _appUserRepository.DeleteAsync(id);
		}

		public async Task<List<AppUser>> GetAllAsync()
		{
			return await _appUserRepository.GetAllAsync();
		}

		public async Task<AppUser?> GetByEmailAsync(string email)
		{
			return await _appUserRepository.FirstOrDefaultAsync(x => x.Email == email);
		}

		public async Task<AppUser?> GetByIdAsync(int id)
		{
			return await _appUserRepository.GetByIdAsync(id);
		}

		public async Task<AppUser?> GetByUserNameAsync(string userName)
		{
			return await _appUserRepository.FirstOrDefaultAsync(x => x.UserName == userName);
		}

		public async Task Update(AppUser appUser)
		{
			await _appUserRepository.UpdateAsync(appUser);
		}
	}
}

using System.Linq.Expressions;

namespace MiniShopMVC.Repositories.Interfaces
{
	public interface IRepository<T> where T : class
	{

		Task<T?> FirstOrDefaultWithIncludesAsync(Expression<Func<T, bool>> predicate, params string[] includes);

		Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

		Task<List<T>> GetAllWithIncludeAsync(params string[] includes);

		Task<T?> GetByIdWithIncludesAsync(int id, params string[] includes);
		Task<List<T>> GetAllAsync();

		Task<T?> GetByIdAsync(int id);

		Task AddAsync(T entity);

		Task UpdateAsync(T entity);

		Task DeleteAsync(int id);
	}
}

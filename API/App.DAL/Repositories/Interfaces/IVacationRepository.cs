using App.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.DAL.Repositories.Interfaces
{
	public interface IVacationRepository : IRepository
	{
		Task<Vacation> GetAsync(int id);
		Task<IEnumerable<Vacation>> GetForYear(int year);
		Task<IEnumerable<Vacation>> GetForUserAsync(int userId, int year);
		Task<int> SaveAsync(Vacation vacation);
	}
}

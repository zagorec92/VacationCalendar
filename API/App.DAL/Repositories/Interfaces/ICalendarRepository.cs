using App.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.DAL.Repositories.Interfaces
{
	public interface ICalendarRepository : IRepository
	{
		Task<IEnumerable<Holiday>> GetHolidaysAsync(int? year, int? month = null);
	}
}

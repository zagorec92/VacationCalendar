using App.Service.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Services.Interfaces
{
	public interface IVacationService
	{
		Task<IEnumerable<VacationDto>> GetForYear(int? year);
		Task<IEnumerable<VacationDto>> GetForUserAsync(int? userId, int? year);
		Task<int> SaveAsync(int? userId, bool isAdmin, VacationDto vacationDto);
	}
}

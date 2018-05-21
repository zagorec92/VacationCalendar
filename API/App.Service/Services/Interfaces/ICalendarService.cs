using App.Service.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Interfaces
{
	public interface ICalendarService
	{
		Task<MonthDto> GetCalendarMonthAsync(int? year, int? month);
		Task<IEnumerable<MonthDto>> GetCalendarYearAsync(int? year);
	}
}

using App.Model;
using System.Collections.Generic;
using System.Linq;

namespace App.Service.DTO
{
	public class MonthDto
	{
		#region Properties

		public int Month { get; set; }
		public int Year { get; set; }
		public List<CalendarDayDto> Days { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Maps object data to <see cref="MonthDto"/>.
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="calendarDays"></param>
		/// <param name="holidays"></param>
		/// <returns>The <see cref="MonthDto"/> instance.</returns>
		public MonthDto MapFrom(int? year, int? month, IEnumerable<CalendarDayDto> calendarDays, IEnumerable<Holiday> holidays)
		{
			Year = year.Value;
			Month = month.Value;
			Days = calendarDays.ToList();

			foreach(CalendarDayDto calendarDayDto in Days)
			{
				Holiday holiday = holidays.FirstOrDefault(x => x.Month == calendarDayDto.Month && x.Day == calendarDayDto.Day);
				if(holiday != null)
				{
					calendarDayDto.IsHoliday = true;
					calendarDayDto.HolidayName = holiday.Name;
				}
			}

			return this;
		}

		#endregion
	}
}

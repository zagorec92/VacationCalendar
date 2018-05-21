using System;

namespace App.Service.DTO
{
	public class CalendarDayDto
	{
		#region Properties

		public int Day { get; set; }

		public int Month { get; set; }

		public int Year { get; set; }

		public DateTime Date { get; set; }

		public string DayName { get; set; }

		public bool? IsHoliday { get; set; }

		public bool? IsWeekend { get; set; }

		public string HolidayName { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Maps object data from <see cref="DateTime"/> to <see cref="CalendarDayDto"/>.
		/// </summary>
		/// <param name="date">The <see cref="DateTime"/> instance containing data.</param>
		/// <returns>The <see cref="CalendarDayDto"/> instance.</returns>
		public CalendarDayDto MapFrom(DateTime date)
		{
			Year = date.Year;
			Month = date.Month;
			Day = date.Day;
			Date = date.Date;
			DayName = date.DayOfWeek.ToString();
			IsWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

			return this;
		}

		#endregion
	}
}

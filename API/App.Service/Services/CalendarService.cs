using App.DAL.Repositories.Interfaces;
using App.Model;
using App.Service.Base;
using App.Service.DTO;
using App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service
{
	public class CalendarService : ServiceBase<ICalendarRepository>, ICalendarService
	{
		#region Constructor

		/// <summary>
		/// Instantiates a new instance of the <see cref="CalendarService"/> class.
		/// </summary>
		/// <param name="repository">The injected <seealso cref="ICalendarRepository"/> instance.</param>
		public CalendarService(ICalendarRepository repository)
			: base(repository) { }

		#endregion

		#region Methods

		/// <summary>
		/// Gets the calendar month for the given year and month.
		/// </summary>
		/// <param name="year">Year.</param>
		/// <param name="month">Month.</param>
		/// <returns>The <see cref="Task{MonthDto}"/> instance.</returns>
		public async Task<MonthDto> GetCalendarMonthAsync(int? year, int? month)
		{
			if (!year.HasValue)
				throw new ArgumentException("Value must not be null.", nameof(year));

			IEnumerable<Holiday> holidays = await Repository.GetHolidaysAsync(year, month);
			IEnumerable<CalendarDayDto> resultDto = GetDates(year, month);

			MonthDto monthDto = new MonthDto().MapFrom(year, month, resultDto, holidays);

			return monthDto;
		}

		/// <summary>
		/// Gets the calendar for the given year.
		/// </summary>
		/// <param name="year">Year.</param>
		/// <returns>The <see cref="Task{IEnumerable{MonthDto}}"/> instance.</returns>
		public async Task<IEnumerable<MonthDto>> GetCalendarYearAsync(int? year)
		{
			if (!year.HasValue)
				throw new ArgumentException("Value must not be null.", nameof(year));

			IEnumerable<Holiday> holidays = await Repository.GetHolidaysAsync(year);
			IList<MonthDto> monthsDto = new List<MonthDto>();

			for(int i = 1; i <= 12; i++)
				monthsDto.Add(new MonthDto().MapFrom(year, i, GetDates(year, i), holidays));

			return monthsDto;
		}

		/// <summary>
		/// Gets the dates for the given year and month.
		/// </summary>
		/// <param name="year">Year.</param>
		/// <param name="month">Month.</param>
		/// <returns>The <see cref="IEnumerable{CalendarDayDto}"/> instance.</returns>
		private static IEnumerable<CalendarDayDto> GetDates(int? year, int? month)
		{
			return Enumerable.Range(1, DateTime.DaysInMonth(year.Value, month.Value))
				.Select(x => new CalendarDayDto().MapFrom(new DateTime(year.Value, month.Value, x)));
		}

		#endregion
	}
}

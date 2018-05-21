using App.DAL.Repositories.Base;
using App.DAL.Repositories.Interfaces;
using App.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DAL.Repository
{
	public class CalendarRepository : RepositoryBase<AppDbContext>, ICalendarRepository
	{
		#region Constructor

		/// <summary>
		/// Instantiates a new instance of the <see cref="CalendarRepository"/> class.
		/// </summary>
		/// <param name="context">The <see cref="AppDbContext"/> instance.</param>
		public CalendarRepository(AppDbContext context)
			: base(context) { }

		#endregion

		#region Methods

		#region Get

		/// <summary>
		/// Gets holidays for the given year and month.
		/// </summary>
		/// <param name="year">Year.</param>
		/// <param name="month">Month.</param>
		/// <typeparam></typeparam>
		/// <returns>The <see cref="Task{IEnumerable{Holiday}}"/> instance.</returns>
		public async Task<IEnumerable<Holiday>> GetHolidaysAsync(int? year, int? month = null)
		{
			return await Context.Set<Holiday>()
				.Where(x => x.Year == year && (!month.HasValue || x.Month == month))
				.ToListAsync();
		}

		#endregion

		#endregion
	}
}

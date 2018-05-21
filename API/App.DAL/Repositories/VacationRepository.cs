using App.DAL.Helpers;
using App.DAL.Repositories.Base;
using App.DAL.Repositories.Interfaces;
using App.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DAL.Repositories
{
	public class VacationRepository : RepositoryBase<AppDbContext>, IVacationRepository
	{
		#region Constructor

		/// <summary>
		/// Instantiates a new instance of the <see cref="VacationRepository"/> class.
		/// </summary>
		/// <param name="context">The <see cref="AppDbContext"/> instance.</param>
		public VacationRepository(AppDbContext context)
			: base(context) { }

		#endregion

		#region Methods

		#region Get

		/// <summary>
		/// Gets the vacation for the given id.
		/// </summary>
		/// <param name="id">Vacation ID.</param>
		/// <returns>The <see cref="Task{Vacation}"/> instance.</returns>
		public async Task<Vacation> GetAsync(int id)
		{
			return await Context.Set<Vacation>().FirstOrDefaultAsync(x => x.ID == id);
		}

		/// <summary>
		/// Gets the non rejected vacations for the given userId and year.
		/// </summary>
		/// <param name="userId">User ID.</param>
		/// <param name="year">Year.</param>
		/// <returns>The <see cref="Task{IEnumerable{Vacation}}"/> instance.</returns>
		public async Task<IEnumerable<Vacation>> GetForUserAsync(int userId, int year)
		{
			return await Context.Set<Vacation>()
				.Where(x => x.UserID == userId && x.StatusID != (int)EnumHelper.VacationStatus.Rejected && (x.DateFrom.Year == year || x.DateTo.Year == year))
				.Include(x => x.Status)
				.Include(x => x.Type)
				.Include(x => x.Availability)
				.ToListAsync();
		}

		/// <summary>
		/// Gets the entered vacations for the given year.
		/// </summary>
		/// <param name="year">Year.</param>
		/// <returns>The <see cref="Task{IEnumerable{Vacation}}"/> instance.</returns>
		public async Task<IEnumerable<Vacation>> GetForYear(int year)
		{
			return await Context.Set<Vacation>()
				.Where(x => x.StatusID == (int)EnumHelper.VacationStatus.Entered && (x.DateFrom.Year == year || x.DateTo.Year == year))
				.Include(x => x.Status)
				.Include(x => x.Type)
				.Include(x => x.Availability)
				.Include(x => x.User)
				.ToListAsync();
		}

		#endregion

		#region Save

		/// <summary>
		/// Inserts or updates the vacation.
		/// </summary>
		/// <param name="vacation">The <see cref="Vacation"/> instance.</param>
		/// <returns>The <see cref="Task{System.Int32}"/> instance.</returns>
		public async Task<int> SaveAsync(Vacation vacation)
		{
			return await InsertOrUpdateAsync(vacation);
		}

		#endregion

		#endregion
	}
}

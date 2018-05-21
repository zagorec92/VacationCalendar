using App.DAL.Helpers;
using App.DAL.Repositories.Base;
using App.DAL.Repositories.Interfaces;
using App.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DAL.Repository
{
	public class UserRepository : RepositoryBase<AppDbContext>, IUserRepository
	{
		#region Constructor

		/// <summary>
		/// Instantiates a new instance of the <see cref="UserRepository"/> class.
		/// </summary>
		/// <param name="context">The <see cref="AppDbContext"/> instance.</param>
		public UserRepository(AppDbContext context)
			: base(context) { }

		#endregion

		#region Methods

		#region Get

		/// <summary>
		/// Gets the user for the given id.
		/// </summary>
		/// <param name="id">User ID.</param>
		/// <returns>The <see cref="Task{User}"/> instance.</returns>
		public async Task<User> GetAsync(int? id)
		{
			return await Get(x => x.ID == id);
		}

		/// <summary>
		/// Gets the user for the given selector delegate.
		/// </summary>
		/// <param name="selector">The <see cref="Func{User, Boolean}"/> delegate.</param>
		/// <returns>The <see cref="Task{User}"/> instance.</returns>
		private async Task<User> Get(Func<User, bool> selector)
		{
			return await Context.Set<User>()
				.Include(x => x.Roles).ThenInclude(x => x.Role)
				.Include(x => x.Vacations).ThenInclude(x => x.Status)
				.Include(x => x.Vacations).ThenInclude(x => x.Type)
				.Include(x => x.Vacations).ThenInclude(x => x.Availability)
				.Where(x => selector(x))
				.FirstOrDefaultAsync();
		}

		/// <summary>
		/// Gets the user for the given email and password.
		/// </summary>
		/// <param name="email">Email.</param>
		/// <param name="password">Password.</param>
		/// <returns>The <see cref="Task{User}"/> instance.</returns>
		public async Task<User> GetAsync(string email, string password)
		{
			return await Context.Set<User>()
				.Include(x => x.Roles)
				.ThenInclude(x => x.Role)
				.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
		}

		/// <summary>
		/// Gets the user with vacations for the given parameters.
		/// </summary>
		/// <param name="name">User name.</param>
		/// <param name="dateFrom">Vacation date from.</param>
		/// <param name="dateTo">Vacation date to.</param>
		/// <returns>The <see cref="Task{IEnumerable{User}}"/> instance.</returns>
		public async Task<IEnumerable<User>> GetUsersWithVacationsAsync(string name, DateTime? dateFrom, DateTime? dateTo)
		{
			return await GetUsers()
				.Include(x => x.Vacations).ThenInclude(x => x.Status)
				.Include(x => x.Vacations).ThenInclude(x => x.Type)
				.Include(x => x.Vacations).ThenInclude(x => x.Availability)
				.Where(x =>
					(
						String.IsNullOrEmpty(name) ||
						x.FirstName.ToLower().Contains(name.ToLower()) ||
						x.LastName.ToLower().Contains(name.ToLower())
					) &&
					(
						(!dateFrom.HasValue) ||
						(x.Vacations.Any(y => y.DateFrom.Month == dateFrom.Value.Month && y.DateFrom >= dateFrom.Value))
					) &&
					(
						(!dateTo.HasValue) ||
						(x.Vacations.Any(y => y.DateTo.Month == dateTo.Value.Month && y.DateTo <= dateTo.Value))
					)
				)
				.ToListAsync();
		}

		/// <summary>
		/// Gets the users who are not in Admin role.
		/// </summary>
		/// <returns>The <see cref="IQueryable{User}"/> instance.</returns>
		private IQueryable<User> GetUsers()
		{
			return Context.Set<User>()
				.Include(x => x.Roles).ThenInclude(x => x.Role)
				.Where(x => !x.Roles.Any(y => y.Role.ID == (int)EnumHelper.Role.Admin))
				.AsQueryable();
		}

		#endregion

		#endregion
	}
}

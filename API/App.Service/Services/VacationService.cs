using App.DAL.Helpers;
using App.DAL.Repositories.Interfaces;
using App.Model;
using App.Service.Base;
using App.Service.DTO;
using App.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.Services
{
	public class VacationService : ServiceBase<IVacationRepository>, IVacationService
	{
		#region Constructor

		/// <summary>
		/// Instantiates a new instance of the <see cref="VacationService"/> class.
		/// </summary>
		/// <param name="repository">The injected <seealso cref="IVacationRepository"/> instance.</param>
		public VacationService(IVacationRepository repository)
			: base(repository) { }

		#endregion

		#region Methods

		/// <summary>
		/// Gets the user vacations for the given user id and year.
		/// </summary>
		/// <param name="userId">User ID.</param>
		/// <param name="year">Year.</param>
		/// <returns>The <see cref="Task{IEnumerable{VacationDto}}"/> instance.</returns>
		public async Task<IEnumerable<VacationDto>> GetForUserAsync(int? userId, int? year)
		{
			if (!userId.HasValue)
				throw new ArgumentException("Value must not null.", nameof(userId));

			IEnumerable<Vacation> vacations = await Repository.GetForUserAsync(userId.Value, year.GetValueOrDefault(DateTime.Today.Year));
			IEnumerable<VacationDto> vacationsDto = vacations.Select(x => new VacationDto().MapFrom(x));

			return vacationsDto;
		}

		/// <summary>
		/// Gets the vacations for the given year.
		/// </summary>
		/// <param name="year">Year.</param>
		/// <returns>The <see cref="Task{IEnumerable{VacationDto}}"/> instance.</returns>
		public async Task<IEnumerable<VacationDto>> GetForYear(int? year)
		{
			if (!year.HasValue)
				throw new ArgumentException("Year must have a vale", nameof(year));

			IEnumerable<Vacation> vacations = await Repository.GetForYear(year.Value);
			IEnumerable<VacationDto> vacationsDto = vacations.Select(x => new VacationDto().MapFrom(x));

			return vacationsDto;
		}

		/// <summary>
		/// Saves the vacation.
		/// </summary>
		/// <param name="userId">User ID.</param>
		/// <param name="isAdmin">Indication if user is in Admin role.</param>
		/// <returns>The <see cref="Task{Int32}"/> instance.</returns>
		/// <returns></returns>
		public async Task<int> SaveAsync(int? userId, bool isAdmin, VacationDto vacationDto)
		{
			if (!vacationDto.ID.HasValue)
			{
				if (vacationDto.Status > (int)EnumHelper.VacationStatus.Entered)
					throw new ArgumentException("New vacation must be in entered status.");

				if (!vacationDto.Active.GetValueOrDefault())
					throw new ArgumentException("New vacation must be in active state.");
			}

			Vacation vacation = null;
			if (vacationDto.ID.HasValue)
			{
				vacation = await Repository.GetAsync(vacationDto.ID.Value);

				if (isAdmin && (vacation.DateFrom != vacationDto.DateFrom || vacation.DateTo != vacationDto.DateTo || vacation.TypeID != vacationDto.Type))
					throw new ArgumentException("Vacation data (except status) can only be changed by the user who entered it.");
			}

			vacation = vacationDto.MapTo(vacation);

			if (!isAdmin)
				vacation.UserID = userId.Value;

			return await Repository.SaveAsync(vacation);
		}

		#endregion
	}
}

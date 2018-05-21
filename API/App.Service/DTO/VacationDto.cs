using App.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Service.DTO
{
	public class VacationDto
	{
		#region Properties

		public int? ID { get; set; }

		[Required]
		public int? Status { get; set; }

		[Required]
		public DateTime? DateFrom { get; set; }

		[Required]
		public DateTime? DateTo { get; set; }

		[Required]
		public int? Type { get; set; }

		[Required]
		public int? Availability { get; set; }

		public string StatusName { get; set; }

		public string TypeName { get; set; }

		public string AvailabilityName { get; set; }

		public string UserFullName { get; set; }

		public bool? Active { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Maps object data from <see cref="Vacation"/> to <see cref="VacationDto"/>.
		/// </summary>
		/// <param name="vacation">The <see cref="Vacation"/> instance containing data.</param>
		/// <returns>The <see cref="VacationDto"/> instance.</returns>
		public VacationDto MapFrom(Vacation vacation)
		{
			ID = vacation.ID;
			DateFrom = vacation.DateFrom;
			DateTo = vacation.DateTo;
			Status = vacation.StatusID;
			Type = vacation.TypeID;
			Availability = vacation.AvailabilityID;
			StatusName = vacation.Status.Name;
			TypeName = vacation.Type.Name;
			AvailabilityName = vacation.Availability.Name;
			UserFullName = $"{vacation.User?.FirstName} {vacation.User?.LastName}";
			Active = vacation.Active;

			return this;
		}

		/// <summary>
		/// Maps object data from <see cref="VacationDto"/> to <see cref="Vacation"/>.
		/// </summary>
		/// <param name="vacation">The <see cref="Vacation"/> instance containing data.</param>
		/// <returns>The <see cref="Vacation"/> instance.</returns>
		public Vacation MapTo(Vacation entity = null)
		{
			Vacation vacation = entity;
			if (vacation == null)
				vacation = new Vacation();

			vacation.ID = this.ID;
			vacation.DateFrom = this.DateFrom.GetValueOrDefault();
			vacation.DateTo = this.DateTo.GetValueOrDefault();
			vacation.TypeID = this.Type.GetValueOrDefault();
			vacation.StatusID = this.Status.GetValueOrDefault();
			vacation.AvailabilityID = this.Availability.GetValueOrDefault();
			vacation.Active = this.Active.GetValueOrDefault();

			return vacation;
		}

		#endregion
	}
}

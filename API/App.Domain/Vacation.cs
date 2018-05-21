using App.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Model
{
	[Table("Vacation")]
	public class Vacation : EntityBase
	{
		[ForeignKey("Type")]
		public int TypeID { get; set; }

		[ForeignKey("Status")]
		public int StatusID { get; set; }

		[ForeignKey("Availability")]
		public int AvailabilityID { get; set; }

		[ForeignKey("User")]
		public int UserID { get; set; }

		[Column(TypeName = "Date")]
		public DateTime DateFrom { get; set; }

		[Column(TypeName = "Date")]
		public DateTime DateTo { get; set; }

		public VacationType Type { get; set; }

		public VacationStatus Status { get; set; }

		public VacationAvailability Availability { get; set; }

		public User User { get; set; }
	}
}

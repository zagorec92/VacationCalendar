using App.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Model
{
	[Table("Holiday")]
	public class Holiday : EEntityBase
	{
		[Column(TypeName = "DATE")]
		public DateTime Date { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public int Day { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public int Month { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public int Year { get; set; }
	}
}

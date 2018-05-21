using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Model.Base
{
	public abstract class EntityBase
	{
		[Key]
		public int? ID { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public bool? Active { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime? DateCreated { get; set; }
	}
}

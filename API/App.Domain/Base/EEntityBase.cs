using System.ComponentModel.DataAnnotations;

namespace App.Model.Base
{
	public abstract class EEntityBase : EntityBase
	{
		[StringLength(255)]
		public string Name { get; set; }

		public string Description { get; set; }
	}
}

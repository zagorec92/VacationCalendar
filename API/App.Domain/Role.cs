using App.Model.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Model
{
	[Table("Role")]
	public class Role : EEntityBase
	{
		public ICollection<UserRoles> Users { get; set; }
	}
}

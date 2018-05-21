using App.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Model
{
	[Table("User")]
	public class User : EntityBase
	{
		[StringLength(255)]
		public string FirstName { get; set; }

		[StringLength(255)]
		public string LastName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public ICollection<Vacation> Vacations { get; set; }

		public ICollection<UserRoles> Roles { get; set; }
	}
}

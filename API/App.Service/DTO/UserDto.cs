using App.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace App.Service.DTO
{
	public class UserDto
	{
		#region Properties

		[Required]
		public int? ID { get; set; }

		[Required]
		public string Email { get; set; }

		public string FullName { get; set; }

		public List<VacationDto> Vacations { get; set; }

		public List<RoleDto> Roles { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Maps object data from <see cref="User"/> to <see cref="UserDto"/>.
		/// </summary>
		/// <param name="user">The <see cref="User"/> instance containing data.</param>
		/// <returns>The <see cref="UserDto"/> instance.</returns>
		public UserDto MapFrom(User user)
		{
			ID = user.ID;
			Email = user.Email;
			FullName = $"{user.FirstName} {user.LastName}";
			Vacations = user.Vacations?.Select(x => new VacationDto().MapFrom(x)).ToList();
			Roles = user.Roles?.Select(x => new RoleDto().MapFrom(x.Role)).ToList();

			return this;
		}

		#endregion
	}
}

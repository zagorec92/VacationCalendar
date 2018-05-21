using App.Model;

namespace App.Service.DTO
{
	public class RoleDto
	{
		#region Properties

		public string Name { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Maps object data from <see cref="Role"/> to <see cref="RoleDto"/>.
		/// </summary>
		/// <param name="role">The <see cref="Role"/> instance containing data.</param>
		/// <returns>The <see cref="RoleDto"/> instance.</returns>
		public RoleDto MapFrom(Role role)
		{
			Name = role.Name;

			return this;
		}

		#endregion
	}
}

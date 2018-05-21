using System.ComponentModel.DataAnnotations.Schema;

namespace App.Model
{
	[Table("User.Role")]
	public class UserRoles
	{
		[ForeignKey("User")]
		public int UserID { get; set; }

		[ForeignKey("Role")]
		public int RoleID{ get; set; }

		public User User { get; set; }

		public Role Role { get; set; }
	}
}

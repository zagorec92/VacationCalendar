namespace App.DAL.Helpers
{
	/// <summary>
	/// 
	/// </summary>
	public static class EnumHelper
	{
		public enum Role
		{
			Admin = 1,
			Employee = 2
		}

		public enum VacationStatus
		{
			Entered = 1,
			Confirmed = 2,
			Rejected = 3
		}

		public enum VacationType
		{
			VacationLeave = 1,
			SickLeave = 2
		}

		public enum VacationAvailability
		{
			Available = 1,
			PartiallyAvailable = 2,
			Unavailable = 3
		}
	}
}

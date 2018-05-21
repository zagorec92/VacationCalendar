using App.Service.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Interfaces
{
	public interface IUserService
	{
		Task<IEnumerable<UserDto>> GetUsersWithVacationsAsync(string name, DateTime? dateFrom, DateTime? dateTo);
		Task<UserDto> GetAsync(int? id);
		Task<string> AuthenticateAsync(string email, string password);
	}
}

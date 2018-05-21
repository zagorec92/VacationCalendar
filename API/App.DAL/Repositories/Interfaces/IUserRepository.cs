using App.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.DAL.Repositories.Interfaces
{
	public interface IUserRepository : IRepository
	{
		Task<IEnumerable<User>> GetUsersWithVacationsAsync(string name, DateTime? dateFrom, DateTime? dateTo);
		Task<User> GetAsync(int? id);
		Task<User> GetAsync(string email, string password);
	}
}

using App.DAL.Repositories.Interfaces;
using App.Model;
using App.Service.Base;
using App.Service.DTO;
using App.Service.Helpers;
using App.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace App.Service
{
	public class UserService : ServiceBase<IUserRepository>, IUserService
	{
		#region Fields

		private IConfiguration _configuration;

		#endregion

		#region Constructor

		/// <summary>
		/// Instantiates a new instance of the <see cref="UserService"/> class.
		/// </summary>
		/// <param name="repository">The injected <see cref="IUserRepository"/> instance.</param>
		/// <param name="configuration">The injected <see cref="IConfiguration"/> instance.</param>
		public UserService(IUserRepository repository, IConfiguration configuration)
			: base(repository)
		{
			_configuration = configuration;
		}

		#endregion

		/// <summary>
		/// Gets the user for the given email and password.
		/// </summary>
		/// <param name="email">Email.</param>
		/// <param name="password">Password.</param>
		/// <returns>The <see cref="Task{UserDto}"/> instance.</returns>
		private async Task<UserDto> GetAsync(string email, string password)
		{
			if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
				throw new ArgumentException("Email or password is empty.");

			User user = await Repository.GetAsync(email, password);
			if (user == null)
				throw new AuthenticationException("Invalid username or password.");

			return new UserDto().MapFrom(user);
		}

		/// <summary>
		/// Gets the users with vacations for the given parameters.
		/// </summary>
		/// <param name="name">User name.</param>
		/// <param name="dateFrom">Vacation date from.</param>
		/// <param name="dateTo">Vacation date to.</param>
		/// <returns>The <see cref="Task{IEnumerable{UserDto}}"/> instance.</returns>
		public async Task<IEnumerable<UserDto>> GetUsersWithVacationsAsync(string name, DateTime? dateFrom, DateTime? dateTo)
		{
			IEnumerable<User> users = await Repository.GetUsersWithVacationsAsync(name, dateFrom, dateTo);
			IEnumerable<UserDto> usersDto = users.Select(x => new UserDto().MapFrom(x));

			return usersDto;
		}

		/// <summary>
		/// Generates JWT token for the user authenticated by email and password.
		/// </summary>
		/// <param name="email">Email.</param>
		/// <param name="password">Password.</param>
		/// <returns>The <see cref="Task{String}"/> instance.</returns>
		public async Task<string> AuthenticateAsync(string email, string password)
		{
			UserDto userDto = await GetAsync(email, SecurityHelper.Hash(password));

			return SecurityHelper.CreateToken(userDto, _configuration);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">User ID.</param>
		/// <returns></returns>
		public async Task<UserDto> GetAsync(int? id)
		{
			User user = await Repository.GetAsync(id);
			UserDto userDto = new UserDto().MapFrom(user);

			return userDto;
		}
	}
}

using App.Service.DTO;
using App.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Filters;

namespace WebApi.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[ValidateModel]
	[ServiceFilter(typeof(ApplicationExceptionFilterAttribute))]
	[Produces("application/json")]
	[Route("api/users")]
	public class UserController : Controller
	{
		#region Fields

		private IUserService _service;

		#endregion

		#region Constructor

		/// <summary>
		/// Instantiates a new instance of the <see cref="UserController"/> class.
		/// </summary>
		/// <param name="service">The injected <seealso cref="IUserService"/> instance.</param>
		public UserController(IUserService service)
		{
			_service = service;
		}

		#endregion

		#region Actions

		/// <summary>
		/// Gets users with vacations filtered by the given parameters.
		/// </summary>
		/// <param name="name">First, last or full user name.</param>
		/// <param name="dateFrom">Vacation end date.</param>
		/// <param name="dateTo">Vacation begin date.</param>
		/// <returns>The <see cref="JsonResult"/> containing data.</returns>
		[HttpGet]
		public async Task<JsonResult> GetUsersWithVacations(string name = null, DateTime? dateTo = null, DateTime? dateFrom = null)
		{
			return Json(await _service.GetUsersWithVacationsAsync(name, dateFrom, dateTo));
		}

		/// <summary>
		/// Gets the user by an id.
		/// </summary>
		/// <param name="id">User ID.</param>
		/// <returns>The <see cref="JsonResult"/> containing data.</returns>
		[HttpGet("{id:int}")]
		[Authorize]
		public async Task<JsonResult> GetWithVacations(int? id)
		{
			return Json(await _service.GetAsync(id));
		}

		/// <summary>
		/// Authenticates user and provides a JWT token.
		/// </summary>
		/// <param name="data">The <see cref="LoginDto"/> instance containing data.</param>
		/// <returns>The <see cref="JsonResult"/> containing data.</returns>
		[HttpPost]
		[Route("login")]
		public async Task<JsonResult> LogIn([FromBody]LoginDto data)
		{
			return Json(await _service.AuthenticateAsync(data.Email, data.Password));
		}

		#endregion
	}
}
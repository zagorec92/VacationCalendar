using App.Service.DTO;
using App.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
	[Route("api/vacation")]
	[Authorize]
	public class VacationController : Controller
	{
		#region Fields

		private IVacationService _service;

		#endregion

		#region Constructor

		/// <summary>
		/// Instantiates a new instance of the <see cref="VacationController"/> class.
		/// </summary>
		/// <param name="service">The injected <seealso cref="IVacationService"/> instance.</param>
		public VacationController(IVacationService service)
		{
			_service = service;
		}

		#endregion

		#region Actions

		/// <summary>
		/// Saves vacation.
		/// </summary>
		/// <param name="vacationDto">The <see cref="VacationDto"/> instance containing data.</param>
		/// <returns>The <see cref="JsonResult"/> containing data.</returns>
		[HttpPost("save")]
		public async Task<JsonResult> Save([FromBody] VacationDto vacationDto)
		{
			int userId = Int32.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Jti));
			bool isAdmin = User.IsInRole("Admin");
			return Json(await _service.SaveAsync(userId, isAdmin, vacationDto));
		}

		/// <summary>
		/// Gets the vacations for given user and year.
		/// </summary>
		/// <param name="userId">User ID.</param>
		/// <param name="year">Year.</param>
		/// <returns>The <see cref="JsonResult"/> containing data.</returns>
		[HttpGet("{userId}/{year}")]
		public async Task<JsonResult> GetForUser(int? userId, int? year)
		{
			return Json(await _service.GetForUserAsync(userId, year));
		}

		/// <summary>
		/// Gets the vacations for given year.
		/// </summary>
		/// <param name="year">Year.</param>
		/// <returns></returns>
		[HttpGet("year/{year:int}")]
		public async Task<JsonResult> GetForYear(int? year)
		{
			return Json(await _service.GetForYear(year));
		}

		#endregion
	}
}
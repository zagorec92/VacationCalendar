using App.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Filters;

namespace WebApi.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[ServiceFilter(typeof(ApplicationExceptionFilterAttribute))]
	[Produces("application/json")]
	[Route("api/calendar")]
	public class CalendarController : Controller
	{
		#region Fields

		private ICalendarService _service;

		#endregion

		#region Constructor

		/// <summary>
		/// Instantiates a new instance of the <see cref="CalendarController"/> class.
		/// </summary>
		/// <param name="service">The injected <seealso cref="ICalendarService"/> instance.</param>
		public CalendarController(ICalendarService service)
		{
			_service = service;
		}

		#endregion

		#region Actions

		/// <summary>
		/// Gets a collection of months for the given year.
		/// </summary>
		/// <param name="year">Year.</param>
		/// <returns>The <see cref="JsonResult"/> containing data.</returns>
		[HttpGet("{year:int}")]
		public async Task<JsonResult> Get(int? year)
		{
			return Json(await _service.GetCalendarYearAsync(year));
		}

		/// <summary>
		/// Gets a single month or a collection of months, depending on the given parameters.
		/// If both parameters are passed, result is single month.
		/// If only year is passed, result is a collection of months.
		/// </summary>
		/// <param name="year">Year.</param>
		/// <param name="month">Month.</param>
		/// <returns>The <see cref="JsonResult"/> containing data.</returns>
		[HttpGet("{year:int}/{month:int}")]
		public async Task<JsonResult> Get(int? year, int? month)
		{
			return Json(await _service.GetCalendarMonthAsync(year, month));
		}

		#endregion
	}
}
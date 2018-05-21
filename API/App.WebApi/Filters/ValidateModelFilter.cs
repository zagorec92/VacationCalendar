using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
	/// <summary>
	/// 
	/// </summary>
	public class ValidateModelAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
				context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
		}
	}
}

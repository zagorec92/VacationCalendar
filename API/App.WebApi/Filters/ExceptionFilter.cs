using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Authentication;

namespace WebApi.Filters
{
	/// <summary>
	/// 
	/// </summary>
	public class ApplicationExceptionFilterAttribute : ExceptionFilterAttribute
	{
		#region Fields

		private readonly ILogger<ApplicationExceptionFilterAttribute> _logger;

		#endregion

		#region Constructor

		/// <summary>
		/// Instantiates a new instance of the <see cref="ApplicationExceptionFilterAttribute"/> class.
		/// </summary>
		/// <param name="logger">The injected <see cref="ILogger{TCategoryName}"/> instance.</param>
		public ApplicationExceptionFilterAttribute(ILogger<ApplicationExceptionFilterAttribute> logger)
		{
			_logger = logger;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Sets the <see cref="StatusCodeResult"/> by handling the occured exception.
		/// </summary>
		/// <param name="context">The <see cref="ExceptionContext"/> instance containing data.</param>
		public override void OnException(ExceptionContext context)
		{
			int statusCode;

			if (context.Exception is ArgumentException)
				statusCode = StatusCodes.Status400BadRequest;
			else if (context.Exception is AuthenticationException)
				statusCode = StatusCodes.Status401Unauthorized;
			else
				statusCode = StatusCodes.Status500InternalServerError;

			_logger.LogError(context.Exception, context.Exception.Message);

			context.Result = new StatusCodeResult(statusCode);
		}

		#endregion
	}
}

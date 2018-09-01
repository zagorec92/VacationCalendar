using App.DAL.Repositories;
using App.DAL.Repositories.Interfaces;
using App.Service;
using App.Service.Interfaces;
using App.Service.Services;
using App.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Filters;

namespace WebApi.Extensions
{
	internal static class ServiceExtensions
	{
		/// <summary>
		/// Adds runtime dependencies.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
		internal static IServiceCollection AddDependencies(this IServiceCollection services)
		{
			services.AddTransient<ICalendarService, CalendarService>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IVacationService, VacationService>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<ICalendarRepository, CalendarRepository>();
			services.AddTransient<IVacationRepository, VacationRepository>();
			services.AddScoped<ApplicationExceptionFilterAttribute>();

			return services;
		}
	}
}

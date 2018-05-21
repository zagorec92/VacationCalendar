using App.DAL;
using App.DAL.Extensions;
using App.DAL.Repositories;
using App.DAL.Repositories.Interfaces;
using App.DAL.Repository;
using App.Service;
using App.Service.Extensions;
using App.Service.Interfaces;
using App.Service.Services;
using App.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using WebApi.Filters;

namespace WebApi
{
	internal class Startup
	{
		#region Fields

		private IConfiguration _configuration;
		private IHostingEnvironment _hostingEnvironment;

		#endregion

		#region Constructor

		/// <summary>
		/// Instantiates a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// <param name="hostingEnvironment">The injected <see cref="IHostingEnvironment"/> instance.</param>
		/// <param name="configuration">The injected <see cref="IConfiguration"/> instance.</param>
		public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
		{
			_configuration = configuration;
			_hostingEnvironment = hostingEnvironment;
		}

		#endregion

		#region Methods

		/// <summary>
		/// This method gets called by the runtime.
		/// Use this method to add services to the container.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(x => x.UseSqlServer(_configuration.GetConnectionString("AppDbContext")));

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
			{
				x.TokenValidationParameters = new TokenValidationParameters() {
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					RequireExpirationTime = true,
					RequireSignedTokens = true,
					ValidIssuer = _configuration.GetData<string>("Security", "Issuer"),
					ValidAudience = _configuration.GetData<string>("Security", "Issuer"),
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetData<string>("Security", "Key")))
				};
			});

			services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
			services.AddMvcCore().AddApiExplorer();
			services.AddCors();

			services.AddTransient<ICalendarService, CalendarService>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IVacationService, VacationService>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<ICalendarRepository, CalendarRepository>();
			services.AddTransient<IVacationRepository, VacationRepository>();

			services.AddScoped<ApplicationExceptionFilterAttribute>();

			services.AddSwaggerGen(x =>
			{
				x.SwaggerDoc("v1", new Info { Title = "EmployeeCalendar", Version = "v1" });
				x.IncludeXmlComments($"{_hostingEnvironment.ContentRootPath}{_configuration.GetData<string>("Data", "DocumentationUrl")}");
			});
		}

		/// <summary>
		/// This method gets called by the runtime.
		/// Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app">Instance of <see cref="IApplicationBuilder"/>.</param>
		public void Configure(IApplicationBuilder app)
		{
			app.UseDefaultFiles();
			app.UseStaticFiles();

			if (_hostingEnvironment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(x => x.WithOrigins(_configuration.GetData<string>("Data", "ReactAppUrl")).AllowAnyMethod().AllowAnyHeader());
			app.UseAuthentication();
			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(x =>
			{
				x.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeCalendar API v1");
				x.RoutePrefix = "docs";
			});

			using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				if (!serviceScope.ServiceProvider.GetService<AppDbContext>().AllMigrationsApplied())
				{
					serviceScope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
					serviceScope.ServiceProvider.GetService<AppDbContext>().EnsureSeeded();
				}
			}
		}
		#endregion
	}
}

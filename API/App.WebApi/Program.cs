using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApi
{
	/// <summary>
	/// 
	/// </summary>
	public class Program
	{
		#region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		private static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();

		#endregion
	}
}

using Microsoft.Extensions.Configuration;

namespace App.Service.Extensions
{
	public static class ConfigurationExtensions
	{
		#region Methods

		/// <summary>
		/// Gets the data from application configuration.
		/// </summary>
		/// <typeparam name="T">Type of returned data.</typeparam>
		/// <param name="configuration">The <see cref="IConfiguration"/> instance.</param>
		/// <param name="section">Name of the section where data is located.</param>
		/// <param name="key">Name of the key which identifies the data.</param>
		/// <returns></returns>
		public static T GetData<T>(this IConfiguration configuration, string section, string key)
		{
			return configuration.GetSection(section).GetValue<T>(key);
		}

		#endregion
	}
}

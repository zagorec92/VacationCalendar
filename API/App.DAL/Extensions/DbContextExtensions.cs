using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace App.DAL.Extensions
{
	public static class DbContextExtension
	{
		#region Methods

		/// <summary>
		/// Checks if all database migrations are applied.
		/// </summary>
		/// <param name="context">The <see cref="DbContext"/> instance.</param>
		/// <returns><see cref="Boolean"/> indicating if all migrations are applied.</returns>
		public static bool AllMigrationsApplied(this DbContext context)
		{
			IEnumerable<string> applied = context.GetService<IHistoryRepository>()
				.GetAppliedMigrations()
				.Select(m => m.MigrationId);

			IEnumerable<string> total = context.GetService<IMigrationsAssembly>()
				.Migrations
				.Select(m => m.Key);

			return !total.Except(applied).Any();
		}

		/// <summary>
		/// Ensures that all the additional database scripts are executed.
		/// </summary>
		/// <param name="context">The <see cref="DbContext"/> instance.</param>
		public static void EnsureSeeded(this DbContext context)
		{
			string seedFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).GetFiles("Seed.sql", SearchOption.AllDirectories)?.FirstOrDefault()?.FullName;

			if (!String.IsNullOrEmpty(seedFilePath))
			{
				string content = File.ReadAllText(seedFilePath);
				context.Database.ExecuteSqlCommand(content);
			}
		}

		#endregion
	}
}

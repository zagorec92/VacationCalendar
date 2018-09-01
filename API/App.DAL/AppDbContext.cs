using App.DAL.Helpers;
using App.Model;
using App.Model.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;

namespace App.DAL
{
	public class AppDbContext : DbContext
	{
		#region Properties

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Vacation> Vacations { get; set; }
		public DbSet<VacationType> VacationTypes { get; set; }
		public DbSet<VacationStatus> VacationStatuses { get; set; }
		public DbSet<VacationAvailability> VacationAvailabilities { get; set; }
		public DbSet<Holiday> Holidays { get; set; }

		#endregion

		#region Constructor

		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options) { }

		#endregion

		#region Methods

		#region OnModelCreating

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("EmployeeCalendar");

			SetKeys(modelBuilder);
			SetComputedColumns(modelBuilder);
			SetQueryFilter(modelBuilder);

			IEnumerable<IMutableEntityType> entities = modelBuilder.Model.GetEntityTypes()
				.Where(x => typeof(EntityBase).IsAssignableFrom(x.ClrType));

			foreach (IMutableEntityType entityType in entities)
			{
				EntityTypeBuilder entityTypeBuilder = modelBuilder.Entity(entityType.ClrType);
				entityTypeBuilder?.Property("DateCreated").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
				entityTypeBuilder?.Property("Active").HasDefaultValueSql("1").ValueGeneratedOnAdd();
			}

			IEnumerable<IMutableForeignKey> foreignKeysCascade = modelBuilder.Model.GetEntityTypes()
				.SelectMany(x => x.GetForeignKeys())
				.Where(x => !x.IsOwnership && x.DeleteBehavior == DeleteBehavior.Cascade);

			foreach (IMutableForeignKey foreignKey in foreignKeysCascade)
				foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

			Seed(modelBuilder);
		}

		#endregion

		#region Set

		private void SetKeys(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserRoles>().HasKey(x => new { x.UserID, x.RoleID });
		}

		private void SetComputedColumns(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Holiday>().Property(x => x.Day).HasComputedColumnSql("DAY([Date])");
			modelBuilder.Entity<Holiday>().Property(x => x.Month).HasComputedColumnSql("MONTH([Date])");
			modelBuilder.Entity<Holiday>().Property(x => x.Year).HasComputedColumnSql("YEAR([Date])");
		}

		private void SetQueryFilter(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasQueryFilter(x => x.Active.GetValueOrDefault());
			modelBuilder.Entity<Role>().HasQueryFilter(x => x.Active.GetValueOrDefault());
			modelBuilder.Entity<Holiday>().HasQueryFilter(x => x.Active.GetValueOrDefault());
			modelBuilder.Entity<Vacation>().HasQueryFilter(x => x.Active.GetValueOrDefault() && x.StatusID != (int)EnumHelper.VacationStatus.Rejected);
			modelBuilder.Entity<VacationStatus>().HasQueryFilter(x => x.Active.GetValueOrDefault());
			modelBuilder.Entity<VacationType>().HasQueryFilter(x => x.Active.GetValueOrDefault());
			modelBuilder.Entity<VacationAvailability>().HasQueryFilter(x => x.Active.GetValueOrDefault());
		}

		#endregion

		#region Seed

		private void Seed(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(SeedHelper.GenerateUsers());
			modelBuilder.Entity<Role>().HasData(SeedHelper.GenerateRoles());
			modelBuilder.Entity<UserRoles>().HasData(SeedHelper.GenerateUserRoles());
			modelBuilder.Entity<VacationStatus>().HasData(SeedHelper.GenerateVacationStatuses());
			modelBuilder.Entity<VacationType>().HasData(SeedHelper.GenerateVacationTypes());
			modelBuilder.Entity<VacationAvailability>().HasData(SeedHelper.GenerateVacationAvailabilities());
			modelBuilder.Entity<Vacation>().HasData(SeedHelper.GenerateVacations());
		}

		#endregion

		#endregion
	}
}

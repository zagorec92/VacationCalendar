using App.DAL.Helpers;
using App.Model;
using App.Model.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("EmployeeCalendar");

			modelBuilder.Entity<UserRoles>().HasKey(x => new { x.UserID, x.RoleID });

			modelBuilder.Entity<Holiday>().Property(x => x.Day).HasComputedColumnSql("DAY([Date])");
			modelBuilder.Entity<Holiday>().Property(x => x.Month).HasComputedColumnSql("MONTH([Date])");
			modelBuilder.Entity<Holiday>().Property(x => x.Year).HasComputedColumnSql("YEAR([Date])");

			modelBuilder.Entity<User>().HasQueryFilter(x => x.Active.GetValueOrDefault());
			modelBuilder.Entity<Role>().HasQueryFilter(x => x.Active.GetValueOrDefault());
			modelBuilder.Entity<Holiday>().HasQueryFilter(x => x.Active.GetValueOrDefault());
			modelBuilder.Entity<Vacation>().HasQueryFilter(x => x.Active.GetValueOrDefault() && x.StatusID != (int)EnumHelper.VacationStatus.Rejected);
			modelBuilder.Entity<VacationStatus>().HasQueryFilter(x => x.Active.GetValueOrDefault());
			modelBuilder.Entity<VacationType>().HasQueryFilter(x => x.Active.GetValueOrDefault());
			modelBuilder.Entity<VacationAvailability>().HasQueryFilter(x => x.Active.GetValueOrDefault());

			IEnumerable<IMutableEntityType> entities = modelBuilder.Model.GetEntityTypes()
				.Where(x => typeof(EntityBase).IsAssignableFrom(x.ClrType));

			foreach (IMutableEntityType entityType in entities)
			{
				EntityTypeBuilder entityTypeBuilder = modelBuilder.Entity(entityType.ClrType);
				entityTypeBuilder?.Property("DateCreated").HasDefaultValueSql("GETDATE()");
				entityTypeBuilder?.Property("Active").HasDefaultValueSql("1").ValueGeneratedOnAdd();
			}

			IEnumerable<IMutableForeignKey> foreignKeysCascade = modelBuilder.Model.GetEntityTypes()
				.SelectMany(x => x.GetForeignKeys())
				.Where(x => !x.IsOwnership && x.DeleteBehavior == DeleteBehavior.Cascade);

			foreach (IMutableForeignKey foreignKey in foreignKeysCascade)
				foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

			Seed(modelBuilder);
		}

		private void Seed(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(
				new User() { ID = 1, FirstName = "Admin1", LastName = null, Email = "admin1@ec.com", Password = "x61Ey612Kl2gpFL56FT9weDnpSo4AV8j8+qx2AuTHdRyY036xxzTTrw10Wq3+4qQyB+XURPWx1ONxp3Y3pB37A==", Active = true, DateCreated = DateTime.Now },
				new User() { ID = 2, FirstName = "Admin2", LastName = null, Email = "admin2@ec.com", Password = "x61Ey612Kl2gpFL56FT9weDnpSo4AV8j8+qx2AuTHdRyY036xxzTTrw10Wq3+4qQyB+XURPWx1ONxp3Y3pB37A==", Active = true, DateCreated = DateTime.Now },
				new User() { ID = 3, FirstName = "Martin", LastName = "Oliver", Email = "martin.oliver@ec.com", Password = "voHa7tBTzLqzhskUuRtanltSu+yVUb7bN+nu2YEylI0r9I97ab6FBbFGasaP5gvhNh7xsHjmnG9MwhMGOGvbeg==", Active = true, DateCreated = DateTime.Now },
				new User() { ID = 4, FirstName = "Christopher ", LastName = "Johnson", Email = "christopher.johnson@ec.com", Password = "LBlU1yhdXop/N50+PTh4ACbfj8ZPevCerWCF3QBSUnmosfIOXpMebDnfxK1Zj9B2OeJnQl9Ds6PW61hTJoj8Ww==", Active = true, DateCreated = DateTime.Now },
				new User() { ID = 5, FirstName = "Richard ", LastName = "Jackson", Email = "richard.jackson@ec.com", Password = "ZEZzRC/6axBx6TMXhBftfXX/C3kPzeWlKyaehMweevTxHzDhbqd+iRuyZsbwk9zIfmS1zYk6EznXE/kPRWr8Iw==", Active = true, DateCreated = DateTime.Now },
				new User() { ID = 6, FirstName = "Jessica ", LastName = "Stratton", Email = "jessica.stratton@ec.com", Password = "p0dzcANjeuT5Ggybu1x/lSivzXu/d4MvvPWI2/fLK4TNoGeaQ+QueZ8q1XYGsX+OI24FFfhx7VIVOmSCc8Ytfw==", Active = true, DateCreated = DateTime.Now },
				new User() { ID = 7, FirstName = "Frank", LastName = "Boehm", Email = "frank.boehm@ec.com", Password = "GbcCtvGxNcoAnOHfE9hHOLdMt0Ra9j3Tby/e/nffg/zezzB9tTYJNSgkLDzlKpNQRVrLHorZBvzNQP0JWrq08Q==", Active = true, DateCreated = DateTime.Now },
				new User() { ID = 8, FirstName = "Joann ", LastName = "Camp", Email = "joan.camp@ec.com", Password = "47Re7pm1d0izINaLr8gpNtNOO8F2obGGlhcduinHgqqNXgsaDjXtaU8GcLjaueeHnIynB+UcNi0cGqNqO1DRXg==", Active = true, DateCreated = DateTime.Now },
				new User() { ID = 9, FirstName = "Julie ", LastName = "Burroughs", Email = "julie.burroughs@ec.com", Password = "7qrgzXN9I2na3BLoFVDGJovOlWI28WEPfg2NA85lIglFmArxAL7pwQfU0IStmrA6Q1KnCoOGDOCuA9ev46RsAw==", Active = true, DateCreated = DateTime.Now }
			);

			modelBuilder.Entity<Role>().HasData(
				new VacationStatus() { ID = 1, Name = "Admin", Description = "Administrator", Active = true, DateCreated = DateTime.Now },
				new VacationStatus() { ID = 2, Name = "Employee", Description = "Employee", Active = true, DateCreated = DateTime.Now }
			);

			modelBuilder.Entity<UserRoles>().HasData(
				new UserRoles() { UserID = 1, RoleID = 1 },
				new UserRoles() { UserID = 2, RoleID = 1 },
				new UserRoles() { UserID = 3, RoleID = 2 },
				new UserRoles() { UserID = 4, RoleID = 2 },
				new UserRoles() { UserID = 5, RoleID = 2 },
				new UserRoles() { UserID = 6, RoleID = 2 },
				new UserRoles() { UserID = 7, RoleID = 2 },
				new UserRoles() { UserID = 8, RoleID = 2 },
				new UserRoles() { UserID = 9, RoleID = 2 }
			);

			modelBuilder.Entity<VacationStatus>().HasData(
				new VacationStatus() { ID = 1, Name = "Entered", Description = "Vacation is entered but not confirmed", Active = true, DateCreated = DateTime.Now },
				new VacationStatus() { ID = 2, Name = "Confirmed", Description = "Vacation is confirmed by administrator", Active = true, DateCreated = DateTime.Now },
				new VacationStatus() { ID = 3, Name = "Rejected", Description = "Vacation is rejected by administrator", Active = true, DateCreated = DateTime.Now }
			);

			modelBuilder.Entity<VacationType>().HasData(
				new VacationStatus() { ID = 1, Name = "Vacation leave", Description = "Free days", Active = true, DateCreated = DateTime.Now },
				new VacationStatus() { ID = 2, Name = "Sick leave", Description = "Sickness days", Active = true, DateCreated = DateTime.Now }
			);

			modelBuilder.Entity<VacationAvailability>().HasData(
				new VacationStatus() { ID = 1, Name = "Available", Description = "Available to contact", Active = true, DateCreated = DateTime.Now },
				new VacationStatus() { ID = 2, Name = "Partially available", Description = "Available to contact at certain time", Active = true, DateCreated = DateTime.Now },
				new VacationStatus() { ID = 3, Name = "Unavailable", Description = "Unavailable to contact", Active = true, DateCreated = DateTime.Now }
			);

			modelBuilder.Entity<Vacation>().HasData(
				new Vacation() { ID = 1, UserID = 4, StatusID = (int)EnumHelper.VacationStatus.Entered, TypeID = (int)EnumHelper.VacationType.VacationLeave, AvailabilityID = (int)EnumHelper.VacationAvailability.Available, DateFrom = DateTime.Parse("2018-05-25"), DateTo = DateTime.Parse("2018-05-30"), DateCreated = DateTime.Now, Active = true },
				new Vacation() { ID = 2, UserID = 6, StatusID = (int)EnumHelper.VacationStatus.Confirmed, TypeID = (int)EnumHelper.VacationType.SickLeave, AvailabilityID = (int)EnumHelper.VacationAvailability.Unavailable, DateFrom = DateTime.Parse("2018-06-11"), DateTo = DateTime.Parse("2018-06-13"), DateCreated = DateTime.Now, Active = true },
				new Vacation() { ID = 3, UserID = 8, StatusID = (int)EnumHelper.VacationStatus.Entered, TypeID = (int)EnumHelper.VacationType.VacationLeave, AvailabilityID = (int)EnumHelper.VacationAvailability.PartiallyAvailable, DateFrom = DateTime.Parse("2018-07-20"), DateTo = DateTime.Parse("2018-07-23"), DateCreated = DateTime.Now, Active = true }
			);
		}

		#endregion
	}
}

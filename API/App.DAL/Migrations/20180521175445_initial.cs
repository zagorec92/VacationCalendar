using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DAL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "EmployeeCalendar");

            migrationBuilder.CreateTable(
                name: "Holiday",
                schema: "EmployeeCalendar",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()"),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    Day = table.Column<int>(nullable: false, computedColumnSql: "DAY([Date])"),
                    Month = table.Column<int>(nullable: false, computedColumnSql: "MONTH([Date])"),
                    Year = table.Column<int>(nullable: false, computedColumnSql: "YEAR([Date])")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "EmployeeCalendar",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()"),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "EmployeeCalendar",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()"),
                    FirstName = table.Column<string>(maxLength: 255, nullable: true),
                    LastName = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vacation.Availability",
                schema: "EmployeeCalendar",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()"),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacation.Availability", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vacation.Status",
                schema: "EmployeeCalendar",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()"),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacation.Status", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vacation.Type",
                schema: "EmployeeCalendar",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()"),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacation.Type", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User.Role",
                schema: "EmployeeCalendar",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User.Role", x => new { x.UserID, x.RoleID });
                    table.ForeignKey(
                        name: "FK_User.Role_Role_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "EmployeeCalendar",
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User.Role_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "EmployeeCalendar",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vacation",
                schema: "EmployeeCalendar",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()"),
                    TypeID = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false),
                    AvailabilityID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    DateFrom = table.Column<DateTime>(type: "Date", nullable: false),
                    DateTo = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vacation_Vacation.Availability_AvailabilityID",
                        column: x => x.AvailabilityID,
                        principalSchema: "EmployeeCalendar",
                        principalTable: "Vacation.Availability",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacation_Vacation.Status_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "EmployeeCalendar",
                        principalTable: "Vacation.Status",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacation_Vacation.Type_TypeID",
                        column: x => x.TypeID,
                        principalSchema: "EmployeeCalendar",
                        principalTable: "Vacation.Type",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacation_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "EmployeeCalendar",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "EmployeeCalendar",
                table: "Role",
                columns: new[] { "ID", "Active", "DateCreated", "Description", "Name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2018, 5, 21, 19, 54, 33, 210, DateTimeKind.Local), "Administrator", "Admin" },
                    { 2, true, new DateTime(2018, 5, 21, 19, 54, 33, 210, DateTimeKind.Local), "Employee", "Employee" }
                });

            migrationBuilder.InsertData(
                schema: "EmployeeCalendar",
                table: "User",
                columns: new[] { "ID", "Active", "DateCreated", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 9, true, new DateTime(2018, 5, 21, 19, 54, 33, 207, DateTimeKind.Local), "julie.burroughs@ec.com", "Julie ", "Burroughs", "7qrgzXN9I2na3BLoFVDGJovOlWI28WEPfg2NA85lIglFmArxAL7pwQfU0IStmrA6Q1KnCoOGDOCuA9ev46RsAw==" },
                    { 7, true, new DateTime(2018, 5, 21, 19, 54, 33, 207, DateTimeKind.Local), "frank.boehm@ec.com", "Frank", "Boehm", "GbcCtvGxNcoAnOHfE9hHOLdMt0Ra9j3Tby/e/nffg/zezzB9tTYJNSgkLDzlKpNQRVrLHorZBvzNQP0JWrq08Q==" },
                    { 6, true, new DateTime(2018, 5, 21, 19, 54, 33, 207, DateTimeKind.Local), "jessica.stratton@ec.com", "Jessica ", "Stratton", "p0dzcANjeuT5Ggybu1x/lSivzXu/d4MvvPWI2/fLK4TNoGeaQ+QueZ8q1XYGsX+OI24FFfhx7VIVOmSCc8Ytfw==" },
                    { 5, true, new DateTime(2018, 5, 21, 19, 54, 33, 207, DateTimeKind.Local), "richard.jackson@ec.com", "Richard ", "Jackson", "ZEZzRC/6axBx6TMXhBftfXX/C3kPzeWlKyaehMweevTxHzDhbqd+iRuyZsbwk9zIfmS1zYk6EznXE/kPRWr8Iw==" },
                    { 8, true, new DateTime(2018, 5, 21, 19, 54, 33, 207, DateTimeKind.Local), "joan.camp@ec.com", "Joann ", "Camp", "47Re7pm1d0izINaLr8gpNtNOO8F2obGGlhcduinHgqqNXgsaDjXtaU8GcLjaueeHnIynB+UcNi0cGqNqO1DRXg==" },
                    { 3, true, new DateTime(2018, 5, 21, 19, 54, 33, 207, DateTimeKind.Local), "martin.oliver@ec.com", "Martin", "Oliver", "voHa7tBTzLqzhskUuRtanltSu+yVUb7bN+nu2YEylI0r9I97ab6FBbFGasaP5gvhNh7xsHjmnG9MwhMGOGvbeg==" },
                    { 2, true, new DateTime(2018, 5, 21, 19, 54, 33, 207, DateTimeKind.Local), "admin2@ec.com", "Admin2", null, "x61Ey612Kl2gpFL56FT9weDnpSo4AV8j8+qx2AuTHdRyY036xxzTTrw10Wq3+4qQyB+XURPWx1ONxp3Y3pB37A==" },
                    { 1, true, new DateTime(2018, 5, 21, 19, 54, 33, 206, DateTimeKind.Local), "admin1@ec.com", "Admin1", null, "x61Ey612Kl2gpFL56FT9weDnpSo4AV8j8+qx2AuTHdRyY036xxzTTrw10Wq3+4qQyB+XURPWx1ONxp3Y3pB37A==" },
                    { 4, true, new DateTime(2018, 5, 21, 19, 54, 33, 207, DateTimeKind.Local), "christopher.johnson@ec.com", "Christopher ", "Johnson", "LBlU1yhdXop/N50+PTh4ACbfj8ZPevCerWCF3QBSUnmosfIOXpMebDnfxK1Zj9B2OeJnQl9Ds6PW61hTJoj8Ww==" }
                });

            migrationBuilder.InsertData(
                schema: "EmployeeCalendar",
                table: "Vacation.Availability",
                columns: new[] { "ID", "Active", "DateCreated", "Description", "Name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2018, 5, 21, 19, 54, 33, 211, DateTimeKind.Local), "Available to contact", "Available" },
                    { 2, true, new DateTime(2018, 5, 21, 19, 54, 33, 211, DateTimeKind.Local), "Available to contact at certain time", "Partially available" },
                    { 3, true, new DateTime(2018, 5, 21, 19, 54, 33, 211, DateTimeKind.Local), "Unavailable to contact", "Unavailable" }
                });

            migrationBuilder.InsertData(
                schema: "EmployeeCalendar",
                table: "Vacation.Status",
                columns: new[] { "ID", "Active", "DateCreated", "Description", "Name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2018, 5, 21, 19, 54, 33, 211, DateTimeKind.Local), "Vacation is entered but not confirmed", "Entered" },
                    { 2, true, new DateTime(2018, 5, 21, 19, 54, 33, 211, DateTimeKind.Local), "Vacation is confirmed by administrator", "Confirmed" },
                    { 3, true, new DateTime(2018, 5, 21, 19, 54, 33, 211, DateTimeKind.Local), "Vacation is rejected by administrator", "Rejected" }
                });

            migrationBuilder.InsertData(
                schema: "EmployeeCalendar",
                table: "Vacation.Type",
                columns: new[] { "ID", "Active", "DateCreated", "Description", "Name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2018, 5, 21, 19, 54, 33, 211, DateTimeKind.Local), "Free days", "Vacation leave" },
                    { 2, true, new DateTime(2018, 5, 21, 19, 54, 33, 211, DateTimeKind.Local), "Sickness days", "Sick leave" }
                });

            migrationBuilder.InsertData(
                schema: "EmployeeCalendar",
                table: "User.Role",
                columns: new[] { "UserID", "RoleID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 2 },
                    { 9, 2 }
                });

            migrationBuilder.InsertData(
                schema: "EmployeeCalendar",
                table: "Vacation",
                columns: new[] { "ID", "Active", "AvailabilityID", "DateCreated", "DateFrom", "DateTo", "StatusID", "TypeID", "UserID" },
                values: new object[,]
                {
                    { 1, true, 1, new DateTime(2018, 5, 21, 19, 54, 33, 212, DateTimeKind.Local), new DateTime(2018, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 4 },
                    { 3, true, 2, new DateTime(2018, 5, 21, 19, 54, 33, 212, DateTimeKind.Local), new DateTime(2018, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 8 },
                    { 2, true, 3, new DateTime(2018, 5, 21, 19, 54, 33, 212, DateTimeKind.Local), new DateTime(2018, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User.Role_RoleID",
                schema: "EmployeeCalendar",
                table: "User.Role",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Vacation_AvailabilityID",
                schema: "EmployeeCalendar",
                table: "Vacation",
                column: "AvailabilityID");

            migrationBuilder.CreateIndex(
                name: "IX_Vacation_StatusID",
                schema: "EmployeeCalendar",
                table: "Vacation",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Vacation_TypeID",
                schema: "EmployeeCalendar",
                table: "Vacation",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Vacation_UserID",
                schema: "EmployeeCalendar",
                table: "Vacation",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holiday",
                schema: "EmployeeCalendar");

            migrationBuilder.DropTable(
                name: "User.Role",
                schema: "EmployeeCalendar");

            migrationBuilder.DropTable(
                name: "Vacation",
                schema: "EmployeeCalendar");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "EmployeeCalendar");

            migrationBuilder.DropTable(
                name: "Vacation.Availability",
                schema: "EmployeeCalendar");

            migrationBuilder.DropTable(
                name: "Vacation.Status",
                schema: "EmployeeCalendar");

            migrationBuilder.DropTable(
                name: "Vacation.Type",
                schema: "EmployeeCalendar");

            migrationBuilder.DropTable(
                name: "User",
                schema: "EmployeeCalendar");
        }
    }
}

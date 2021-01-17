using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Remontyash.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Emps",
                columns: table => new
                {
                    Empid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emps", x => x.Empid);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TypeTechnics",
                columns: table => new
                {
                    TypeTechnicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTechnics", x => x.TypeTechnicId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    USERID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LOGIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EMPID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ROLEID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.USERID);
                    table.ForeignKey(
                        name: "FK_Users_Emps",
                        column: x => x.EMPID,
                        principalTable: "Emps",
                        principalColumn: "Empid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.ROLEID,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeJobs",
                columns: table => new
                {
                    TypeJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeTechnicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeJobs", x => x.TypeJobId);
                    table.ForeignKey(
                        name: "FK_dbo.TypeJobs_dbo.TypeTechnics_TypeTechnicId",
                        column: x => x.TypeTechnicId,
                        principalTable: "TypeTechnics",
                        principalColumn: "TypeTechnicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Empid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Accepted = table.Column<DateTime>(type: "datetime", nullable: false),
                    Completed = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_dbo.Orders_dbo.Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.Orders_dbo.Emps_Empid",
                        column: x => x.Empid,
                        principalTable: "Emps",
                        principalColumn: "Empid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.Orders_dbo.TypeJobs_TypeJobId",
                        column: x => x.TypeJobId,
                        principalTable: "TypeJobs",
                        principalColumn: "TypeJobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Empid",
                table: "Orders",
                column: "Empid");

            migrationBuilder.CreateIndex(
                name: "IX_TypeJobId",
                table: "Orders",
                column: "TypeJobId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTechnicId",
                table: "TypeJobs",
                column: "TypeTechnicId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EMPID",
                table: "Users",
                column: "EMPID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ROLEID",
                table: "Users",
                column: "ROLEID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__MigrationHistory");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "TypeJobs");

            migrationBuilder.DropTable(
                name: "Emps");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TypeTechnics");
        }
    }
}

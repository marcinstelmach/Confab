using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Confab.Modules.Conferences.Core.Dal.Migrations
{
    public partial class Conference_Module_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Conferences");

            migrationBuilder.CreateTable(
                name: "Hosts",
                schema: "Conferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conferences",
                schema: "Conferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParticipantsLimit = table.Column<int>(type: "int", nullable: true),
                    From = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    To = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conferences_Hosts_HostId",
                        column: x => x.HostId,
                        principalSchema: "Conferences",
                        principalTable: "Hosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_HostId",
                schema: "Conferences",
                table: "Conferences",
                column: "HostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conferences",
                schema: "Conferences");

            migrationBuilder.DropTable(
                name: "Hosts",
                schema: "Conferences");
        }
    }
}

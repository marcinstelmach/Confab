using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Confab.Modules.Speakers.Core.Dal.Migrations
{
    public partial class Speakers_Module_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Speakers");

            migrationBuilder.CreateTable(
                name: "Speakers",
                schema: "Speakers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Speakers",
                schema: "Speakers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Confab.Modules.Tickets.Core.Dal.Migrations
{
    public partial class Tickets_Module_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Tickets");

            migrationBuilder.CreateTable(
                name: "Conferences",
                schema: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParticipantsLimit = table.Column<int>(type: "int", nullable: true),
                    From = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    To = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketSales",
                schema: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSales_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalSchema: "Tickets",
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketSaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchasedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalSchema: "Tickets",
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketSales_TicketSaleId",
                        column: x => x.TicketSaleId,
                        principalSchema: "Tickets",
                        principalTable: "TicketSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Code",
                schema: "Tickets",
                table: "Tickets",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ConferenceId",
                schema: "Tickets",
                table: "Tickets",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketSaleId",
                schema: "Tickets",
                table: "Tickets",
                column: "TicketSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSales_ConferenceId",
                schema: "Tickets",
                table: "TicketSales",
                column: "ConferenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketSales",
                schema: "Tickets");

            migrationBuilder.DropTable(
                name: "Conferences",
                schema: "Tickets");
        }
    }
}

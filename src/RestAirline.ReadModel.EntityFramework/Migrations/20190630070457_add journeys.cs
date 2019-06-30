using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestAirline.ReadModel.EntityFramework.Migrations
{
    public partial class addjourneys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journeys",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    JourneyKey = table.Column<string>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    DepartureStation = table.Column<string>(nullable: true),
                    ArriveDate = table.Column<DateTime>(nullable: false),
                    ArriveStation = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BookingReadModelId = table.Column<string>(nullable: true),
                    BookingReadModelId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journeys_Bookings_BookingReadModelId",
                        column: x => x.BookingReadModelId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Journeys_Bookings_BookingReadModelId1",
                        column: x => x.BookingReadModelId1,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FlightKey = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    DepartureStation = table.Column<string>(nullable: true),
                    ArriveDate = table.Column<DateTime>(nullable: false),
                    ArriveStation = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Aircraft = table.Column<int>(nullable: false),
                    JourneyKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Journeys_JourneyKey",
                        column: x => x.JourneyKey,
                        principalTable: "Journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_JourneyKey",
                table: "Flights",
                column: "JourneyKey",
                unique: true,
                filter: "[JourneyKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_BookingReadModelId",
                table: "Journeys",
                column: "BookingReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_BookingReadModelId1",
                table: "Journeys",
                column: "BookingReadModelId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Journeys");
        }
    }
}

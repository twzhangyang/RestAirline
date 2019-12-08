using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestAirline.Booking.ReadModel.EntityFramework.Migrations
{
    public partial class create_readmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Journeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    PassengerKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PassengerType = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    BookingReadModelId = table.Column<string>(nullable: true),
                    BookingReadModelId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passengers_Bookings_BookingReadModelId",
                        column: x => x.BookingReadModelId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Passengers_Bookings_BookingReadModelId1",
                        column: x => x.BookingReadModelId1,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FlightKey = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    DepartureStation = table.Column<string>(nullable: true),
                    ArriveDate = table.Column<DateTime>(nullable: false),
                    ArriveStation = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Aircraft = table.Column<int>(nullable: false),
                    JourneyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Journeys_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_JourneyId",
                table: "Flights",
                column: "JourneyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_BookingReadModelId",
                table: "Journeys",
                column: "BookingReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_BookingReadModelId1",
                table: "Journeys",
                column: "BookingReadModelId1");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_BookingReadModelId",
                table: "Passengers",
                column: "BookingReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_BookingReadModelId1",
                table: "Passengers",
                column: "BookingReadModelId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Journeys");

            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}

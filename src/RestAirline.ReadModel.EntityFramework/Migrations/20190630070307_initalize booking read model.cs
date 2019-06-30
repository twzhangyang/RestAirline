using Microsoft.EntityFrameworkCore.Migrations;

namespace RestAirline.ReadModel.EntityFramework.Migrations
{
    public partial class initalizebookingreadmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    DepartureStation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
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
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}

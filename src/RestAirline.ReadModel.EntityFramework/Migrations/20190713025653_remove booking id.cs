using Microsoft.EntityFrameworkCore.Migrations;

namespace RestAirline.ReadModel.EntityFramework.Migrations
{
    public partial class removebookingid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Bookings_BookingId",
                table: "Journeys");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Bookings_BookingId",
                table: "Passengers");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Passengers",
                newName: "BookingReadModelId1");

            migrationBuilder.RenameIndex(
                name: "IX_Passengers_BookingId",
                table: "Passengers",
                newName: "IX_Passengers_BookingReadModelId1");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Journeys",
                newName: "BookingReadModelId1");

            migrationBuilder.RenameIndex(
                name: "IX_Journeys_BookingId",
                table: "Journeys",
                newName: "IX_Journeys_BookingReadModelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Bookings_BookingReadModelId1",
                table: "Journeys",
                column: "BookingReadModelId1",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Bookings_BookingReadModelId1",
                table: "Passengers",
                column: "BookingReadModelId1",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Bookings_BookingReadModelId1",
                table: "Journeys");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Bookings_BookingReadModelId1",
                table: "Passengers");

            migrationBuilder.RenameColumn(
                name: "BookingReadModelId1",
                table: "Passengers",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Passengers_BookingReadModelId1",
                table: "Passengers",
                newName: "IX_Passengers_BookingId");

            migrationBuilder.RenameColumn(
                name: "BookingReadModelId1",
                table: "Journeys",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Journeys_BookingReadModelId1",
                table: "Journeys",
                newName: "IX_Journeys_BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Bookings_BookingId",
                table: "Journeys",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Bookings_BookingId",
                table: "Passengers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

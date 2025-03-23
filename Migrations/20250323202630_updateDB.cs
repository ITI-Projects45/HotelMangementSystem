using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelMangementSystem.Migrations
{
    /// <inheritdoc />
    public partial class updateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserReviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RoomReservationId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserReviewId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReviews_UserId",
                table: "UserReviews",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomReservationId",
                table: "Rooms",
                column: "RoomReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservations_ReservationId",
                table: "RoomReservations",
                column: "ReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserReviewId",
                table: "Reviews",
                column: "UserReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_UserReviews_UserReviewId",
                table: "Reviews",
                column: "UserReviewId",
                principalTable: "UserReviews",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservations_Reservations_ReservationId",
                table: "RoomReservations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomReservations_RoomReservationId",
                table: "Rooms",
                column: "RoomReservationId",
                principalTable: "RoomReservations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReviews_AspNetUsers_UserId",
                table: "UserReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_UserReviews_UserReviewId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservations_Reservations_ReservationId",
                table: "RoomReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomReservations_RoomReservationId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReviews_AspNetUsers_UserId",
                table: "UserReviews");

            migrationBuilder.DropIndex(
                name: "IX_UserReviews_UserId",
                table: "UserReviews");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomReservationId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_RoomReservations_ReservationId",
                table: "RoomReservations");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserReviewId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "RoomReservationId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "UserReviewId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserReviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

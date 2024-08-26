using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Reservation_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationFacilities");

            migrationBuilder.CreateTable(
                name: "ReservationRoomFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    FacilityId = table.Column<int>(type: "int", nullable: false),
                    RoomFacilityId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRoomFacilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationRoomFacilities_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationRoomFacilities_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationRoomFacilities_RoomFacilities_RoomFacilityId",
                        column: x => x.RoomFacilityId,
                        principalTable: "RoomFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationRoomFacilities_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRoomFacilities_FacilityId",
                table: "ReservationRoomFacilities",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRoomFacilities_ReservationId_RoomId_FacilityId",
                table: "ReservationRoomFacilities",
                columns: new[] { "ReservationId", "RoomId", "FacilityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRoomFacilities_RoomFacilityId",
                table: "ReservationRoomFacilities",
                column: "RoomFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRoomFacilities_RoomId",
                table: "ReservationRoomFacilities",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationRoomFacilities");

            migrationBuilder.CreateTable(
                name: "ReservationFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationFacilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationFacilities_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationFacilities_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFacilities_FacilityId",
                table: "ReservationFacilities",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFacilities_ReservationId_FacilityId",
                table: "ReservationFacilities",
                columns: new[] { "ReservationId", "FacilityId" },
                unique: true);
        }
    }
}

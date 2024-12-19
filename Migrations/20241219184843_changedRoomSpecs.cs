using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HavenHotel.Migrations
{
    /// <inheritdoc />
    public partial class changedRoomSpecs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Rooms",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Rooms",
                newName: "IsAvailable");
        }
    }
}

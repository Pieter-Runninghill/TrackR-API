using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackR_API.Migrations
{
    /// <inheritdoc />
    public partial class AddReimbursementValueToTripsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ReimbursementValue",
                table: "Trips",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReimbursementValue",
                table: "Trips");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IWebcamProtect.Migrations
{
    /// <inheritdoc />
    public partial class referenceCamera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Cameras",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Cameras");
        }
    }
}

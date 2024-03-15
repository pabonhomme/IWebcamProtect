using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IWebcamProtect.Migrations
{
    /// <inheritdoc />
    public partial class DetectionEvent_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CameraId",
                table: "DetectionEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetectionEvents_CameraId",
                table: "DetectionEvents",
                column: "CameraId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetectionEvents_Cameras_CameraId",
                table: "DetectionEvents",
                column: "CameraId",
                principalTable: "Cameras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetectionEvents_Cameras_CameraId",
                table: "DetectionEvents");

            migrationBuilder.DropIndex(
                name: "IX_DetectionEvents_CameraId",
                table: "DetectionEvents");

            migrationBuilder.DropColumn(
                name: "CameraId",
                table: "DetectionEvents");
        }
    }
}

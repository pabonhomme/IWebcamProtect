using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IWebcamProtect.Migrations
{
    /// <inheritdoc />
    public partial class deletePropCamera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WatchDuration",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "WatchTimeStart",
                table: "Cameras");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WatchDuration",
                table: "Cameras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "WatchTimeStart",
                table: "Cameras",
                type: "datetime2",
                nullable: true);
        }
    }
}

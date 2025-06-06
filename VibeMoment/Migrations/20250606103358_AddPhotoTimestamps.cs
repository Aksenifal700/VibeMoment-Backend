using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibeMoment.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Photos",
                newName: "Title");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                table: "Photos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedAt",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Photos",
                newName: "Name");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibeMoment.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtToPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Photos",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Photos");
        }
    }
}

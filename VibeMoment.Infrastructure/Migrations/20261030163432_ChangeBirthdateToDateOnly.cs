using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibeMoment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBirthdateToDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "SET LOCAL TIME ZONE 'UTC'");
            
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Birthdate",
                table: "UserProfiles",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "UserProfiles",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibeMoment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTitleIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photos_Title",
                table: "Photos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Photos_Title",
                table: "Photos",
                column: "Title");
        }
    }
}

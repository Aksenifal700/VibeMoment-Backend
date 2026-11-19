using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibeMoment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBlobNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlobName",
                table: "Photos",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlobName",
                table: "Photos");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibeMoment.Migrations
{
    /// <inheritdoc />
    public partial class AddedDataFieldToPhotoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Photos",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Photos");
        }
    }
}

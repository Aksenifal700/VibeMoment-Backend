using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibeMoment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BackfillUserProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                 INSERT INTO "UserProfiles" ("Id", "UserId")
                                 SELECT gen_random_uuid(), u."Id"
                                 FROM "Users" AS u
                                 WHERE NOT EXISTS (
                                     SELECT 1
                                     FROM "UserProfiles" AS p
                                     WHERE p."UserId" = u."Id"
                                 );
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

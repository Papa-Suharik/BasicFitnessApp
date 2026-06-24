using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicFitnessApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedIndexesForRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserId_ExpiresOn",
                table: "RefreshTokens");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId_ExpiresOn",
                table: "RefreshTokens",
                columns: new[] { "UserId", "ExpiresOn" },
                filter: "\"IsRevoked\" = false");
        }
    }
}

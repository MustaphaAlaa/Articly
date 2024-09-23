using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articly.Entites.Migrations
{
    /// <inheritdoc />
    public partial class Tag_DisplayName_Unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tags_DisplayName",
                table: "Tags",
                column: "DisplayName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_DisplayName",
                table: "Tags");
        }
    }
}

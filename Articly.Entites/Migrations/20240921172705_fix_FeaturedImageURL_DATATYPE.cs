using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articly.Entites.Migrations
{
    /// <inheritdoc />
    public partial class fix_FeaturedImageURL_DATATYPE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FeaturedImaageUrl",
                table: "Articles",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FeaturedImaageUrl",
                table: "Articles",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}

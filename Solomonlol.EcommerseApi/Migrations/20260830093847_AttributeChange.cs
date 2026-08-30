using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solomonlol.EcommerseApi.Migrations
{
    /// <inheritdoc />
    public partial class AttributeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "ProductAttributes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "ProductAttributes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

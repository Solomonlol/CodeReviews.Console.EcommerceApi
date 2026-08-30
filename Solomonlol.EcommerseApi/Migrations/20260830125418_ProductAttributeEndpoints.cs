using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solomonlol.EcommerseApi.Migrations
{
    /// <inheritdoc />
    public partial class ProductAttributeEndpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributeValues",
                table: "ProductAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeValues_ProductId_ProductAttributeId",
                table: "ProductAttributeValues");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductAttributeValues");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductAttributes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributeValues",
                table: "ProductAttributeValues",
                columns: new[] { "ProductId", "ProductAttributeId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_Name",
                table: "ProductAttributes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributeValues",
                table: "ProductAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_Name",
                table: "ProductAttributes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductAttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributeValues",
                table: "ProductAttributeValues",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductId_ProductAttributeId",
                table: "ProductAttributeValues",
                columns: new[] { "ProductId", "ProductAttributeId" },
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solomonlol.EcommerseApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_CategoryId",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_Name",
                table: "ProductAttributes");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_CategoryId_Name",
                table: "ProductAttributes",
                columns: new[] { "CategoryId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_CategoryId_Name",
                table: "ProductAttributes");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_CategoryId",
                table: "ProductAttributes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_Name",
                table: "ProductAttributes",
                column: "Name",
                unique: true);
        }
    }
}

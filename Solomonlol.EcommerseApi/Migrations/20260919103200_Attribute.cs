using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solomonlol.EcommerseApi.Migrations
{
    /// <inheritdoc />
    public partial class Attribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValues_ProductAttributes_ProductAttributeId",
                table: "ProductAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributeValues",
                table: "ProductAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeValues_ProductAttributeId",
                table: "ProductAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributes",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "ProductAttributeId",
                table: "ProductAttributeValues");

            migrationBuilder.AddColumn<string>(
                name: "ProductAttributeName",
                table: "ProductAttributeValues",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductAttributes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributeValues",
                table: "ProductAttributeValues",
                columns: new[] { "ProductId", "ProductAttributeName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributes",
                table: "ProductAttributes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductAttributeName",
                table: "ProductAttributeValues",
                column: "ProductAttributeName");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValues_ProductAttributes_ProductAttributeName",
                table: "ProductAttributeValues",
                column: "ProductAttributeName",
                principalTable: "ProductAttributes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValues_ProductAttributes_ProductAttributeName",
                table: "ProductAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributeValues",
                table: "ProductAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeValues_ProductAttributeName",
                table: "ProductAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributes",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "ProductAttributeName",
                table: "ProductAttributeValues");

            migrationBuilder.AddColumn<int>(
                name: "ProductAttributeId",
                table: "ProductAttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductAttributes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributeValues",
                table: "ProductAttributeValues",
                columns: new[] { "ProductId", "ProductAttributeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributes",
                table: "ProductAttributes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductAttributeId",
                table: "ProductAttributeValues",
                column: "ProductAttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValues_ProductAttributes_ProductAttributeId",
                table: "ProductAttributeValues",
                column: "ProductAttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

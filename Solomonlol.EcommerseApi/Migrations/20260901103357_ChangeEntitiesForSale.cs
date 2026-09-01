using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solomonlol.EcommerseApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEntitiesForSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndedAt",
                table: "Sales",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnded",
                table: "Sales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Sales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndedAt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsEnded",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");
        }
    }
}

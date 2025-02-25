using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleNavigationToSaleItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Sales_SaleId1",
                table: "SaleItems");

            migrationBuilder.DropIndex(
                name: "IX_SaleItems_SaleId1",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "SaleId1",
                table: "SaleItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SaleId1",
                table: "SaleItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleId1",
                table: "SaleItems",
                column: "SaleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Sales_SaleId1",
                table: "SaleItems",
                column: "SaleId1",
                principalTable: "Sales",
                principalColumn: "Id");
        }
    }
}

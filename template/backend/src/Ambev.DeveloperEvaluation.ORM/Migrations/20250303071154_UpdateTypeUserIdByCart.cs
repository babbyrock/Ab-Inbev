using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    public partial class UpdateTypeUserIdByCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "NewUserId",
            table: "Carts",
            type: "uuid",
            nullable: false,
            defaultValueSql: "gen_random_uuid()");

        migrationBuilder.DropColumn(
            name: "UserId",
            table: "Carts");
        migrationBuilder.RenameColumn(
            name: "NewUserId",
            table: "Carts",
            newName: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "UserId",
            table: "Carts",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.DropColumn(
            name: "NewUserId",
            table: "Carts");
    }
}
}
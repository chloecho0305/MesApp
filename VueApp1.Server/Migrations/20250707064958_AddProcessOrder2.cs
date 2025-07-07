using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VueApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddProcessOrder2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "WorkOrders",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "WorkOrders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "WorkOrders",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "WorkOrders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "WorkOrders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProductNumber",
                table: "WorkOrders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "ProductNumber",
                table: "WorkOrders");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "WorkOrders",
                newName: "OrderNo");
        }
    }
}

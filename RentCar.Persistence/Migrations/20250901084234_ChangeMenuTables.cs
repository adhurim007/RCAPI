using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMenuTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Route",
                table: "Menus",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Menus",
                newName: "Title");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Menus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Claim",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Menus",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Menus",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Menus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasSubMenu",
                table: "Menus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                table: "Menus",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Menus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Menus",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortNumber",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentId",
                table: "Menus",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus",
                column: "ParentId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_ParentId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "Claim",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "HasSubMenu",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "SortNumber",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Menus");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Menus",
                newName: "Route");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Menus",
                newName: "Name");
        }
    }
}

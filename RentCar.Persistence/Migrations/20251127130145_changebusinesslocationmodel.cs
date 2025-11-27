using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changebusinesslocationmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "BusinessLocations");

            migrationBuilder.DropColumn(
                name: "State",
                table: "BusinessLocations");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "BusinessLocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "BusinessLocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLocations_CityId",
                table: "BusinessLocations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLocations_StateId",
                table: "BusinessLocations",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessLocations_Cities_CityId",
                table: "BusinessLocations",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessLocations_States_StateId",
                table: "BusinessLocations",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessLocations_Cities_CityId",
                table: "BusinessLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessLocations_States_StateId",
                table: "BusinessLocations");

            migrationBuilder.DropIndex(
                name: "IX_BusinessLocations_CityId",
                table: "BusinessLocations");

            migrationBuilder.DropIndex(
                name: "IX_BusinessLocations_StateId",
                table: "BusinessLocations");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "BusinessLocations");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "BusinessLocations");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "BusinessLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "BusinessLocations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

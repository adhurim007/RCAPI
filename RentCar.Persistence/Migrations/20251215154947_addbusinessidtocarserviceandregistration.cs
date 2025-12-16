using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addbusinessidtocarserviceandregistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                table: "CarServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                table: "CarRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarServices_BusinessId",
                table: "CarServices",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRegistrations_BusinessId",
                table: "CarRegistrations",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRegistrations_Businesses_BusinessId",
                table: "CarRegistrations",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarServices_Businesses_BusinessId",
                table: "CarServices",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRegistrations_Businesses_BusinessId",
                table: "CarRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_CarServices_Businesses_BusinessId",
                table: "CarServices");

            migrationBuilder.DropIndex(
                name: "IX_CarServices_BusinessId",
                table: "CarServices");

            migrationBuilder.DropIndex(
                name: "IX_CarRegistrations_BusinessId",
                table: "CarRegistrations");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "CarServices");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "CarRegistrations");
        }
    }
}

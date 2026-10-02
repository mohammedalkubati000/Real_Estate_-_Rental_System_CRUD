using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate___Rental_System_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class AddUuidRentalContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "RentalContracts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_Uuid",
                table: "RentalContracts",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_Uuid",
                table: "Properties",
                column: "Uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RentalContracts_Uuid",
                table: "RentalContracts");

            migrationBuilder.DropIndex(
                name: "IX_Properties_Uuid",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Properties");
        }
    }
}

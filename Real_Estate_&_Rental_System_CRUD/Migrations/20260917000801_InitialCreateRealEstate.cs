using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate___Rental_System_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateRealEstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalContracts_Propertys_PropertyId",
                table: "RentalContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Propertys",
                table: "Propertys");

            migrationBuilder.RenameTable(
                name: "Propertys",
                newName: "Properties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalContracts_Properties_PropertyId",
                table: "RentalContracts",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalContracts_Properties_PropertyId",
                table: "RentalContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "Propertys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propertys",
                table: "Propertys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalContracts_Propertys_PropertyId",
                table: "RentalContracts",
                column: "PropertyId",
                principalTable: "Propertys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

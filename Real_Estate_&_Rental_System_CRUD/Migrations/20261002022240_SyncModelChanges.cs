using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate___Rental_System_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tenants_Uuid",
                table: "Tenants");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Tenants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Uuid",
                table: "Tenants",
                column: "Uuid",
                unique: true,
                filter: "[Uuid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Uuid",
                table: "Payments",
                column: "Uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tenants_Uuid",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Payments_Uuid",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Payments");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Tenants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Uuid",
                table: "Tenants",
                column: "Uuid",
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate___Rental_System_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantUuid : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Uuid",
                table: "Tenants",
                column: "Uuid",
                unique: true,
                filter: "[Uuid] IS NOT NULL");
        }
    }
}

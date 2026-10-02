using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate___Rental_System_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class AddUuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Tenants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Tenants");
        }
    }
}

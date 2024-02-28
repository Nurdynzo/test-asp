using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountToEditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "AppEditions",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppEditions_ExpiringEditionId",
                table: "AppEditions",
                column: "ExpiringEditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEditions_AppEditions_ExpiringEditionId",
                table: "AppEditions",
                column: "ExpiringEditionId",
                principalTable: "AppEditions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppEditions_AppEditions_ExpiringEditionId",
                table: "AppEditions");

            migrationBuilder.DropIndex(
                name: "IX_AppEditions_ExpiringEditionId",
                table: "AppEditions");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "AppEditions");
        }
    }
}

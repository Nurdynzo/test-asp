using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMedicationAddDeletedUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DiscontinueUserId",
                table: "Medications",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DiscontinueUserId",
                table: "Medications",
                column: "DiscontinueUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_AppUsers_DiscontinueUserId",
                table: "Medications",
                column: "DiscontinueUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_AppUsers_DiscontinueUserId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_DiscontinueUserId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "DiscontinueUserId",
                table: "Medications");
        }
    }
}

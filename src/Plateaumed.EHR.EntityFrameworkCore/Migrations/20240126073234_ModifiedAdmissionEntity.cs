using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedAdmissionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WardBedId",
                table: "Admissions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WardId",
                table: "Admissions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_WardBedId",
                table: "Admissions",
                column: "WardBedId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_WardId",
                table: "Admissions",
                column: "WardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_WardBeds_WardBedId",
                table: "Admissions",
                column: "WardBedId",
                principalTable: "WardBeds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Wards_WardId",
                table: "Admissions",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_WardBeds_WardBedId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Wards_WardId",
                table: "Admissions");

            migrationBuilder.DropIndex(
                name: "IX_Admissions_WardBedId",
                table: "Admissions");

            migrationBuilder.DropIndex(
                name: "IX_Admissions_WardId",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "WardBedId",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Admissions");
        }
    }
}

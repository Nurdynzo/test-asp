using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class PatientUserIdForeignKeyRelaxed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AppUsers_UserId",
                table: "Patients");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AppUsers_UserId",
                table: "Patients",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AppUsers_UserId",
                table: "Patients");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AppUsers_UserId",
                table: "Patients",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddVaccinationProcedureId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcedureEntryType",
                table: "Vaccinations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProcedureId",
                table: "Vaccinations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_ProcedureId",
                table: "Vaccinations",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccinations_Procedures_ProcedureId",
                table: "Vaccinations",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccinations_Procedures_ProcedureId",
                table: "Vaccinations");

            migrationBuilder.DropIndex(
                name: "IX_Vaccinations_ProcedureId",
                table: "Vaccinations");

            migrationBuilder.DropColumn(
                name: "ProcedureEntryType",
                table: "Vaccinations");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "Vaccinations");
        }
    }
}

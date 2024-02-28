using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInputeNoteAddProcedureId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProcedureId",
                table: "InputNotes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InputNotes_ProcedureId",
                table: "InputNotes",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_InputNotes_Procedures_ProcedureId",
                table: "InputNotes",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InputNotes_Procedures_ProcedureId",
                table: "InputNotes");

            migrationBuilder.DropIndex(
                name: "IX_InputNotes_ProcedureId",
                table: "InputNotes");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "InputNotes");
        }
    }
}

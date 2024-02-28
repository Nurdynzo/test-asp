using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNurseSpecializedProcedureId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecializedProcedureNurseDetails_SpecializedProcedures_Spec~",
                table: "SpecializedProcedureNurseDetails");

            migrationBuilder.RenameColumn(
                name: "SpecializedProcedureProcedureId",
                table: "SpecializedProcedureNurseDetails",
                newName: "ProcedureId");

            migrationBuilder.RenameIndex(
                name: "IX_SpecializedProcedureNurseDetails_SpecializedProcedureProced~",
                table: "SpecializedProcedureNurseDetails",
                newName: "IX_SpecializedProcedureNurseDetails_ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecializedProcedureNurseDetails_Procedures_ProcedureId",
                table: "SpecializedProcedureNurseDetails",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecializedProcedureNurseDetails_Procedures_ProcedureId",
                table: "SpecializedProcedureNurseDetails");

            migrationBuilder.RenameColumn(
                name: "ProcedureId",
                table: "SpecializedProcedureNurseDetails",
                newName: "SpecializedProcedureProcedureId");

            migrationBuilder.RenameIndex(
                name: "IX_SpecializedProcedureNurseDetails_ProcedureId",
                table: "SpecializedProcedureNurseDetails",
                newName: "IX_SpecializedProcedureNurseDetails_SpecializedProcedureProced~");


            migrationBuilder.AddForeignKey(
                name: "FK_SpecializedProcedureNurseDetails_SpecializedProcedures_Spec~",
                table: "SpecializedProcedureNurseDetails",
                column: "SpecializedProcedureProcedureId",
                principalTable: "SpecializedProcedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationshipToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InterpretedByStaffId",
                table: "ProcedureConsentStatement",
                newName: "InterpretedByStaffUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureConsentStatement_InterpretedByStaffUserId",
                table: "ProcedureConsentStatement",
                column: "InterpretedByStaffUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureConsentStatement_AppUsers_InterpretedByStaffUserId",
                table: "ProcedureConsentStatement",
                column: "InterpretedByStaffUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureConsentStatement_AppUsers_InterpretedByStaffUserId",
                table: "ProcedureConsentStatement");

            migrationBuilder.DropIndex(
                name: "IX_ProcedureConsentStatement_InterpretedByStaffUserId",
                table: "ProcedureConsentStatement");

            migrationBuilder.RenameColumn(
                name: "InterpretedByStaffUserId",
                table: "ProcedureConsentStatement",
                newName: "InterpretedByStaffId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSpecializedProceduresEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecializedProcedures_StaffMembers_AnaesthetistId",
                table: "SpecializedProcedures");

            migrationBuilder.DropIndex(
                name: "IX_SpecializedProcedures_AnaesthetistId",
                table: "SpecializedProcedures");

            migrationBuilder.RenameColumn(
                name: "Procedures",
                table: "SpecializedProcedures",
                newName: "ProcedureName");

            migrationBuilder.RenameColumn(
                name: "AnaesthetistId",
                table: "SpecializedProcedures",
                newName: "SnowmedId");

            migrationBuilder.AddColumn<long>(
                name: "AnaesthetistUserId",
                table: "SpecializedProcedures",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmedByConsultantName",
                table: "ProcedureConsentStatement",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmedByPrimarySpecialistName",
                table: "ProcedureConsentStatement",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecializedProcedures_AnaesthetistUserId",
                table: "SpecializedProcedures",
                column: "AnaesthetistUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecializedProcedures_AppUsers_AnaesthetistUserId",
                table: "SpecializedProcedures",
                column: "AnaesthetistUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecializedProcedures_AppUsers_AnaesthetistUserId",
                table: "SpecializedProcedures");

            migrationBuilder.DropIndex(
                name: "IX_SpecializedProcedures_AnaesthetistUserId",
                table: "SpecializedProcedures");

            migrationBuilder.DropColumn(
                name: "AnaesthetistUserId",
                table: "SpecializedProcedures");

            migrationBuilder.DropColumn(
                name: "ConfirmedByConsultantName",
                table: "ProcedureConsentStatement");

            migrationBuilder.DropColumn(
                name: "ConfirmedByPrimarySpecialistName",
                table: "ProcedureConsentStatement");

            migrationBuilder.RenameColumn(
                name: "SnowmedId",
                table: "SpecializedProcedures",
                newName: "AnaesthetistId");

            migrationBuilder.RenameColumn(
                name: "ProcedureName",
                table: "SpecializedProcedures",
                newName: "Procedures");

            migrationBuilder.CreateIndex(
                name: "IX_SpecializedProcedures_AnaesthetistId",
                table: "SpecializedProcedures",
                column: "AnaesthetistId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecializedProcedures_StaffMembers_AnaesthetistId",
                table: "SpecializedProcedures",
                column: "AnaesthetistId",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }
    }
}

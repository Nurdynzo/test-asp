using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedProcedureConsentStatementEntity02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsultantId",
                table: "ProcedureConsentStatement");

            migrationBuilder.DropColumn(
                name: "PrimarySpecialistId",
                table: "ProcedureConsentStatement");

            migrationBuilder.AddColumn<string>(
                name: "ConsultantName",
                table: "ProcedureConsentStatement",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimarySpecialistName",
                table: "ProcedureConsentStatement",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsultantName",
                table: "ProcedureConsentStatement");

            migrationBuilder.DropColumn(
                name: "PrimarySpecialistName",
                table: "ProcedureConsentStatement");

            migrationBuilder.AddColumn<long>(
                name: "ConsultantId",
                table: "ProcedureConsentStatement",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PrimarySpecialistId",
                table: "ProcedureConsentStatement",
                type: "bigint",
                nullable: true);
        }
    }
}

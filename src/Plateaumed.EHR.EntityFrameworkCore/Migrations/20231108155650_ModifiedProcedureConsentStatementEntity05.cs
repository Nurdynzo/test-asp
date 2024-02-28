using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedProcedureConsentStatementEntity05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NextOfKinOrGuardianGovIssuedIdNumber",
                table: "ProcedureConsentStatement",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryNextOfKinOrGuardianGovIssuedIdNumber",
                table: "ProcedureConsentStatement",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondarySignatureOfWitnessGovIssuedIdNumber",
                table: "ProcedureConsentStatement",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignatureOfWitnessGovIssuedIdNumber",
                table: "ProcedureConsentStatement",
                type: "varchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextOfKinOrGuardianGovIssuedIdNumber",
                table: "ProcedureConsentStatement");

            migrationBuilder.DropColumn(
                name: "SecondaryNextOfKinOrGuardianGovIssuedIdNumber",
                table: "ProcedureConsentStatement");

            migrationBuilder.DropColumn(
                name: "SecondarySignatureOfWitnessGovIssuedIdNumber",
                table: "ProcedureConsentStatement");

            migrationBuilder.DropColumn(
                name: "SignatureOfWitnessGovIssuedIdNumber",
                table: "ProcedureConsentStatement");
        }
    }
}

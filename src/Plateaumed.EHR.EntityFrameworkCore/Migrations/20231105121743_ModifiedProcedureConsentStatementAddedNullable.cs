using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Authorization.Users;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedProcedureConsentStatementAddedNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "UsePatientAuthorizedNextOfKinOrGuardian",
                table: "ProcedureConsentStatement",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<IdentificationType>(
                name: "SignatureOfWitnessGovIssuedId",
                table: "ProcedureConsentStatement",
                type: "identification_type",
                nullable: true,
                oldClrType: typeof(IdentificationType),
                oldType: "identification_type");

            migrationBuilder.AlterColumn<IdentificationType>(
                name: "SecondarySignatureOfWitnessGovIssuedId",
                table: "ProcedureConsentStatement",
                type: "identification_type",
                nullable: true,
                oldClrType: typeof(IdentificationType),
                oldType: "identification_type");

            migrationBuilder.AlterColumn<IdentificationType>(
                name: "SecondaryNextOfKinOrGuardianGovIssuedId",
                table: "ProcedureConsentStatement",
                type: "identification_type",
                nullable: true,
                oldClrType: typeof(IdentificationType),
                oldType: "identification_type");

            migrationBuilder.AlterColumn<IdentificationType>(
                name: "NextOfKinOrGuardianGovIssuedId",
                table: "ProcedureConsentStatement",
                type: "identification_type",
                nullable: true,
                oldClrType: typeof(IdentificationType),
                oldType: "identification_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "UsePatientAuthorizedNextOfKinOrGuardian",
                table: "ProcedureConsentStatement",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<IdentificationType>(
                name: "SignatureOfWitnessGovIssuedId",
                table: "ProcedureConsentStatement",
                type: "identification_type",
                nullable: false,
                defaultValue: IdentificationType.StateIdCard,
                oldClrType: typeof(IdentificationType),
                oldType: "identification_type",
                oldNullable: true);

            migrationBuilder.AlterColumn<IdentificationType>(
                name: "SecondarySignatureOfWitnessGovIssuedId",
                table: "ProcedureConsentStatement",
                type: "identification_type",
                nullable: false,
                defaultValue: IdentificationType.StateIdCard,
                oldClrType: typeof(IdentificationType),
                oldType: "identification_type",
                oldNullable: true);

            migrationBuilder.AlterColumn<IdentificationType>(
                name: "SecondaryNextOfKinOrGuardianGovIssuedId",
                table: "ProcedureConsentStatement",
                type: "identification_type",
                nullable: false,
                defaultValue: IdentificationType.StateIdCard,
                oldClrType: typeof(IdentificationType),
                oldType: "identification_type",
                oldNullable: true);

            migrationBuilder.AlterColumn<IdentificationType>(
                name: "NextOfKinOrGuardianGovIssuedId",
                table: "ProcedureConsentStatement",
                type: "identification_type",
                nullable: false,
                defaultValue: IdentificationType.StateIdCard,
                oldClrType: typeof(IdentificationType),
                oldType: "identification_type",
                oldNullable: true);
        }
    }
}

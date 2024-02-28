using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Authorization.Users;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedProcedureConsentStatementEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcedureConsentStatement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    IntendedBenefits = table.Column<string>(type: "varchar(2000)", nullable: true),
                    FrequentlyOccuringRisks = table.Column<string>(type: "varchar(2000)", nullable: true),
                    ExtraProcedures = table.Column<string>(type: "varchar(2000)", nullable: true),
                    IsRegionalAnaesthesia = table.Column<bool>(type: "boolean", nullable: false),
                    IsLocalAnaesthesia = table.Column<bool>(type: "boolean", nullable: false),
                    IsSedationAnaesthesia = table.Column<bool>(type: "boolean", nullable: false),
                    UsePatientAuthorizedNextOfKinOrGuardian = table.Column<bool>(type: "boolean", nullable: false),
                    SignatureOfNextOfKinOrGuardian = table.Column<string>(type: "varchar(250)", nullable: true),
                    NextOfKinOrGuardianGovIssuedId = table.Column<IdentificationType>(type: "identification_type", nullable: false),
                    SignatureOfWitness = table.Column<string>(type: "varchar(250)", nullable: true),
                    SignatureOfWitnessGovIssuedId = table.Column<IdentificationType>(type: "identification_type", nullable: false),
                    SecondaryLanguageOfInterpretation = table.Column<string>(type: "varchar(250)", nullable: true),
                    InterpretedByStaffId = table.Column<long>(type: "bigint", nullable: true),
                    SecondarySignatureOfNextOfKinOrGuardian = table.Column<string>(type: "varchar(250)", nullable: true),
                    SecondaryNextOfKinOrGuardianGovIssuedId = table.Column<IdentificationType>(type: "identification_type", nullable: false),
                    SecondarySignatureOfWitness = table.Column<string>(type: "varchar(250)", nullable: true),
                    SecondarySignatureOfWitnessGovIssuedId = table.Column<IdentificationType>(type: "identification_type", nullable: false),
                    ConsultantId = table.Column<long>(type: "bigint", nullable: true),
                    PrimarySpecialistId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureConsentStatement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcedureConsentStatement_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedureConsentStatement_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureConsentStatement_PatientId",
                table: "ProcedureConsentStatement",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureConsentStatement_ProcedureId",
                table: "ProcedureConsentStatement",
                column: "ProcedureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcedureConsentStatement");
        }
    }
}

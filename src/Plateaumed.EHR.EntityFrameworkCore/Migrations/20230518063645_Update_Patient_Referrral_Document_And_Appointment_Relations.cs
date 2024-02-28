using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientReferrralDocumentAndAppointmentRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_AttendingClinic",
                table: "PatientAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_ReferringClinic",
                table: "PatientAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_PatientReferral_PatientReferralId",
                table: "PatientAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_StaffMembers_AttendingPhysician",
                table: "PatientAppointments");

            migrationBuilder.DropTable(
                name: "PatientReferral");

            migrationBuilder.RenameColumn(
                name: "ReferringClinic",
                table: "PatientAppointments",
                newName: "ReferringClinicId");

            migrationBuilder.RenameColumn(
                name: "PatientReferralId",
                table: "PatientAppointments",
                newName: "PatientReferralDocumentId");

            migrationBuilder.RenameColumn(
                name: "AttendingPhysician",
                table: "PatientAppointments",
                newName: "AttendingPhysicianId");

            migrationBuilder.RenameColumn(
                name: "AttendingClinic",
                table: "PatientAppointments",
                newName: "AttendingClinicId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAppointments_ReferringClinic",
                table: "PatientAppointments",
                newName: "IX_PatientAppointments_ReferringClinicId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAppointments_PatientReferralId",
                table: "PatientAppointments",
                newName: "IX_PatientAppointments_PatientReferralDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAppointments_AttendingPhysician",
                table: "PatientAppointments",
                newName: "IX_PatientAppointments_AttendingPhysicianId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAppointments_AttendingClinic",
                table: "PatientAppointments",
                newName: "IX_PatientAppointments_AttendingClinicId");

            migrationBuilder.CreateTable(
                name: "PatientReferralDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReferringHealthCareProvider = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    DiagnosisSummary = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    ReferralDocument = table.Column<Guid>(type: "uuid", nullable: true),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_PatientReferralDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientReferralDocuments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientReferralDocuments_PatientId",
                table: "PatientReferralDocuments",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_AttendingClinicId",
                table: "PatientAppointments",
                column: "AttendingClinicId",
                principalTable: "AppOrganizationUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_ReferringClinicId",
                table: "PatientAppointments",
                column: "ReferringClinicId",
                principalTable: "AppOrganizationUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_PatientReferralDocuments_PatientReferra~",
                table: "PatientAppointments",
                column: "PatientReferralDocumentId",
                principalTable: "PatientReferralDocuments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_StaffMembers_AttendingPhysicianId",
                table: "PatientAppointments",
                column: "AttendingPhysicianId",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_AttendingClinicId",
                table: "PatientAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_ReferringClinicId",
                table: "PatientAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_PatientReferralDocuments_PatientReferra~",
                table: "PatientAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_StaffMembers_AttendingPhysicianId",
                table: "PatientAppointments");

            migrationBuilder.DropTable(
                name: "PatientReferralDocuments");

            migrationBuilder.RenameColumn(
                name: "ReferringClinicId",
                table: "PatientAppointments",
                newName: "ReferringClinic");

            migrationBuilder.RenameColumn(
                name: "PatientReferralDocumentId",
                table: "PatientAppointments",
                newName: "PatientReferralId");

            migrationBuilder.RenameColumn(
                name: "AttendingPhysicianId",
                table: "PatientAppointments",
                newName: "AttendingPhysician");

            migrationBuilder.RenameColumn(
                name: "AttendingClinicId",
                table: "PatientAppointments",
                newName: "AttendingClinic");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAppointments_ReferringClinicId",
                table: "PatientAppointments",
                newName: "IX_PatientAppointments_ReferringClinic");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAppointments_PatientReferralDocumentId",
                table: "PatientAppointments",
                newName: "IX_PatientAppointments_PatientReferralId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAppointments_AttendingPhysicianId",
                table: "PatientAppointments",
                newName: "IX_PatientAppointments_AttendingPhysician");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAppointments_AttendingClinicId",
                table: "PatientAppointments",
                newName: "IX_PatientAppointments_AttendingClinic");

            migrationBuilder.CreateTable(
                name: "PatientReferral",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Diagnosis = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    ReferralDocument = table.Column<Guid>(type: "uuid", nullable: true),
                    ReferringHospital = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientReferral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientReferral_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientReferral_PatientId",
                table: "PatientReferral",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientReferral_TenantId",
                table: "PatientReferral",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_AttendingClinic",
                table: "PatientAppointments",
                column: "AttendingClinic",
                principalTable: "AppOrganizationUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_ReferringClinic",
                table: "PatientAppointments",
                column: "ReferringClinic",
                principalTable: "AppOrganizationUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_PatientReferral_PatientReferralId",
                table: "PatientAppointments",
                column: "PatientReferralId",
                principalTable: "PatientReferral",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_StaffMembers_AttendingPhysician",
                table: "PatientAppointments",
                column: "AttendingPhysician",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }
    }
}

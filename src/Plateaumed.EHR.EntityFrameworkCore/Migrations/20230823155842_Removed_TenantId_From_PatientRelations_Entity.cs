using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Authorization.Users;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTenantIdFromPatientRelationsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PatientRelations_TenantId",
                table: "PatientRelations");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "PatientRelations");

            migrationBuilder.AlterColumn<IdentificationType>(
                name: "IdentificationType",
                table: "PatientRelations",
                type: "identification_type",
                nullable: true,
                oldClrType: typeof(IdentificationType),
                oldType: "identification_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<IdentificationType>(
                name: "IdentificationType",
                table: "PatientRelations",
                type: "identification_type",
                nullable: false,
                defaultValue: IdentificationType.StateIdCard,
                oldClrType: typeof(IdentificationType),
                oldType: "identification_type",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "PatientRelations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientRelations_TenantId",
                table: "PatientRelations",
                column: "TenantId");
        }
    }
}

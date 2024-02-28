using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Authorization.Users;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPatientRelationToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentificationCode",
                table: "PatientRelations",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true
            );

            migrationBuilder.AddColumn<IdentificationType>(
                name: "IdentificationType",
                table: "PatientRelations",
                type: "identification_type",
                nullable: false,
                defaultValue: IdentificationType.StateIdCard
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IdentificationCode", table: "PatientRelations");

            migrationBuilder.DropColumn(name: "IdentificationType", table: "PatientRelations");
        }
    }
}

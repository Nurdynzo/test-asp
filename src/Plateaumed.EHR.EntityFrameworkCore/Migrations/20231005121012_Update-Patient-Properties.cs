using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Patients;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Religion>(
                name: "Religion",
                table: "Patients",
                type: "religion",
                nullable: true,
                oldClrType: typeof(Religion),
                oldType: "religion");

            migrationBuilder.AlterColumn<MaritalStatus>(
                name: "MaritalStatus",
                table: "Patients",
                type: "marital_status",
                nullable: true,
                oldClrType: typeof(MaritalStatus),
                oldType: "marital_status");

            migrationBuilder.AlterColumn<BloodGroup>(
                name: "BloodGroup",
                table: "Patients",
                type: "blood_group",
                nullable: true,
                oldClrType: typeof(BloodGroup),
                oldType: "blood_group");

            migrationBuilder.AlterColumn<BloodGenotype>(
                name: "BloodGenotype",
                table: "Patients",
                type: "blood_genotype",
                nullable: true,
                oldClrType: typeof(BloodGenotype),
                oldType: "blood_genotype");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Religion>(
                name: "Religion",
                table: "Patients",
                type: "religion",
                nullable: false,
                defaultValue: Religion.Christianity,
                oldClrType: typeof(Religion),
                oldType: "religion",
                oldNullable: true);

            migrationBuilder.AlterColumn<MaritalStatus>(
                name: "MaritalStatus",
                table: "Patients",
                type: "marital_status",
                nullable: false,
                defaultValue: MaritalStatus.Single,
                oldClrType: typeof(MaritalStatus),
                oldType: "marital_status",
                oldNullable: true);

            migrationBuilder.AlterColumn<BloodGroup>(
                name: "BloodGroup",
                table: "Patients",
                type: "blood_group",
                nullable: false,
                defaultValue: BloodGroup.A_Positive,
                oldClrType: typeof(BloodGroup),
                oldType: "blood_group",
                oldNullable: true);

            migrationBuilder.AlterColumn<BloodGenotype>(
                name: "BloodGenotype",
                table: "Patients",
                type: "blood_genotype",
                nullable: false,
                defaultValue: BloodGenotype.AA,
                oldClrType: typeof(BloodGenotype),
                oldType: "blood_genotype",
                oldNullable: true);
        }
    }
}

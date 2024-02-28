using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifyReferralAndConsultTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceivingConsultant",
                table: "PatientReferralOrConsultLetters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceivingHospital",
                table: "PatientReferralOrConsultLetters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceivingUnit",
                table: "PatientReferralOrConsultLetters",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "Facilities",
                type: "character varying(7)",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivingConsultant",
                table: "PatientReferralOrConsultLetters");

            migrationBuilder.DropColumn(
                name: "ReceivingHospital",
                table: "PatientReferralOrConsultLetters");

            migrationBuilder.DropColumn(
                name: "ReceivingUnit",
                table: "PatientReferralOrConsultLetters");

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "Facilities",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7,
                oldNullable: true);
        }
    }
}

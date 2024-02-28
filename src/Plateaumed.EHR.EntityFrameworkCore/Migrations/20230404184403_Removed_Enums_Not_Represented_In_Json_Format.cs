using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RemovedEnumsNotRepresentedInJsonFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "TenantDocuments");

            migrationBuilder.DropColumn(
                name: "BloodGenotype",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BloodGroup",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Relationship",
                table: "PatientRelations");

            migrationBuilder.DropColumn(
                name: "BenefiaryType",
                table: "PatientInsurers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "PatientInsurers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "InsuranceProviders");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "DayOfTheWeek",
                table: "AppOrganizationUnitTimes");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .Annotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled")
                .Annotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .Annotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .Annotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .Annotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .OldAnnotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .OldAnnotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled")
                .OldAnnotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .OldAnnotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertairy")
                .OldAnnotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .OldAnnotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .OldAnnotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .OldAnnotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .Annotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled")
                .Annotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .Annotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertairy")
                .Annotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .Annotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .Annotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .Annotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .OldAnnotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .OldAnnotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled")
                .OldAnnotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .OldAnnotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .OldAnnotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .OldAnnotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "TenantDocuments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BloodGenotype",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BloodGroup",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaritalStatus",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Religion",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Relationship",
                table: "PatientRelations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BenefiaryType",
                table: "PatientInsurers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "PatientInsurers",
                type: "insurance_provider_type",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "InsuranceProviders",
                type: "insurance_provider_type",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Facilities",
                type: "facility_level",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayOfTheWeek",
                table: "AppOrganizationUnitTimes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

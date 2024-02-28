using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Insurance;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.MultiTenancy;
using Plateaumed.EHR.Patients;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedEnumsWithJsonFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .Annotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled")
                .Annotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .Annotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .Annotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .Annotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .Annotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertairy")
                .Annotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .Annotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .Annotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .Annotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .Annotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .Annotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .Annotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .Annotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .Annotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .OldAnnotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .OldAnnotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled")
                .OldAnnotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .OldAnnotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .OldAnnotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .OldAnnotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host");

            migrationBuilder.AddColumn<TenantDocumentType>(
                name: "Type",
                table: "TenantDocuments",
                type: "tenant_document_type",
                nullable: false,
                defaultValue: TenantDocumentType.MedicalDegree);

            migrationBuilder.AddColumn<BloodGenotype>(
                name: "BloodGenotype",
                table: "Patients",
                type: "blood_genotype",
                nullable: false,
                defaultValue: BloodGenotype.AA);

            migrationBuilder.AddColumn<BloodGroup>(
                name: "BloodGroup",
                table: "Patients",
                type: "blood_group",
                nullable: false,
                defaultValue: BloodGroup.A_Positive);

            migrationBuilder.AddColumn<MaritalStatus>(
                name: "MaritalStatus",
                table: "Patients",
                type: "marital_status",
                nullable: false,
                defaultValue: MaritalStatus.Single);

            migrationBuilder.AddColumn<Religion>(
                name: "Religion",
                table: "Patients",
                type: "religion",
                nullable: false,
                defaultValue: Religion.Christianity);

            migrationBuilder.AddColumn<Relationship>(
                name: "Relationship",
                table: "PatientRelations",
                type: "relationship",
                nullable: false,
                defaultValue: Relationship.Husband);

            migrationBuilder.AddColumn<InsuranceBenefiaryType>(
                name: "BenefiaryType",
                table: "PatientInsurers",
                type: "insurance_benefiary_type",
                nullable: false,
                defaultValue: InsuranceBenefiaryType.Primary);

            migrationBuilder.AddColumn<InsuranceProviderType>(
                name: "Type",
                table: "PatientInsurers",
                type: "insurance_provider_type",
                nullable: false,
                defaultValue: InsuranceProviderType.National);

            migrationBuilder.AddColumn<InsuranceProviderType>(
                name: "Type",
                table: "InsuranceProviders",
                type: "insurance_provider_type",
                nullable: false,
                defaultValue: InsuranceProviderType.National);

            migrationBuilder.AddColumn<FacilityLevel>(
                name: "Level",
                table: "Facilities",
                type: "facility_level",
                nullable: true);

            migrationBuilder.AddColumn<DaysOfTheWeek>(
                name: "DayOfTheWeek",
                table: "AppOrganizationUnitTimes",
                type: "days_of_the_week",
                nullable: false,
                defaultValue: DaysOfTheWeek.Sunday);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                .OldAnnotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .OldAnnotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .OldAnnotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .OldAnnotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertairy")
                .OldAnnotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .OldAnnotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .OldAnnotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .OldAnnotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .OldAnnotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .OldAnnotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .OldAnnotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .OldAnnotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .OldAnnotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host");
        }
    }
}

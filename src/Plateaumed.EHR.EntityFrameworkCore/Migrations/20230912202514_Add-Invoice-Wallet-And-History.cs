using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Misc;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceWalletAndHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .Annotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
                .Annotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .Annotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .Annotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .Annotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .Annotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertairy")
                .Annotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .Annotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .Annotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .Annotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .Annotation("Npgsql:Enum:invoice_source", "AccidentAndEmergency,OutPatient,InPatient,Pharmacy,Lab,Others")
                .Annotation("Npgsql:Enum:invoice_type", "InvoiceCreation,Proforma")
                .Annotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .Annotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .Annotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .Annotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .Annotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .Annotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .Annotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .Annotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .Annotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .Annotation("Npgsql:Enum:transaction_type", "Credit,Debit")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .OldAnnotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .OldAnnotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
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
                .OldAnnotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .OldAnnotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .OldAnnotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .OldAnnotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .OldAnnotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .OldAnnotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .OldAnnotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .OldAnnotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceId",
                table: "Invoices",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12);

            migrationBuilder.AddColumn<InvoiceSource>(
                name: "InvoiceSource",
                table: "Invoices",
                type: "invoice_source",
                nullable: false,
                defaultValue: InvoiceSource.AccidentAndEmergency);

            migrationBuilder.AddColumn<InvoiceType>(
                name: "InvoiceType",
                table: "Invoices",
                type: "invoice_type",
                nullable: false,
                defaultValue: InvoiceType.InvoiceCreation);

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    BalanceAmount = table.Column<decimal>(name: "Balance_Amount", type: "numeric", nullable: true),
                    BalanceCurrency = table.Column<string>(name: "Balance_Currency", type: "text", nullable: true),
                    BankName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
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
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AmountAmount = table.Column<decimal>(name: "Amount_Amount", type: "numeric", nullable: true),
                    AmountCurrency = table.Column<string>(name: "Amount_Currency", type: "text", nullable: true),
                    TransactionType = table.Column<TransactionType>(type: "transaction_type", nullable: false),
                    CurrentBalanceAmount = table.Column<decimal>(name: "CurrentBalance_Amount", type: "numeric", nullable: true),
                    CurrentBalanceCurrency = table.Column<string>(name: "CurrentBalance_Currency", type: "text", nullable: true),
                    Narration = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    WalletId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_WalletHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletHistories_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistories_WalletId",
                table: "WalletHistories",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_PatientId",
                table: "Wallets",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletHistories");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropColumn(
                name: "InvoiceSource",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "Invoices");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .Annotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
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
                .Annotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .Annotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .Annotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .Annotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .Annotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .Annotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .Annotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .Annotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .OldAnnotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .OldAnnotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
                .OldAnnotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .OldAnnotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .OldAnnotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .OldAnnotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .OldAnnotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertairy")
                .OldAnnotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .OldAnnotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .OldAnnotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .OldAnnotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .OldAnnotation("Npgsql:Enum:invoice_source", "AccidentAndEmergency,OutPatient,InPatient,Pharmacy,Lab,Others")
                .OldAnnotation("Npgsql:Enum:invoice_type", "InvoiceCreation,Proforma")
                .OldAnnotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .OldAnnotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .OldAnnotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .OldAnnotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .OldAnnotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .OldAnnotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .OldAnnotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .OldAnnotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .OldAnnotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .OldAnnotation("Npgsql:Enum:transaction_type", "Credit,Debit")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceId",
                table: "Invoices",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12,
                oldNullable: true);
        }
    }
}

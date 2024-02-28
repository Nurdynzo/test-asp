using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Invoices;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceRefundRequest : Migration
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
                .Annotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied")
                .Annotation("Npgsql:Enum:invoice_refund_status", "Pending,Approved,Rejected")
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
                .Annotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding")
                .Annotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .Annotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .Annotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected")
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
                .OldAnnotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied")
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
                .OldAnnotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding")
                .OldAnnotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .OldAnnotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .OldAnnotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected");

            migrationBuilder.CreateTable(
                name: "InvoiceRefundRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<long>(type: "bigint", nullable: true),
                    FacilityId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceItemId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<InvoiceRefundStatus>(type: "invoice_refund_status", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_InvoiceRefundRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceRefundRequests_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceRefundRequests_InvoiceItems_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "InvoiceItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceRefundRequests_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceRefundRequests_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRefundRequests_FacilityId",
                table: "InvoiceRefundRequests",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRefundRequests_InvoiceId",
                table: "InvoiceRefundRequests",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRefundRequests_InvoiceItemId",
                table: "InvoiceRefundRequests",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRefundRequests_PatientId",
                table: "InvoiceRefundRequests",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceRefundRequests");

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
                .Annotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied")
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
                .Annotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding")
                .Annotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .Annotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .Annotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected")
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
                .OldAnnotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied")
                .OldAnnotation("Npgsql:Enum:invoice_refund_status", "Pending,Approved,Rejected")
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
                .OldAnnotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding")
                .OldAnnotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .OldAnnotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .OldAnnotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected");
        }
    }
}

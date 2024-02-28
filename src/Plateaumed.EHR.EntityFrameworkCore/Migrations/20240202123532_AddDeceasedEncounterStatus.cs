﻿using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddDeceasedEncounterStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PatientEncounters");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .Annotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
                .Annotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .Annotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .Annotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .Annotation("Npgsql:Enum:blood_group_and_genotype_source", "ClinicalInvestigation,SelfReport")
                .Annotation("Npgsql:Enum:condition_control", "Well_Controlled,Poorly_Controlled")
                .Annotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .Annotation("Npgsql:Enum:encounter_status_type", "In_Progress,Transfer_In_Pending,Transfer_Out_Pending,Transferred,Discharge_Pending,Discharged,Admission_Pending,Completed,Deceased")
                .Annotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertiary")
                .Annotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .Annotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .Annotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .Annotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .Annotation("Npgsql:Enum:intensity", "Low,Moderate,High")
                .Annotation("Npgsql:Enum:investigation_status", "AllInvestigations,Requested,Invoiced,Processing,ResultReady,ImageReady,AwaitingReview,ReportReady")
                .Annotation("Npgsql:Enum:invoice_cancel_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied,Refunded,AwaitCancellationApproval")
                .Annotation("Npgsql:Enum:invoice_refund_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:invoice_source", "AccidentAndEmergency,OutPatient,InPatient,Pharmacy,Lab,Others")
                .Annotation("Npgsql:Enum:invoice_type", "InvoiceCreation,Proforma")
                .Annotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .Annotation("Npgsql:Enum:menstrual_flow_type", "Regular,Heavy")
                .Annotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .Annotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .Annotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .Annotation("Npgsql:Enum:price_time_frequency", "Day,Week,Visit")
                .Annotation("Npgsql:Enum:pricing_category", "Consultation,Ward_Admission,Procedure,Laboratory,Others")
                .Annotation("Npgsql:Enum:pricing_type", "General_Pricing")
                .Annotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .Annotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .Annotation("Npgsql:Enum:room_type", "OperatingRoom,ConsultingRoom")
                .Annotation("Npgsql:Enum:severity", "Mild,Moderate,Severe")
                .Annotation("Npgsql:Enum:symptoms_category", "Systemic,Cardiovascular,Respiratory,Gastrointestinal,Genitourinary,Musculoskeletal,Dermatological,Neurological")
                .Annotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .Annotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .Annotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .Annotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding,PaidInvoiceItem,DebtRelief")
                .Annotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .Annotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .Annotation("Npgsql:Enum:unit_of_time", "Day,Week,Month,Year")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .Annotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .OldAnnotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
                .OldAnnotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .OldAnnotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .OldAnnotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .OldAnnotation("Npgsql:Enum:blood_group_and_genotype_source", "ClinicalInvestigation,SelfReport")
                .OldAnnotation("Npgsql:Enum:condition_control", "Well_Controlled,Poorly_Controlled")
                .OldAnnotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .OldAnnotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertiary")
                .OldAnnotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .OldAnnotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .OldAnnotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .OldAnnotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .OldAnnotation("Npgsql:Enum:intensity", "Low,Moderate,High")
                .OldAnnotation("Npgsql:Enum:investigation_status", "AllInvestigations,Requested,Invoiced,Processing,ResultReady,ImageReady,AwaitingReview,ReportReady")
                .OldAnnotation("Npgsql:Enum:invoice_cancel_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied,Refunded,AwaitCancellationApproval")
                .OldAnnotation("Npgsql:Enum:invoice_refund_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:invoice_source", "AccidentAndEmergency,OutPatient,InPatient,Pharmacy,Lab,Others")
                .OldAnnotation("Npgsql:Enum:invoice_type", "InvoiceCreation,Proforma")
                .OldAnnotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .OldAnnotation("Npgsql:Enum:menstrual_flow_type", "Regular,Heavy")
                .OldAnnotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .OldAnnotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .OldAnnotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .OldAnnotation("Npgsql:Enum:price_time_frequency", "Day,Week,Visit")
                .OldAnnotation("Npgsql:Enum:pricing_category", "Consultation,Ward_Admission,Procedure,Laboratory,Others")
                .OldAnnotation("Npgsql:Enum:pricing_type", "General_Pricing")
                .OldAnnotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .OldAnnotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .OldAnnotation("Npgsql:Enum:room_type", "OperatingRoom,ConsultingRoom")
                .OldAnnotation("Npgsql:Enum:severity", "Mild,Moderate,Severe")
                .OldAnnotation("Npgsql:Enum:symptoms_category", "Systemic,Cardiovascular,Respiratory,Gastrointestinal,Genitourinary,Musculoskeletal,Dermatological,Neurological")
                .OldAnnotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .OldAnnotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .OldAnnotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .OldAnnotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding,PaidInvoiceItem,DebtRelief")
                .OldAnnotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .OldAnnotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .OldAnnotation("Npgsql:Enum:unit_of_time", "Day,Week,Month,Year")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .OldAnnotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected");

            migrationBuilder.AddColumn<EncounterStatusType>(
                name: "Status",
                table: "PatientEncounters",
                type: "encounter_status_type",
                nullable: false,
                defaultValue: EncounterStatusType.InProgress);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PatientEncounters");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .Annotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
                .Annotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .Annotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .Annotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .Annotation("Npgsql:Enum:blood_group_and_genotype_source", "ClinicalInvestigation,SelfReport")
                .Annotation("Npgsql:Enum:condition_control", "Well_Controlled,Poorly_Controlled")
                .Annotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .Annotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertiary")
                .Annotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .Annotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .Annotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .Annotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .Annotation("Npgsql:Enum:intensity", "Low,Moderate,High")
                .Annotation("Npgsql:Enum:investigation_status", "AllInvestigations,Requested,Invoiced,Processing,ResultReady,ImageReady,AwaitingReview,ReportReady")
                .Annotation("Npgsql:Enum:invoice_cancel_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied,Refunded,AwaitCancellationApproval")
                .Annotation("Npgsql:Enum:invoice_refund_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:invoice_source", "AccidentAndEmergency,OutPatient,InPatient,Pharmacy,Lab,Others")
                .Annotation("Npgsql:Enum:invoice_type", "InvoiceCreation,Proforma")
                .Annotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .Annotation("Npgsql:Enum:menstrual_flow_type", "Regular,Heavy")
                .Annotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .Annotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .Annotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .Annotation("Npgsql:Enum:price_time_frequency", "Day,Week,Visit")
                .Annotation("Npgsql:Enum:pricing_category", "Consultation,Ward_Admission,Procedure,Laboratory,Others")
                .Annotation("Npgsql:Enum:pricing_type", "General_Pricing")
                .Annotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .Annotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .Annotation("Npgsql:Enum:room_type", "OperatingRoom,ConsultingRoom")
                .Annotation("Npgsql:Enum:severity", "Mild,Moderate,Severe")
                .Annotation("Npgsql:Enum:symptoms_category", "Systemic,Cardiovascular,Respiratory,Gastrointestinal,Genitourinary,Musculoskeletal,Dermatological,Neurological")
                .Annotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .Annotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .Annotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .Annotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding,PaidInvoiceItem,DebtRelief")
                .Annotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .Annotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .Annotation("Npgsql:Enum:unit_of_time", "Day,Week,Month,Year")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .Annotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .OldAnnotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
                .OldAnnotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .OldAnnotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .OldAnnotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .OldAnnotation("Npgsql:Enum:blood_group_and_genotype_source", "ClinicalInvestigation,SelfReport")
                .OldAnnotation("Npgsql:Enum:condition_control", "Well_Controlled,Poorly_Controlled")
                .OldAnnotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .OldAnnotation("Npgsql:Enum:encounter_status_type", "In_Progress,Transfer_In_Pending,Transfer_Out_Pending,Transferred,Discharge_Pending,Discharged,Admission_Pending,Completed,Deceased")
                .OldAnnotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertiary")
                .OldAnnotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .OldAnnotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .OldAnnotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .OldAnnotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .OldAnnotation("Npgsql:Enum:intensity", "Low,Moderate,High")
                .OldAnnotation("Npgsql:Enum:investigation_status", "AllInvestigations,Requested,Invoiced,Processing,ResultReady,ImageReady,AwaitingReview,ReportReady")
                .OldAnnotation("Npgsql:Enum:invoice_cancel_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied,Refunded,AwaitCancellationApproval")
                .OldAnnotation("Npgsql:Enum:invoice_refund_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:invoice_source", "AccidentAndEmergency,OutPatient,InPatient,Pharmacy,Lab,Others")
                .OldAnnotation("Npgsql:Enum:invoice_type", "InvoiceCreation,Proforma")
                .OldAnnotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .OldAnnotation("Npgsql:Enum:menstrual_flow_type", "Regular,Heavy")
                .OldAnnotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .OldAnnotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .OldAnnotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .OldAnnotation("Npgsql:Enum:price_time_frequency", "Day,Week,Visit")
                .OldAnnotation("Npgsql:Enum:pricing_category", "Consultation,Ward_Admission,Procedure,Laboratory,Others")
                .OldAnnotation("Npgsql:Enum:pricing_type", "General_Pricing")
                .OldAnnotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .OldAnnotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .OldAnnotation("Npgsql:Enum:room_type", "OperatingRoom,ConsultingRoom")
                .OldAnnotation("Npgsql:Enum:severity", "Mild,Moderate,Severe")
                .OldAnnotation("Npgsql:Enum:symptoms_category", "Systemic,Cardiovascular,Respiratory,Gastrointestinal,Genitourinary,Musculoskeletal,Dermatological,Neurological")
                .OldAnnotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .OldAnnotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .OldAnnotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .OldAnnotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding,PaidInvoiceItem,DebtRelief")
                .OldAnnotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .OldAnnotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .OldAnnotation("Npgsql:Enum:unit_of_time", "Day,Week,Month,Year")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .OldAnnotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PatientEncounters",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

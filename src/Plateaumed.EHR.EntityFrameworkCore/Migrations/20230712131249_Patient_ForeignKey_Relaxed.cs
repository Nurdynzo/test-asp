using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class PatientForeignKeyRelaxed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientInsurers_Patients_PatientId",
                table: "PatientInsurers");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReferralDocuments_Patients_PatientId",
                table: "PatientReferralDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientRelations_Patients_PatientId",
                table: "PatientRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PatientOccupationCategories_PatientOccupationCateg~",
                table: "Patients");

            migrationBuilder.AddColumn<long>(
                name: "PatientOccupationCategoryFkId",
                table: "Patients",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientOccupationCategoryFkId",
                table: "Patients",
                column: "PatientOccupationCategoryFkId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientInsurers_Patients_PatientId",
                table: "PatientInsurers",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReferralDocuments_Patients_PatientId",
                table: "PatientReferralDocuments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRelations_Patients_PatientId",
                table: "PatientRelations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PatientOccupationCategories_PatientOccupationCateg~",
                table: "Patients",
                column: "PatientOccupationCategoryFkId",
                principalTable: "PatientOccupationCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PatientOccupationCategories_PatientOccupationCate~1",
                table: "Patients",
                column: "PatientOccupationCategoryId",
                principalTable: "PatientOccupationCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PatientOccupations_PatientOccupationCategoryId",
                table: "Patients",
                column: "PatientOccupationCategoryId",
                principalTable: "PatientOccupations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppEditions_AppTenants_TenantId",
                table: "AppEditions");

            migrationBuilder.DropForeignKey(
                name: "FK_AppEditions_Countries_CountryId",
                table: "AppEditions");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientInsurers_Patients_PatientId",
                table: "PatientInsurers");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReferralDocuments_Patients_PatientId",
                table: "PatientReferralDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientRelations_Patients_PatientId",
                table: "PatientRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PatientOccupationCategories_PatientOccupationCateg~",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PatientOccupationCategories_PatientOccupationCate~1",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PatientOccupations_PatientOccupationCategoryId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PatientOccupationCategoryFkId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PatientOccupationCategoryId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_AppEditions_CountryId",
                table: "AppEditions");

            migrationBuilder.DropIndex(
                name: "IX_AppEditions_TenantId",
                table: "AppEditions");

            migrationBuilder.DropColumn(
                name: "PatientOccupationCategoryFkId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AppEditions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppEditions");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientOccupationCategoryId",
                table: "Patients",
                column: "PatientOccupationCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientInsurers_Patients_PatientId",
                table: "PatientInsurers",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReferralDocuments_Patients_PatientId",
                table: "PatientReferralDocuments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRelations_Patients_PatientId",
                table: "PatientRelations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PatientOccupationCategories_PatientOccupationCateg~",
                table: "Patients",
                column: "PatientOccupationCategoryId",
                principalTable: "PatientOccupationCategories",
                principalColumn: "Id");
        }
    }
}

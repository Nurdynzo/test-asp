using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCreatedByUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientReviewDetailedHistories_AppUsers_CreatedByUserId",
                table: "PatientReviewDetailedHistories");

            migrationBuilder.DropIndex(
                name: "IX_PatientReviewDetailedHistories_CreatedByUserId",
                table: "PatientReviewDetailedHistories");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "PatientReviewDetailedHistories");

            migrationBuilder.CreateIndex(
                name: "IX_PatientReviewDetailedHistories_CreatorUserId",
                table: "PatientReviewDetailedHistories",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReviewDetailedHistories_AppUsers_CreatorUserId",
                table: "PatientReviewDetailedHistories",
                column: "CreatorUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientReviewDetailedHistories_AppUsers_CreatorUserId",
                table: "PatientReviewDetailedHistories");

            migrationBuilder.DropIndex(
                name: "IX_PatientReviewDetailedHistories_CreatorUserId",
                table: "PatientReviewDetailedHistories");

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "PatientReviewDetailedHistories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientReviewDetailedHistories_CreatedByUserId",
                table: "PatientReviewDetailedHistories",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReviewDetailedHistories_AppUsers_CreatedByUserId",
                table: "PatientReviewDetailedHistories",
                column: "CreatedByUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }
    }
}

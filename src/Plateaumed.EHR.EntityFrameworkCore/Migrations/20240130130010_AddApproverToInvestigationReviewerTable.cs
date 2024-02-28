using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddApproverToInvestigationReviewerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ApproverId",
                table: "InvestigationResultReviewer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReviewerId",
                table: "InvestigationResultReviewer",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "InvestigationResultReviewer");
        }
    }
}

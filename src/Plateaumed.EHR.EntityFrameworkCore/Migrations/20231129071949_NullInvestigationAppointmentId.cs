using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class NullInvestigationAppointmentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationRequests_PatientAppointments_AppointmentId",
                table: "InvestigationRequests");

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentId",
                table: "InvestigationRequests",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationResults_InvestigationId",
                table: "InvestigationResults",
                column: "InvestigationId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationResults_InvestigationRequestId",
                table: "InvestigationResults",
                column: "InvestigationRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationRequests_PatientAppointments_AppointmentId",
                table: "InvestigationRequests",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationResults_InvestigationRequests_InvestigationReq~",
                table: "InvestigationResults",
                column: "InvestigationRequestId",
                principalTable: "InvestigationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationResults_Investigations_InvestigationId",
                table: "InvestigationResults",
                column: "InvestigationId",
                principalTable: "Investigations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationRequests_PatientAppointments_AppointmentId",
                table: "InvestigationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationResults_InvestigationRequests_InvestigationReq~",
                table: "InvestigationResults");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationResults_Investigations_InvestigationId",
                table: "InvestigationResults");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationResults_InvestigationId",
                table: "InvestigationResults");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationResults_InvestigationRequestId",
                table: "InvestigationResults");

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentId",
                table: "InvestigationRequests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationRequests_PatientAppointments_AppointmentId",
                table: "InvestigationRequests",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAppointmentsFromAllInputs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BedMaking_PatientAppointments_AppointmentId",
                table: "BedMaking");

            migrationBuilder.DropForeignKey(
                name: "FK_Feeding_PatientAppointments_AppointmentId",
                table: "Feeding");

            migrationBuilder.DropForeignKey(
                name: "FK_InputNotes_PatientAppointments_AppointmentId",
                table: "InputNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationRequests_PatientAppointments_AppointmentId",
                table: "InvestigationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_PatientAppointments_AppointmentId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanItems_PatientAppointments_AppointmentId",
                table: "PlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_PatientAppointments_AppointmentId",
                table: "Symptoms");

            migrationBuilder.DropForeignKey(
                name: "FK_WoundDressing_PatientAppointments_AppointmentId",
                table: "WoundDressing");

            migrationBuilder.DropIndex(
                name: "IX_WoundDressing_AppointmentId",
                table: "WoundDressing");

            migrationBuilder.DropIndex(
                name: "IX_Symptoms_AppointmentId",
                table: "Symptoms");

            migrationBuilder.DropIndex(
                name: "IX_PlanItems_AppointmentId",
                table: "PlanItems");

            migrationBuilder.DropIndex(
                name: "IX_Meals_AppointmentId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationRequests_AppointmentId",
                table: "InvestigationRequests");

            migrationBuilder.DropIndex(
                name: "IX_InputNotes_AppointmentId",
                table: "InputNotes");

            migrationBuilder.DropIndex(
                name: "IX_Feeding_AppointmentId",
                table: "Feeding");

            migrationBuilder.DropIndex(
                name: "IX_BedMaking_AppointmentId",
                table: "BedMaking");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "WoundDressing");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "PlanItems");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "InvestigationRequests");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "InputNotes");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Feeding");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "BedMaking");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "WoundDressing",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "Symptoms",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "PlanItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "Meals",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "InvestigationRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "InputNotes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "Feeding",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "BedMaking",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WoundDressing_AppointmentId",
                table: "WoundDressing",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_AppointmentId",
                table: "Symptoms",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanItems_AppointmentId",
                table: "PlanItems",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_AppointmentId",
                table: "Meals",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationRequests_AppointmentId",
                table: "InvestigationRequests",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InputNotes_AppointmentId",
                table: "InputNotes",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeding_AppointmentId",
                table: "Feeding",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BedMaking_AppointmentId",
                table: "BedMaking",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BedMaking_PatientAppointments_AppointmentId",
                table: "BedMaking",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feeding_PatientAppointments_AppointmentId",
                table: "Feeding",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InputNotes_PatientAppointments_AppointmentId",
                table: "InputNotes",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationRequests_PatientAppointments_AppointmentId",
                table: "InvestigationRequests",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_PatientAppointments_AppointmentId",
                table: "Meals",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanItems_PatientAppointments_AppointmentId",
                table: "PlanItems",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_PatientAppointments_AppointmentId",
                table: "Symptoms",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WoundDressing_PatientAppointments_AppointmentId",
                table: "WoundDressing",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id");
        }
    }
}

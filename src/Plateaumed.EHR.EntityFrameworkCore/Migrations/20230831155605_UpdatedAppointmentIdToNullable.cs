using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAppointmentIdToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_PatientAppointments_AppointmentId",
                table: "Symptoms");

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentId",
                table: "Symptoms",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_PatientAppointments_AppointmentId",
                table: "Symptoms",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_PatientAppointments_AppointmentId",
                table: "Symptoms");

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentId",
                table: "Symptoms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_PatientAppointments_AppointmentId",
                table: "Symptoms",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

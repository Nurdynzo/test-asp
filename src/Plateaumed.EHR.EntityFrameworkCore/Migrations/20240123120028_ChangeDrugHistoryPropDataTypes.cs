using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Misc;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDrugHistoryPropDataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsageInterval",
                table: "DrugHistories",
                type: "text",
                nullable: true,
                oldClrType: typeof(UnitOfTime),
                oldType: "unit_of_time");

            migrationBuilder.AlterColumn<string>(
                name: "StopInterval",
                table: "DrugHistories",
                type: "text",
                nullable: true,
                oldClrType: typeof(UnitOfTime),
                oldType: "unit_of_time");

            migrationBuilder.AlterColumn<string>(
                name: "PrescriptionInterval",
                table: "DrugHistories",
                type: "text",
                nullable: true,
                oldClrType: typeof(UnitOfTime),
                oldType: "unit_of_time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<UnitOfTime>(
                name: "UsageInterval",
                table: "DrugHistories",
                type: "unit_of_time",
                nullable: false,
                defaultValue: UnitOfTime.Day,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<UnitOfTime>(
                name: "StopInterval",
                table: "DrugHistories",
                type: "unit_of_time",
                nullable: false,
                defaultValue: UnitOfTime.Day,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<UnitOfTime>(
                name: "PrescriptionInterval",
                table: "DrugHistories",
                type: "unit_of_time",
                nullable: false,
                defaultValue: UnitOfTime.Day,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}

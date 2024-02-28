using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedChildrenSiblingGenderToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoOfFemaleChildren",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfFemaleSiblings",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfMaleChildren",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfMaleSiblings",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfFemaleChildren",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NoOfFemaleSiblings",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NoOfMaleChildren",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NoOfMaleSiblings",
                table: "Patients");
        }
    }
}

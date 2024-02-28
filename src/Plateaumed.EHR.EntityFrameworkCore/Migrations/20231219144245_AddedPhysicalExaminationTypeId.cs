using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhysicalExaminationTypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.AddColumn<long>(
                name: "PhysicalExaminationTypeId",
                table: "PhysicalExaminations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PhysicalExaminationTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(200)", nullable: true),
                    Type = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalExaminationTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExaminations_PhysicalExaminationTypeId",
                table: "PhysicalExaminations",
                column: "PhysicalExaminationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExaminationTypes_Type",
                table: "PhysicalExaminationTypes",
                column: "Type",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExaminations_PhysicalExaminationTypes_PhysicalExami~",
                table: "PhysicalExaminations",
                column: "PhysicalExaminationTypeId",
                principalTable: "PhysicalExaminationTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExaminations_PhysicalExaminationTypes_PhysicalExami~",
                table: "PhysicalExaminations");

            migrationBuilder.DropTable(
                name: "PhysicalExaminationTypes");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalExaminations_PhysicalExaminationTypeId",
                table: "PhysicalExaminations");

            migrationBuilder.DropColumn(
                name: "PhysicalExaminationTypeId",
                table: "PhysicalExaminations"); 
        }
    }
}

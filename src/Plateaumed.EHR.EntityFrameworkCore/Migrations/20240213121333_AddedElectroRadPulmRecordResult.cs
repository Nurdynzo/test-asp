using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedElectroRadPulmRecordResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectroRadPulmInvestigationResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    InvestigationId = table.Column<long>(type: "bigint", nullable: false),
                    InvestigationRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ResultDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ResultTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Conclusion = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    EncounterId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectroRadPulmInvestigationResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectroRadPulmInvestigationResult_InvestigationRequests_Inv~",
                        column: x => x.InvestigationRequestId,
                        principalTable: "InvestigationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectroRadPulmInvestigationResult_Investigations_Investigat~",
                        column: x => x.InvestigationId,
                        principalTable: "Investigations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectroRadPulmInvestigationResult_PatientEncounters_Encount~",
                        column: x => x.EncounterId,
                        principalTable: "PatientEncounters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectroRadPulmInvestigationResult_EncounterId",
                table: "ElectroRadPulmInvestigationResult",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectroRadPulmInvestigationResult_InvestigationId",
                table: "ElectroRadPulmInvestigationResult",
                column: "InvestigationId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectroRadPulmInvestigationResult_InvestigationRequestId",
                table: "ElectroRadPulmInvestigationResult",
                column: "InvestigationRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectroRadPulmInvestigationResult");
        }
    }
}

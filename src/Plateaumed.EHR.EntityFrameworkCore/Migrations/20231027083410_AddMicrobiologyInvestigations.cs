using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddMicrobiologyInvestigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationSuggestion_Investigations_InvestigationId",
                table: "InvestigationSuggestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvestigationSuggestion",
                table: "InvestigationSuggestion");

            migrationBuilder.RenameTable(
                name: "InvestigationSuggestion",
                newName: "InvestigationSuggestions");

            migrationBuilder.RenameIndex(
                name: "IX_InvestigationSuggestion_InvestigationId",
                table: "InvestigationSuggestions",
                newName: "IX_InvestigationSuggestions_InvestigationId");

            migrationBuilder.AddColumn<string>(
                name: "SpecificOrganism",
                table: "Investigations",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "InvestigationId",
                table: "InvestigationSuggestions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "InvestigationSuggestions",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvestigationSuggestions",
                table: "InvestigationSuggestions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DipstickInvestigation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvestigationId = table.Column<long>(type: "bigint", nullable: true),
                    Parameter = table.Column<string>(type: "text", nullable: true),
                    InvestigationId2 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DipstickInvestigation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DipstickInvestigation_Investigations_InvestigationId",
                        column: x => x.InvestigationId,
                        principalTable: "Investigations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DipstickInvestigation_Investigations_InvestigationId2",
                        column: x => x.InvestigationId2,
                        principalTable: "Investigations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MicrobiologyInvestigation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvestigationId = table.Column<long>(type: "bigint", nullable: true),
                    MethyleneBlueStain = table.Column<bool>(type: "boolean", nullable: false),
                    AntibioticSensitivityTest = table.Column<bool>(type: "boolean", nullable: false),
                    NugentScore = table.Column<bool>(type: "boolean", nullable: false),
                    Culture = table.Column<bool>(type: "boolean", nullable: false),
                    GramStain = table.Column<bool>(type: "boolean", nullable: false),
                    Microscopy = table.Column<bool>(type: "boolean", nullable: false),
                    CommonResults = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicrobiologyInvestigation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MicrobiologyInvestigation_Investigations_InvestigationId",
                        column: x => x.InvestigationId,
                        principalTable: "Investigations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DipstickRange",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Unit = table.Column<string>(type: "text", nullable: true),
                    DipstickInvestigationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DipstickRange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DipstickRange_DipstickInvestigation_DipstickInvestigationId",
                        column: x => x.DipstickInvestigationId,
                        principalTable: "DipstickInvestigation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DipstickResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Result = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    DipstickRangeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DipstickResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DipstickResult_DipstickRange_DipstickRangeId",
                        column: x => x.DipstickRangeId,
                        principalTable: "DipstickRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DipstickInvestigation_InvestigationId",
                table: "DipstickInvestigation",
                column: "InvestigationId");

            migrationBuilder.CreateIndex(
                name: "IX_DipstickInvestigation_InvestigationId2",
                table: "DipstickInvestigation",
                column: "InvestigationId2");

            migrationBuilder.CreateIndex(
                name: "IX_DipstickRange_DipstickInvestigationId",
                table: "DipstickRange",
                column: "DipstickInvestigationId");

            migrationBuilder.CreateIndex(
                name: "IX_DipstickResult_DipstickRangeId",
                table: "DipstickResult",
                column: "DipstickRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_MicrobiologyInvestigation_InvestigationId",
                table: "MicrobiologyInvestigation",
                column: "InvestigationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationSuggestions_Investigations_InvestigationId",
                table: "InvestigationSuggestions",
                column: "InvestigationId",
                principalTable: "Investigations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationSuggestions_Investigations_InvestigationId",
                table: "InvestigationSuggestions");

            migrationBuilder.DropTable(
                name: "DipstickResult");

            migrationBuilder.DropTable(
                name: "MicrobiologyInvestigation");

            migrationBuilder.DropTable(
                name: "DipstickRange");

            migrationBuilder.DropTable(
                name: "DipstickInvestigation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvestigationSuggestions",
                table: "InvestigationSuggestions");

            migrationBuilder.DropColumn(
                name: "SpecificOrganism",
                table: "Investigations");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "InvestigationSuggestions");

            migrationBuilder.RenameTable(
                name: "InvestigationSuggestions",
                newName: "InvestigationSuggestion");

            migrationBuilder.RenameIndex(
                name: "IX_InvestigationSuggestions_InvestigationId",
                table: "InvestigationSuggestion",
                newName: "IX_InvestigationSuggestion_InvestigationId");

            migrationBuilder.AlterColumn<long>(
                name: "InvestigationId",
                table: "InvestigationSuggestion",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvestigationSuggestion",
                table: "InvestigationSuggestion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationSuggestion_Investigations_InvestigationId",
                table: "InvestigationSuggestion",
                column: "InvestigationId",
                principalTable: "Investigations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

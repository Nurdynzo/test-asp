using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Misc;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddInvestigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Investigations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ShortName = table.Column<string>(type: "text", nullable: true),
                    SnomedId = table.Column<string>(type: "text", nullable: true),
                    Synonyms = table.Column<string>(type: "text", nullable: true),
                    Specimen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investigations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvestigationComponent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    SnomedId = table.Column<string>(type: "text", nullable: true),
                    Synonyms = table.Column<string>(type: "text", nullable: true),
                    Specimen = table.Column<string>(type: "text", nullable: true),
                    InvestigationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestigationComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestigationComponent_Investigations_InvestigationId",
                        column: x => x.InvestigationId,
                        principalTable: "Investigations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvestigationBinaryResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Normal = table.Column<bool>(type: "boolean", nullable: false),
                    InvestigationComponentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestigationBinaryResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestigationBinaryResult_InvestigationComponent_Investigat~",
                        column: x => x.InvestigationComponentId,
                        principalTable: "InvestigationComponent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvestigationRange",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgeMin = table.Column<int>(type: "integer", nullable: true),
                    AgeMinUnit = table.Column<UnitOfTime>(type: "unit_of_time", nullable: true),
                    AgeMax = table.Column<int>(type: "integer", nullable: true),
                    AgeMaxUnit = table.Column<UnitOfTime>(type: "unit_of_time", nullable: true),
                    Gender = table.Column<GenderType>(type: "gender_type", nullable: true),
                    Unit = table.Column<string>(type: "text", nullable: true),
                    MinRange = table.Column<decimal>(type: "numeric", nullable: true),
                    MaxRange = table.Column<decimal>(type: "numeric", nullable: true),
                    InvestigationComponentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestigationRange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestigationRange_InvestigationComponent_InvestigationComp~",
                        column: x => x.InvestigationComponentId,
                        principalTable: "InvestigationComponent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvestigationSuggestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Result = table.Column<string>(type: "text", nullable: true),
                    SnomedId = table.Column<string>(type: "text", nullable: true),
                    Normal = table.Column<bool>(type: "boolean", nullable: false),
                    InvestigationComponentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestigationSuggestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestigationSuggestion_InvestigationComponent_Investigatio~",
                        column: x => x.InvestigationComponentId,
                        principalTable: "InvestigationComponent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationBinaryResult_InvestigationComponentId",
                table: "InvestigationBinaryResult",
                column: "InvestigationComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationComponent_InvestigationId",
                table: "InvestigationComponent",
                column: "InvestigationId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationRange_InvestigationComponentId",
                table: "InvestigationRange",
                column: "InvestigationComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationSuggestion_InvestigationComponentId",
                table: "InvestigationSuggestion",
                column: "InvestigationComponentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestigationBinaryResult");

            migrationBuilder.DropTable(
                name: "InvestigationRange");

            migrationBuilder.DropTable(
                name: "InvestigationSuggestion");

            migrationBuilder.DropTable(
                name: "InvestigationComponent");

            migrationBuilder.DropTable(
                name: "Investigations");
        }
    }
}

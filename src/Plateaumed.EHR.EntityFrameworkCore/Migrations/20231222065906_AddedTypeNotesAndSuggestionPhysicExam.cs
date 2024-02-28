using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedTypeNotesAndSuggestionPhysicExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonData",
                table: "PatientPhysicalExaminations");

            migrationBuilder.CreateTable(
                name: "PatientPhysicalExamSuggestionAnswer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    SymptomSnowmedId = table.Column<string>(type: "varchar(300)", nullable: true),
                    Description = table.Column<string>(type: "varchar(500)", nullable: true),
                    IsAbsent = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPhysicalExamSuggestionAnswer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientPhysicalExamTypeNote",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "varchar(300)", nullable: true),
                    Note = table.Column<string>(type: "varchar(5000)", nullable: true),
                    PatientPhysicalExaminationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPhysicalExamTypeNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExamTypeNote_PatientPhysicalExaminations_Pat~",
                        column: x => x.PatientPhysicalExaminationId,
                        principalTable: "PatientPhysicalExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientPhysicalExamSuggestionQualifier",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    QualifierId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "varchar(500)", nullable: true),
                    PatientPhysicalExamSuggestionAnswerId = table.Column<long>(type: "bigint", nullable: true),
                    PatientPhysicalExamSuggestionAnswerId1 = table.Column<long>(type: "bigint", nullable: true),
                    PatientPhysicalExamSuggestionAnswerId2 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPhysicalExamSuggestionQualifier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExamSuggestionQualifier_PatientPhysicalExamS~",
                        column: x => x.PatientPhysicalExamSuggestionAnswerId,
                        principalTable: "PatientPhysicalExamSuggestionAnswer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExamSuggestionQualifier_PatientPhysicalExam~1",
                        column: x => x.PatientPhysicalExamSuggestionAnswerId1,
                        principalTable: "PatientPhysicalExamSuggestionAnswer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExamSuggestionQualifier_PatientPhysicalExam~2",
                        column: x => x.PatientPhysicalExamSuggestionAnswerId2,
                        principalTable: "PatientPhysicalExamSuggestionAnswer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientPhysicalExamSuggestionQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    HeaderName = table.Column<string>(type: "varchar(300)", nullable: true),
                    PatientPhysicalExamSuggestionAnswerId = table.Column<long>(type: "bigint", nullable: true),
                    PatientPhysicalExaminationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPhysicalExamSuggestionQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExamSuggestionQuestion_PatientPhysicalExamSu~",
                        column: x => x.PatientPhysicalExamSuggestionAnswerId,
                        principalTable: "PatientPhysicalExamSuggestionAnswer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExamSuggestionQuestion_PatientPhysicalExamin~",
                        column: x => x.PatientPhysicalExaminationId,
                        principalTable: "PatientPhysicalExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExamSuggestionQualifier_PatientPhysicalExam~1",
                table: "PatientPhysicalExamSuggestionQualifier",
                column: "PatientPhysicalExamSuggestionAnswerId1");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExamSuggestionQualifier_PatientPhysicalExam~2",
                table: "PatientPhysicalExamSuggestionQualifier",
                column: "PatientPhysicalExamSuggestionAnswerId2");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExamSuggestionQualifier_PatientPhysicalExamS~",
                table: "PatientPhysicalExamSuggestionQualifier",
                column: "PatientPhysicalExamSuggestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExamSuggestionQuestion_PatientPhysicalExamin~",
                table: "PatientPhysicalExamSuggestionQuestion",
                column: "PatientPhysicalExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExamSuggestionQuestion_PatientPhysicalExamSu~",
                table: "PatientPhysicalExamSuggestionQuestion",
                column: "PatientPhysicalExamSuggestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExamTypeNote_PatientPhysicalExaminationId",
                table: "PatientPhysicalExamTypeNote",
                column: "PatientPhysicalExaminationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientPhysicalExamSuggestionQualifier");

            migrationBuilder.DropTable(
                name: "PatientPhysicalExamSuggestionQuestion");

            migrationBuilder.DropTable(
                name: "PatientPhysicalExamTypeNote");

            migrationBuilder.DropTable(
                name: "PatientPhysicalExamSuggestionAnswer");

            migrationBuilder.AddColumn<string>(
                name: "JsonData",
                table: "PatientPhysicalExaminations",
                type: "text",
                nullable: true);
        }
    }
}

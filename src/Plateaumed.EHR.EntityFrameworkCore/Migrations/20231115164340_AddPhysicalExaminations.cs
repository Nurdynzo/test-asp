using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Authorization.Users;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddPhysicalExaminations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExaminationQualifiers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubQualifier = table.Column<string>(type: "text", nullable: true),
                    SubDivision = table.Column<string>(type: "text", nullable: true),
                    Qualifier = table.Column<string>(type: "text", nullable: true),
                    SnomedId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationQualifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalExaminations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Header = table.Column<string>(type: "text", nullable: true),
                    PresentTerms = table.Column<string>(type: "text", nullable: true),
                    SnomedId = table.Column<string>(type: "text", nullable: true),
                    AbsentTerms = table.Column<string>(type: "text", nullable: true),
                    AbsentTermsSnomedId = table.Column<string>(type: "text", nullable: true),
                    Site = table.Column<bool>(type: "boolean", nullable: false),
                    Plane = table.Column<bool>(type: "boolean", nullable: false),
                    Qualifiers = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<GenderType>(type: "gender_type", nullable: true),
                    Unit = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalExaminations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationQualifiers");

            migrationBuilder.DropTable(
                name: "PhysicalExaminations");
        }
    }
}

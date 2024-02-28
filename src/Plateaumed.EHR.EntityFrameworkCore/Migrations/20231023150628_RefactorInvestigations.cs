using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RefactorInvestigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM public.\"InvestigationRange\"");
            migrationBuilder.Sql("DELETE FROM public.\"InvestigationSuggestion\"");
            migrationBuilder.Sql("DELETE FROM public.\"InvestigationBinaryResult\"");
            migrationBuilder.Sql("DELETE FROM public.\"InvestigationComponent\"");
            migrationBuilder.Sql("DELETE FROM public.\"Investigations\"");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationBinaryResult_InvestigationComponent_Investigat~",
                table: "InvestigationBinaryResult");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationRange_InvestigationComponent_InvestigationComp~",
                table: "InvestigationRange");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationSuggestion_InvestigationComponent_Investigatio~",
                table: "InvestigationSuggestion");

            migrationBuilder.DropTable(
                name: "InvestigationComponent");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationSuggestion_InvestigationComponentId",
                table: "InvestigationSuggestion");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationRange_InvestigationComponentId",
                table: "InvestigationRange");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationBinaryResult_InvestigationComponentId",
                table: "InvestigationBinaryResult");

            migrationBuilder.DropColumn(
                name: "InvestigationComponentId",
                table: "InvestigationSuggestion");

            migrationBuilder.DropColumn(
                name: "InvestigationComponentId",
                table: "InvestigationRange");

            migrationBuilder.DropColumn(
                name: "InvestigationComponentId",
                table: "InvestigationBinaryResult");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "InvestigationBinaryResult",
                newName: "Result");

            migrationBuilder.AddColumn<long>(
                name: "InvestigationId",
                table: "InvestigationSuggestion",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PartOfId",
                table: "Investigations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InvestigationId",
                table: "InvestigationRange",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "InvestigationId",
                table: "InvestigationBinaryResult",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationSuggestion_InvestigationId",
                table: "InvestigationSuggestion",
                column: "InvestigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Investigations_PartOfId",
                table: "Investigations",
                column: "PartOfId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationRange_InvestigationId",
                table: "InvestigationRange",
                column: "InvestigationId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationBinaryResult_InvestigationId",
                table: "InvestigationBinaryResult",
                column: "InvestigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationBinaryResult_Investigations_InvestigationId",
                table: "InvestigationBinaryResult",
                column: "InvestigationId",
                principalTable: "Investigations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationRange_Investigations_InvestigationId",
                table: "InvestigationRange",
                column: "InvestigationId",
                principalTable: "Investigations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Investigations_Investigations_PartOfId",
                table: "Investigations",
                column: "PartOfId",
                principalTable: "Investigations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationSuggestion_Investigations_InvestigationId",
                table: "InvestigationSuggestion",
                column: "InvestigationId",
                principalTable: "Investigations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationBinaryResult_Investigations_InvestigationId",
                table: "InvestigationBinaryResult");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationRange_Investigations_InvestigationId",
                table: "InvestigationRange");

            migrationBuilder.DropForeignKey(
                name: "FK_Investigations_Investigations_PartOfId",
                table: "Investigations");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationSuggestion_Investigations_InvestigationId",
                table: "InvestigationSuggestion");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationSuggestion_InvestigationId",
                table: "InvestigationSuggestion");

            migrationBuilder.DropIndex(
                name: "IX_Investigations_PartOfId",
                table: "Investigations");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationRange_InvestigationId",
                table: "InvestigationRange");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationBinaryResult_InvestigationId",
                table: "InvestigationBinaryResult");

            migrationBuilder.DropColumn(
                name: "InvestigationId",
                table: "InvestigationSuggestion");

            migrationBuilder.DropColumn(
                name: "PartOfId",
                table: "Investigations");

            migrationBuilder.DropColumn(
                name: "InvestigationId",
                table: "InvestigationRange");

            migrationBuilder.DropColumn(
                name: "InvestigationId",
                table: "InvestigationBinaryResult");

            migrationBuilder.RenameColumn(
                name: "Result",
                table: "InvestigationBinaryResult",
                newName: "Name");

            migrationBuilder.AddColumn<long>(
                name: "InvestigationComponentId",
                table: "InvestigationSuggestion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InvestigationComponentId",
                table: "InvestigationRange",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InvestigationComponentId",
                table: "InvestigationBinaryResult",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InvestigationComponent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvestigationId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    SnomedId = table.Column<string>(type: "text", nullable: true),
                    Specimen = table.Column<string>(type: "text", nullable: true),
                    Synonyms = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationSuggestion_InvestigationComponentId",
                table: "InvestigationSuggestion",
                column: "InvestigationComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationRange_InvestigationComponentId",
                table: "InvestigationRange",
                column: "InvestigationComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationBinaryResult_InvestigationComponentId",
                table: "InvestigationBinaryResult",
                column: "InvestigationComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationComponent_InvestigationId",
                table: "InvestigationComponent",
                column: "InvestigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationBinaryResult_InvestigationComponent_Investigat~",
                table: "InvestigationBinaryResult",
                column: "InvestigationComponentId",
                principalTable: "InvestigationComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationRange_InvestigationComponent_InvestigationComp~",
                table: "InvestigationRange",
                column: "InvestigationComponentId",
                principalTable: "InvestigationComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationSuggestion_InvestigationComponent_Investigatio~",
                table: "InvestigationSuggestion",
                column: "InvestigationComponentId",
                principalTable: "InvestigationComponent",
                principalColumn: "Id");
        }
    }
}

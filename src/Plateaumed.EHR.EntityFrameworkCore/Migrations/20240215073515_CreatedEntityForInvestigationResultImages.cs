using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class CreatedEntityForInvestigationResultImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ElectroRadPulmInvestigationResult");

            migrationBuilder.CreateTable(
                name: "ElectroRadPulmInvestigationResultImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ElectroRadPulmInvestigationResultId = table.Column<long>(type: "bigint", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
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
                    table.PrimaryKey("PK_ElectroRadPulmInvestigationResultImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectroRadPulmInvestigationResultImages_ElectroRadPulmInves~",
                        column: x => x.ElectroRadPulmInvestigationResultId,
                        principalTable: "ElectroRadPulmInvestigationResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectroRadPulmInvestigationResultImages_ElectroRadPulmInves~",
                table: "ElectroRadPulmInvestigationResultImages",
                column: "ElectroRadPulmInvestigationResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectroRadPulmInvestigationResultImages");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ElectroRadPulmInvestigationResult",
                type: "text",
                nullable: true);
        }
    }
}

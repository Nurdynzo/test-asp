using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPatientEntityAndOccupationRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientOccupations_PatientOccupationCategories_PatientOccup~",
                table: "PatientOccupations");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PatientOccupationCategories_PatientOccupationCateg~",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PatientOccupationCategories_PatientOccupationCate~1",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PatientOccupations_PatientOccupationCategoryId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PatientOccupations_PatientOccupationId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "PatientOccupationCategories");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PatientOccupationCategoryFkId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PatientOccupationCategoryId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PatientOccupationId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LocationOfWork",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientOccupationCategoryFkId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientOccupationCategoryId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientOccupationId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "PatientOccupationCategoryId",
                table: "PatientOccupations",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientOccupations_PatientOccupationCategoryId",
                table: "PatientOccupations",
                newName: "IX_PatientOccupations_PatientId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "PatientOccupations",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "PatientOccupations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "PatientOccupations",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "PatientOccupations",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OccupationId",
                table: "PatientOccupations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "PatientOccupations",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OccupationCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccupationCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Occupations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    OccupationCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Occupations_OccupationCategories_OccupationCategoryId",
                        column: x => x.OccupationCategoryId,
                        principalTable: "OccupationCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientOccupations_OccupationId",
                table: "PatientOccupations",
                column: "OccupationId");

            migrationBuilder.CreateIndex(
                name: "IX_OccupationCategories_TenantId",
                table: "OccupationCategories",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_OccupationCategoryId",
                table: "Occupations",
                column: "OccupationCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientOccupations_Occupations_OccupationId",
                table: "PatientOccupations",
                column: "OccupationId",
                principalTable: "Occupations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientOccupations_Patients_PatientId",
                table: "PatientOccupations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientOccupations_Occupations_OccupationId",
                table: "PatientOccupations");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientOccupations_Patients_PatientId",
                table: "PatientOccupations");

            migrationBuilder.DropTable(
                name: "Occupations");

            migrationBuilder.DropTable(
                name: "OccupationCategories");

            migrationBuilder.DropIndex(
                name: "IX_PatientOccupations_OccupationId",
                table: "PatientOccupations");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "PatientOccupations");

            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "PatientOccupations");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "PatientOccupations");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "PatientOccupations");

            migrationBuilder.DropColumn(
                name: "OccupationId",
                table: "PatientOccupations");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "PatientOccupations");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "PatientOccupations",
                newName: "PatientOccupationCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientOccupations_PatientId",
                table: "PatientOccupations",
                newName: "IX_PatientOccupations_PatientOccupationCategoryId");

            migrationBuilder.AddColumn<string>(
                name: "LocationOfWork",
                table: "Patients",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PatientOccupationCategoryFkId",
                table: "Patients",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PatientOccupationCategoryId",
                table: "Patients",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PatientOccupationId",
                table: "Patients",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PatientOccupationCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientOccupationCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientOccupationCategoryFkId",
                table: "Patients",
                column: "PatientOccupationCategoryFkId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientOccupationCategoryId",
                table: "Patients",
                column: "PatientOccupationCategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientOccupationId",
                table: "Patients",
                column: "PatientOccupationId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientOccupationCategories_TenantId",
                table: "PatientOccupationCategories",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientOccupations_PatientOccupationCategories_PatientOccup~",
                table: "PatientOccupations",
                column: "PatientOccupationCategoryId",
                principalTable: "PatientOccupationCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PatientOccupationCategories_PatientOccupationCateg~",
                table: "Patients",
                column: "PatientOccupationCategoryFkId",
                principalTable: "PatientOccupationCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PatientOccupationCategories_PatientOccupationCate~1",
                table: "Patients",
                column: "PatientOccupationCategoryId",
                principalTable: "PatientOccupationCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PatientOccupations_PatientOccupationCategoryId",
                table: "Patients",
                column: "PatientOccupationCategoryId",
                principalTable: "PatientOccupations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PatientOccupations_PatientOccupationId",
                table: "Patients",
                column: "PatientOccupationId",
                principalTable: "PatientOccupations",
                principalColumn: "Id");
        }
    }
}

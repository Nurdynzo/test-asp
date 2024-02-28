using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedFacilityGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacilityGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: true),
                    Website = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    Address = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    City = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    State = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    PostCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    BankAccountHolder = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    BankAccountNumber = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    PatientCodeTemplateId = table.Column<long>(type: "bigint", nullable: false),
                    StaffCodeTemplateId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityGroups_PatientCodeTemplates_PatientCodeTemplateId",
                        column: x => x.PatientCodeTemplateId,
                        principalTable: "PatientCodeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacilityGroups_StaffCodeTemplates_StaffCodeTemplateId",
                        column: x => x.StaffCodeTemplateId,
                        principalTable: "StaffCodeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacilityGroups_PatientCodeTemplateId",
                table: "FacilityGroups",
                column: "PatientCodeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityGroups_StaffCodeTemplateId",
                table: "FacilityGroups",
                column: "StaffCodeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityGroups_TenantId",
                table: "FacilityGroups",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilityGroups");
        }
    }
}

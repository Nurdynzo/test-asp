using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.MultiTenancy;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class CreateOnboardingProgressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnboardingStatus",
                table: "AppTenants");

            migrationBuilder.CreateTable(
                name: "TenantOnboardingProgress",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantOnboardingStatus = table.Column<TenantOnboardingStatus>(type: "tenant_onboarding_status", nullable: false),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantOnboardingProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantOnboardingProgress_AppTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AppTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantOnboardingProgress_TenantId",
                table: "TenantOnboardingProgress",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantOnboardingProgress");

            migrationBuilder.AddColumn<TenantOnboardingStatus>(
                name: "OnboardingStatus",
                table: "AppTenants",
                type: "tenant_onboarding_status",
                nullable: true);
        }
    }
}

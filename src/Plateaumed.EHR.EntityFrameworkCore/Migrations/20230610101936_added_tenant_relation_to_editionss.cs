using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class addedtenantrelationtoeditionss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppEditions_Countries_CountryId",
                table: "AppEditions"); 

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "AppEditions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppEditions_AppTenants_TenantId",
                table: "AppEditions",
                column: "TenantId",
                principalTable: "AppTenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEditions_Countries_CountryId",
                table: "AppEditions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppEditions_AppTenants_TenantId",
                table: "AppEditions");

            migrationBuilder.DropForeignKey(
                name: "FK_AppEditions_Countries_CountryId",
                table: "AppEditions"); 

            migrationBuilder.AlterColumn<long>(
                name: "CountryId",
                table: "AppEditions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true); 
        }
    }
}

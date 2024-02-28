using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeachMedicationProductEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {  
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "varchar(255)", nullable: true),
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
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenericDrugs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GenericSctName = table.Column<string>(type: "varchar(500)", nullable: true),
                    ActiveIngredients = table.Column<string>(type: "varchar(500)", nullable: true),
                    DoseForm = table.Column<string>(type: "varchar(500)", nullable: true),
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
                    table.PrimaryKey("PK_GenericDrugs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductName = table.Column<string>(type: "varchar(500)", nullable: true),
                    NigeriaRegNo = table.Column<string>(type: "varchar(500)", nullable: true),
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    BrandName = table.Column<string>(type: "varchar(500)", nullable: true),
                    Manufacturer = table.Column<string>(type: "varchar(500)", nullable: true),
                    GenericsSctId = table.Column<long>(type: "bigint", nullable: true),
                    SnowmedId = table.Column<string>(type: "varchar(100)", nullable: true),
                    DoseFormId = table.Column<long>(type: "bigint", nullable: true),
                    DoseFormName = table.Column<string>(type: "varchar(500)", nullable: true),
                    ActiveIngredients = table.Column<string>(type: "varchar(500)", nullable: true),
                    DoseStrengthId = table.Column<long>(type: "bigint", nullable: true),
                    DoseStrengthName = table.Column<string>(type: "varchar(500)", nullable: true),
                    CountryOfManufacture = table.Column<string>(type: "varchar(500)", nullable: true),
                    Synonyms = table.Column<string>(type: "varchar(500)", nullable: true),
                    Synonyms2 = table.Column<string>(type: "varchar(500)", nullable: true),
                    Synonyms3 = table.Column<string>(type: "varchar(500)", nullable: true),
                    Notes = table.Column<string>(type: "varchar(2000)", nullable: true),
                    SnowmedCategoryId1 = table.Column<string>(type: "varchar(100)", nullable: true),
                    SnowmedCategoryName1 = table.Column<string>(type: "varchar(500)", nullable: true),
                    SnowmedCategoryId2 = table.Column<string>(type: "varchar(100)", nullable: true),
                    SnowmedCategoryName2 = table.Column<string>(type: "varchar(500)", nullable: true),
                    SnowmedCategoryId3 = table.Column<string>(type: "varchar(100)", nullable: true),
                    SnowmedCategoryName3 = table.Column<string>(type: "varchar(500)", nullable: true),
                    SnowmedCategoryId4 = table.Column<string>(type: "varchar(100)", nullable: true),
                    SnowmedCategoryName4 = table.Column<string>(type: "varchar(500)", nullable: true),
                    SnowmedCategoryId5 = table.Column<string>(type: "varchar(100)", nullable: true),
                    SnowmedCategoryName5 = table.Column<string>(type: "varchar(500)", nullable: true),
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
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_GenericDrugs_GenericSctName",
                table: "GenericDrugs",
                column: "GenericSctName");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "GenericDrugs");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

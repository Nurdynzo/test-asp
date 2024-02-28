using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedSnowmedBodyPartsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SnowmedBodyParts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SnowmedId = table.Column<long>(type: "bigint", nullable: true),
                    Part = table.Column<string>(type: "varchar(100)", nullable: true),
                    SubPart = table.Column<string>(type: "varchar(255)", nullable: true),
                    Synonym = table.Column<string>(type: "varchar(300)", nullable: true),
                    Description = table.Column<string>(type: "varchar(500)", nullable: true),
                    Gender = table.Column<string>(type: "varchar(2)", nullable: true),
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
                    table.PrimaryKey("PK_SnowmedBodyParts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SnowmedBodyParts_Part",
                table: "SnowmedBodyParts",
                column: "Part");

            migrationBuilder.CreateIndex(
                name: "IX_SnowmedBodyParts_Synonym",
                table: "SnowmedBodyParts",
                column: "Synonym");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnowmedBodyParts");
        }
    }
}

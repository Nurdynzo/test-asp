using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedSpecializedProcedureEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpecializedProcedure",
                table: "Procedures",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SpecializedProcedures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: false),
                    Procedures = table.Column<string>(type: "text", nullable: true),
                    RequireAnaesthetist = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsProcedureInSameSession = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    AnaesthetistId = table.Column<long>(type: "bigint", nullable: true),
                    RoomId = table.Column<long>(type: "bigint", nullable: true),
                    Duration = table.Column<string>(type: "varchar(20)", nullable: true),
                    ProposedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Time = table.Column<string>(type: "varchar(20)", nullable: true),
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
                    table.PrimaryKey("PK_SpecializedProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecializedProcedures_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecializedProcedures_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecializedProcedures_StaffMembers_AnaesthetistId",
                        column: x => x.AnaesthetistId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecializedProcedures_AnaesthetistId",
                table: "SpecializedProcedures",
                column: "AnaesthetistId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecializedProcedures_ProcedureId",
                table: "SpecializedProcedures",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecializedProcedures_RoomId",
                table: "SpecializedProcedures",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecializedProcedures");

            migrationBuilder.DropColumn(
                name: "IsSpecializedProcedure",
                table: "Procedures");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduledProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleProcedures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: false),
                    SnowmedId = table.Column<long>(type: "bigint", nullable: true),
                    ProcedureName = table.Column<string>(type: "text", nullable: true),
                    RequireAnaesthetist = table.Column<bool>(type: "boolean", nullable: false),
                    IsProcedureInSameSession = table.Column<bool>(type: "boolean", nullable: false),
                    RoomId = table.Column<long>(type: "bigint", nullable: true),
                    Duration = table.Column<string>(type: "text", nullable: true),
                    ProposedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Time = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_ScheduleProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleProcedures_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleProcedures_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleProcedures_ProcedureId",
                table: "ScheduleProcedures",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleProcedures_RoomId",
                table: "ScheduleProcedures",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleProcedures");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddBedMakings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BedMaking",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    Stamp = table.Column<long>(type: "bigint", nullable: true),
                    AppointmentId = table.Column<long>(type: "bigint", nullable: true),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    BedMakingSnowmedId = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "varchar(2000)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_BedMaking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BedMaking_AppUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BedMaking_AppUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BedMaking_AppUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BedMaking_PatientAppointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "PatientAppointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BedMaking_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BedMaking_AppointmentId",
                table: "BedMaking",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BedMaking_CreatorUserId",
                table: "BedMaking",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BedMaking_DeleterUserId",
                table: "BedMaking",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BedMaking_LastModifierUserId",
                table: "BedMaking",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BedMaking_PatientId",
                table: "BedMaking",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_BedMaking_TenantId_PatientId",
                table: "BedMaking",
                columns: new[] { "TenantId", "PatientId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BedMaking");
        }
    }
}

﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class BedMakingSnowmedIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BedMakingSnowmedId",
                table: "BedMaking",
                newName: "BedMakingSnowmedIds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BedMakingSnowmedIds",
                table: "BedMaking",
                newName: "BedMakingSnowmedId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Remembvoc.Migrations
{
    /// <inheritdoc />
    public partial class AddedPrioritiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Points = table.Column<double>(type: "REAL", nullable: false),
                    LastCheck = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MinutesToRepeat = table.Column<int>(type: "INTEGER", nullable: false),
                    Period = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Priorities_Words_Id",
                        column: x => x.Id,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Priorities");
        }
    }
}

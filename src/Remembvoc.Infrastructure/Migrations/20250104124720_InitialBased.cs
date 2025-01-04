using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Remembvoc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialBased : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("PRAGMA foreign_keys = 0;");
            
            migrationBuilder.DropPrimaryKey(
                name: "PK_Priorities",
                table: "Priorities");

            migrationBuilder.DropIndex(
                name: "IX_Priorities_WordId",
                table: "Priorities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Priorities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Priorities",
                table: "Priorities",
                column: "WordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("PRAGMA foreign_keys = 1;");
            
            migrationBuilder.DropPrimaryKey(
                name: "PK_Priorities",
                table: "Priorities");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Priorities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Priorities",
                table: "Priorities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Priorities_WordId",
                table: "Priorities",
                column: "WordId",
                unique: true);
        }
    }
}

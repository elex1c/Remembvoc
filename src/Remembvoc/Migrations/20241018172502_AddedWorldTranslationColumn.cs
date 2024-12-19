using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Remembvoc.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorldTranslationColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Translation",
                table: "Words",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Translation",
                table: "Words");
        }
    }
}

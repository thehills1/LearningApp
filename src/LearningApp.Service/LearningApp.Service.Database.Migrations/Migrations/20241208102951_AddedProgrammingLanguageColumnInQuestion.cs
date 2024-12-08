using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningApp.Service.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedProgrammingLanguageColumnInQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MultipleCorrectAnswers",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "ProgrammingLanguage",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgrammingLanguage",
                table: "Questions");

            migrationBuilder.AddColumn<bool>(
                name: "MultipleCorrectAnswers",
                table: "Questions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
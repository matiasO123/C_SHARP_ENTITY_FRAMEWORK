using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ef_code_first.Migrations
{
    /// <inheritdoc />
    public partial class user_linked_to_WorkExperience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_workingExperiences_UserId",
                table: "workingExperiences",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_workingExperiences_Users_UserId",
                table: "workingExperiences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workingExperiences_Users_UserId",
                table: "workingExperiences");

            migrationBuilder.DropIndex(
                name: "IX_workingExperiences_UserId",
                table: "workingExperiences");
        }
    }
}

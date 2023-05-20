using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeCare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyFeedbackModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubmitedByStudentId",
                table: "RequestFeedBacks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestFeedBacks_SubmitedByStudentId",
                table: "RequestFeedBacks",
                column: "SubmitedByStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFeedBacks_Students_SubmitedByStudentId",
                table: "RequestFeedBacks",
                column: "SubmitedByStudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestFeedBacks_Students_SubmitedByStudentId",
                table: "RequestFeedBacks");

            migrationBuilder.DropIndex(
                name: "IX_RequestFeedBacks_SubmitedByStudentId",
                table: "RequestFeedBacks");

            migrationBuilder.DropColumn(
                name: "SubmitedByStudentId",
                table: "RequestFeedBacks");
        }
    }
}

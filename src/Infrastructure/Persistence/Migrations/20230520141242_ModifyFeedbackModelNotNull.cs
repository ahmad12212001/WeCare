using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeCare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyFeedbackModelNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestFeedBacks_Students_SubmitedByStudentId",
                table: "RequestFeedBacks");

            migrationBuilder.AlterColumn<int>(
                name: "SubmitedByStudentId",
                table: "RequestFeedBacks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFeedBacks_Students_SubmitedByStudentId",
                table: "RequestFeedBacks",
                column: "SubmitedByStudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestFeedBacks_Students_SubmitedByStudentId",
                table: "RequestFeedBacks");

            migrationBuilder.AlterColumn<int>(
                name: "SubmitedByStudentId",
                table: "RequestFeedBacks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFeedBacks_Students_SubmitedByStudentId",
                table: "RequestFeedBacks",
                column: "SubmitedByStudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}

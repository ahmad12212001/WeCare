using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeCare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExamAddName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Courses_CourseId",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Exams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Courses_CourseId",
                table: "Requests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Courses_CourseId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Exams");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Requests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Courses_CourseId",
                table: "Requests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}

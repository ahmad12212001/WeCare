using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeCare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCourseAddMajorGroupRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MajorGroupId",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MajorGroupId",
                table: "Courses",
                column: "MajorGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_MajorGroups_MajorGroupId",
                table: "Courses",
                column: "MajorGroupId",
                principalTable: "MajorGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_MajorGroups_MajorGroupId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_MajorGroupId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MajorGroupId",
                table: "Courses");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeCare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyMajorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_MajorGroups_MajorGroupId",
                table: "Majors");

            migrationBuilder.AlterColumn<int>(
                name: "MajorGroupId",
                table: "Majors",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_MajorGroups_MajorGroupId",
                table: "Majors",
                column: "MajorGroupId",
                principalTable: "MajorGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_MajorGroups_MajorGroupId",
                table: "Majors");

            migrationBuilder.AlterColumn<int>(
                name: "MajorGroupId",
                table: "Majors",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_MajorGroups_MajorGroupId",
                table: "Majors",
                column: "MajorGroupId",
                principalTable: "MajorGroups",
                principalColumn: "Id");
        }
    }
}

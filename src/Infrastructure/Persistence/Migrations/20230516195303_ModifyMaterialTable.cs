using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WeCare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyMaterialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<int>(
                name: "TotalRequest",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Materials",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Materials",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Materials",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MajorGroupId",
                table: "Majors",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MajorGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_RequestId",
                table: "Materials",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Majors_MajorGroupId",
                table: "Majors",
                column: "MajorGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_MajorGroups_MajorGroupId",
                table: "Majors",
                column: "MajorGroupId",
                principalTable: "MajorGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Requests_RequestId",
                table: "Materials",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_MajorGroups_MajorGroupId",
                table: "Majors");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Requests_RequestId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "MajorGroups");

            migrationBuilder.DropIndex(
                name: "IX_Materials_RequestId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Majors_MajorGroupId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "TotalRequest",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "MajorGroupId",
                table: "Majors");

        }
    }
}

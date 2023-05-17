using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WeCare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDocumentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.AddColumn<int>(
                name: "MaterialStatus",
                table: "Materials",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VolunteerStudentId",
                table: "Materials",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_VolunteerStudentId",
                table: "Materials",
                column: "VolunteerStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Students_VolunteerStudentId",
                table: "Materials",
                column: "VolunteerStudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Students_VolunteerStudentId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_VolunteerStudentId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "MaterialStatus",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "VolunteerStudentId",
                table: "Materials");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaterialId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    DocumentType = table.Column<int>(type: "integer", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Materiald = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_MaterialId",
                table: "Documents",
                column: "MaterialId");
        }
    }
}

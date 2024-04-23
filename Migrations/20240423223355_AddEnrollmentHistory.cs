using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloud_MVC_Tutorial.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrollmentHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "Enrollment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Enrollment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "EnrollmentHistories",
                columns: table => new
                {
                    EnrollmentHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentHistories", x => x.EnrollmentHistoryId);
                    table.ForeignKey(
                        name: "FK_EnrollmentHistories_Enrollment_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollment",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentHistories_EnrollmentId",
                table: "EnrollmentHistories",
                column: "EnrollmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentHistories");

            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Enrollment");
        }
    }
}

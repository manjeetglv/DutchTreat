using Microsoft.EntityFrameworkCore.Migrations;

namespace Drums.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolYear = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Period = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    StudentFullName = table.Column<string>(nullable: true),
                    ReportType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeSpecificSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<bool>(nullable: false),
                    ReportCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeSpecificSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeSpecificSetting_ReportCards_ReportCardId",
                        column: x => x.ReportCardId,
                        principalTable: "ReportCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LayoutSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<bool>(nullable: false),
                    ReportCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LayoutSetting_ReportCards_ReportCardId",
                        column: x => x.ReportCardId,
                        principalTable: "ReportCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportCardContentSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<bool>(nullable: false),
                    ReportCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCardContentSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportCardContentSetting_ReportCards_ReportCardId",
                        column: x => x.ReportCardId,
                        principalTable: "ReportCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeSpecificSetting_ReportCardId",
                table: "GradeSpecificSetting",
                column: "ReportCardId");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutSetting_ReportCardId",
                table: "LayoutSetting",
                column: "ReportCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCardContentSetting_ReportCardId",
                table: "ReportCardContentSetting",
                column: "ReportCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradeSpecificSetting");

            migrationBuilder.DropTable(
                name: "LayoutSetting");

            migrationBuilder.DropTable(
                name: "ReportCardContentSetting");

            migrationBuilder.DropTable(
                name: "ReportCards");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGSheet2.Data.Migrations
{
    public partial class tutorialpages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SentTime",
                table: "GameMessages",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 20, 14, 23, 17, 933, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 4, 9, 58, 23, 574, DateTimeKind.Utc));

            migrationBuilder.CreateTable(
                name: "TutorialPages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialPages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TutorialSections",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Header = table.Column<string>(nullable: true),
                    HTMLContent = table.Column<string>(nullable: true),
                    PageID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialSections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TutorialSections_TutorialPages_PageID",
                        column: x => x.PageID,
                        principalTable: "TutorialPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorialSections_PageID",
                table: "TutorialSections",
                column: "PageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorialSections");

            migrationBuilder.DropTable(
                name: "TutorialPages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentTime",
                table: "GameMessages",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 4, 9, 58, 23, 574, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 20, 14, 23, 17, 933, DateTimeKind.Utc));
        }
    }
}

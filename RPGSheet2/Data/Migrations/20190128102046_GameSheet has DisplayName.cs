using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGSheet2.Data.Migrations
{
    public partial class GameSheethasDisplayName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "GameSheet",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentTime",
                table: "GameMessages",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 28, 10, 20, 46, 307, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 22, 15, 10, 43, 215, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "GameSheet");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentTime",
                table: "GameMessages",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 22, 15, 10, 43, 215, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 28, 10, 20, 46, 307, DateTimeKind.Utc));
        }
    }
}

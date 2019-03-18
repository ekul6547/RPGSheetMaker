using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGSheet2.Data.Migrations
{
    public partial class RemovedUsernamefromGameAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "GameAccesses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentTime",
                table: "GameMessages",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 4, 9, 58, 23, 574, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 28, 10, 20, 46, 307, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SentTime",
                table: "GameMessages",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 28, 10, 20, 46, 307, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 4, 9, 58, 23, 574, DateTimeKind.Utc));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "GameAccesses",
                nullable: true);
        }
    }
}

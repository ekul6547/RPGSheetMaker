using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGSheet2.Data.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordValidTime",
                table: "Games");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentTime",
                table: "GameMessages",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 22, 15, 10, 43, 215, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 22, 11, 45, 36, 880, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "PasswordValidTime",
                table: "Games",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentTime",
                table: "GameMessages",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 22, 11, 45, 36, 880, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 22, 15, 10, 43, 215, DateTimeKind.Utc));
        }
    }
}

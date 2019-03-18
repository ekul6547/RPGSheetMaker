using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGSheet2.Data.Migrations
{
    public partial class initcarryover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sheets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SheetName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    OwnerID = table.Column<string>(nullable: true),
                    Version = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GameSheet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    originalSheetID = table.Column<int>(nullable: true),
                    Version = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSheet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GameSheet_Sheets_originalSheetID",
                        column: x => x.originalSheetID,
                        principalTable: "Sheets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SheetFields",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sheetID = table.Column<int>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    value_string = table.Column<string>(nullable: true),
                    value_int = table.Column<int>(nullable: false),
                    value_float = table.Column<float>(nullable: false),
                    value_bool = table.Column<bool>(nullable: false),
                    value_stat = table.Column<int>(nullable: false),
                    value_stat_midpoint = table.Column<int>(nullable: false),
                    value_stat_divisor = table.Column<int>(nullable: false),
                    IsDropdown = table.Column<bool>(nullable: false),
                    xpos = table.Column<float>(nullable: false),
                    ypos = table.Column<float>(nullable: false),
                    height = table.Column<float>(nullable: false),
                    width = table.Column<float>(nullable: false),
                    hexColour = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetFields", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SheetFields_Sheets_sheetID",
                        column: x => x.sheetID,
                        principalTable: "Sheets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SearchID = table.Column<string>(nullable: true),
                    OwnerID = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    PasswordValidTime = table.Column<TimeSpan>(nullable: false),
                    gameSheetID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Games_GameSheet_gameSheetID",
                        column: x => x.gameSheetID,
                        principalTable: "GameSheet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameSheetFields",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OriginalFieldID = table.Column<int>(nullable: true),
                    gameSheetID = table.Column<int>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    value_string = table.Column<string>(nullable: true),
                    value_int = table.Column<int>(nullable: false),
                    value_float = table.Column<float>(nullable: false),
                    value_bool = table.Column<bool>(nullable: false),
                    value_stat = table.Column<int>(nullable: false),
                    value_stat_midpoint = table.Column<int>(nullable: false),
                    value_stat_divisor = table.Column<int>(nullable: false),
                    IsDropdown = table.Column<bool>(nullable: false),
                    xpos = table.Column<float>(nullable: false),
                    ypos = table.Column<float>(nullable: false),
                    height = table.Column<float>(nullable: false),
                    width = table.Column<float>(nullable: false),
                    hexColour = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSheetFields", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GameSheetFields_SheetFields_OriginalFieldID",
                        column: x => x.OriginalFieldID,
                        principalTable: "SheetFields",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameSheetFields_GameSheet_gameSheetID",
                        column: x => x.gameSheetID,
                        principalTable: "GameSheet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gameID = table.Column<int>(nullable: true),
                    OwnerID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsNPC = table.Column<bool>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Characters_Games_gameID",
                        column: x => x.gameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameAccesses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gameID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameAccesses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GameAccesses_Games_gameID",
                        column: x => x.gameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameMessages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gameID = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    SenderID = table.Column<string>(nullable: true),
                    SentTime = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 1, 22, 11, 45, 36, 880, DateTimeKind.Utc))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GameMessages_Games_gameID",
                        column: x => x.gameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DropdownValues",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gameSheetFieldID = table.Column<int>(nullable: true),
                    Key = table.Column<byte>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DropdownValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DropdownValues_GameSheetFields_gameSheetFieldID",
                        column: x => x.gameSheetFieldID,
                        principalTable: "GameSheetFields",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharacterValues",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gameSheetFieldID = table.Column<int>(nullable: true),
                    characterID = table.Column<int>(nullable: true),
                    value_string = table.Column<string>(nullable: true),
                    value_int = table.Column<int>(nullable: false),
                    value_float = table.Column<float>(nullable: false),
                    value_bool = table.Column<bool>(nullable: false),
                    value_stat = table.Column<int>(nullable: false),
                    value_dropdown = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CharacterValues_Characters_characterID",
                        column: x => x.characterID,
                        principalTable: "Characters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CharacterValues_GameSheetFields_gameSheetFieldID",
                        column: x => x.gameSheetFieldID,
                        principalTable: "GameSheetFields",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnHideFor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    characterID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnHideFor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnHideFor_Characters_characterID",
                        column: x => x.characterID,
                        principalTable: "Characters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_gameID",
                table: "Characters",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterValues_characterID",
                table: "CharacterValues",
                column: "characterID");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterValues_gameSheetFieldID",
                table: "CharacterValues",
                column: "gameSheetFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_DropdownValues_gameSheetFieldID",
                table: "DropdownValues",
                column: "gameSheetFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_GameAccesses_gameID",
                table: "GameAccesses",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_GameMessages_gameID",
                table: "GameMessages",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_gameSheetID",
                table: "Games",
                column: "gameSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_GameSheet_originalSheetID",
                table: "GameSheet",
                column: "originalSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_GameSheetFields_OriginalFieldID",
                table: "GameSheetFields",
                column: "OriginalFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_GameSheetFields_gameSheetID",
                table: "GameSheetFields",
                column: "gameSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_SheetFields_sheetID",
                table: "SheetFields",
                column: "sheetID");

            migrationBuilder.CreateIndex(
                name: "IX_UnHideFor_characterID",
                table: "UnHideFor",
                column: "characterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterValues");

            migrationBuilder.DropTable(
                name: "DropdownValues");

            migrationBuilder.DropTable(
                name: "GameAccesses");

            migrationBuilder.DropTable(
                name: "GameMessages");

            migrationBuilder.DropTable(
                name: "UnHideFor");

            migrationBuilder.DropTable(
                name: "GameSheetFields");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "SheetFields");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "GameSheet");

            migrationBuilder.DropTable(
                name: "Sheets");
        }
    }
}

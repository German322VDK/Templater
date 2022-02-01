using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teplater.SQLite.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarkKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarkValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarkKey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    MarkKeysId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarkKey_MarkKeys_MarkKeysId",
                        column: x => x.MarkKeysId,
                        principalTable: "MarkKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    KeysId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_MarkKeys_KeysId",
                        column: x => x.KeysId,
                        principalTable: "MarkKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarkValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    MarkValuesId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarkValue_MarkValues_MarkValuesId",
                        column: x => x.MarkValuesId,
                        principalTable: "MarkValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateTimeInitial = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValuesId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_MarkValues_ValuesId",
                        column: x => x.ValuesId,
                        principalTable: "MarkValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TemplateId",
                table: "Documents",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ValuesId",
                table: "Documents",
                column: "ValuesId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkKey_MarkKeysId",
                table: "MarkKey",
                column: "MarkKeysId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkValue_MarkValuesId",
                table: "MarkValue",
                column: "MarkValuesId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_KeysId",
                table: "Templates",
                column: "KeysId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "MarkKey");

            migrationBuilder.DropTable(
                name: "MarkValue");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "MarkValues");

            migrationBuilder.DropTable(
                name: "MarkKeys");
        }
    }
}

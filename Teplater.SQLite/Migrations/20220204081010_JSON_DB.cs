using Microsoft.EntityFrameworkCore.Migrations;

namespace Teplater.SQLite.Migrations
{
    public partial class JSON_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_MarkValues_ValuesId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Templates_MarkKeys_KeysId",
                table: "Templates");

            migrationBuilder.DropTable(
                name: "MarkKey");

            migrationBuilder.DropTable(
                name: "MarkValue");

            migrationBuilder.DropTable(
                name: "MarkKeys");

            migrationBuilder.DropTable(
                name: "MarkValues");

            migrationBuilder.DropIndex(
                name: "IX_Templates_KeysId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ValuesId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "KeysId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "ValuesId",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "JSONKeys",
                table: "Templates",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JSONValues",
                table: "Documents",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JSONKeys",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "JSONValues",
                table: "Documents");

            migrationBuilder.AddColumn<int>(
                name: "KeysId",
                table: "Templates",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ValuesId",
                table: "Documents",
                type: "INTEGER",
                nullable: true);

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
                name: "MarkValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MarkValuesId = table.Column<int>(type: "INTEGER", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Templates_KeysId",
                table: "Templates",
                column: "KeysId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_MarkValues_ValuesId",
                table: "Documents",
                column: "ValuesId",
                principalTable: "MarkValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_MarkKeys_KeysId",
                table: "Templates",
                column: "KeysId",
                principalTable: "MarkKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streamscape.Migrations
{
    /// <inheritdoc />
    public partial class MyList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Titles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Episodes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Episodes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserTitles",
                columns: table => new
                {
                    MyListId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTitles", x => new { x.MyListId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserTitles_Titles_MyListId",
                        column: x => x.MyListId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTitles_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 12, 1, 22, 42, 784, DateTimeKind.Utc).AddTicks(8922));

            migrationBuilder.CreateIndex(
                name: "IX_UserTitles_UsersId",
                table: "UserTitles",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTitles");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Episodes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 9, 12, 11, 36, 329, DateTimeKind.Utc).AddTicks(7876));
        }
    }
}

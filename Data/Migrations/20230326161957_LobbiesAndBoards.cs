using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class LobbiesAndBoards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Row1 = table.Column<int?[]>(type: "integer[]", nullable: false),
                    Row2 = table.Column<int?[]>(type: "integer[]", nullable: false),
                    Row3 = table.Column<int?[]>(type: "integer[]", nullable: false),
                    TurnCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lobbies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BoardId = table.Column<long>(type: "bigint", nullable: false),
                    XUserId = table.Column<string>(type: "text", nullable: true),
                    OUserId = table.Column<string>(type: "text", nullable: true),
                    Turn = table.Column<int>(type: "integer", nullable: false),
                    IsStared = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lobbies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lobbies_AspNetUsers_OUserId",
                        column: x => x.OUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lobbies_AspNetUsers_XUserId",
                        column: x => x.XUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lobbies_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lobbies_BoardId",
                table: "Lobbies",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Lobbies_OUserId",
                table: "Lobbies",
                column: "OUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lobbies_XUserId",
                table: "Lobbies",
                column: "XUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lobbies");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}

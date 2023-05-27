using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cSharp.Migrations
{
    /// <inheritdoc />
    public partial class preventCascadeInsert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsUsers_Chats_ChatId",
                table: "ChatsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatsUsers_Users_UserId",
                table: "ChatsUsers");

            migrationBuilder.DropIndex(
                name: "IX_ChatsUsers_ChatId",
                table: "ChatsUsers");

            migrationBuilder.DropIndex(
                name: "IX_ChatsUsers_UserId",
                table: "ChatsUsers");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "ChatsUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChatsUsers");

            migrationBuilder.CreateIndex(
                name: "IX_ChatsUsers_UsersId",
                table: "ChatsUsers",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsUsers_Chats_ChatsId",
                table: "ChatsUsers",
                column: "ChatsId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsUsers_Users_UsersId",
                table: "ChatsUsers",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsUsers_Chats_ChatsId",
                table: "ChatsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatsUsers_Users_UsersId",
                table: "ChatsUsers");

            migrationBuilder.DropIndex(
                name: "IX_ChatsUsers_UsersId",
                table: "ChatsUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatId",
                table: "ChatsUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ChatsUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ChatsUsers_ChatId",
                table: "ChatsUsers",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatsUsers_UserId",
                table: "ChatsUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsUsers_Chats_ChatId",
                table: "ChatsUsers",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsUsers_Users_UserId",
                table: "ChatsUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SimpleForumApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Users_PostId",
                table: "Commentaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Posts_PostId1",
                table: "Commentaries");

            migrationBuilder.DropIndex(
                name: "IX_Commentaries_PostId1",
                table: "Commentaries");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "Commentaries");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Posts_PostId",
                table: "Commentaries",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Posts_PostId",
                table: "Commentaries");

            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "Commentaries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_PostId1",
                table: "Commentaries",
                column: "PostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Users_PostId",
                table: "Commentaries",
                column: "PostId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Posts_PostId1",
                table: "Commentaries",
                column: "PostId1",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

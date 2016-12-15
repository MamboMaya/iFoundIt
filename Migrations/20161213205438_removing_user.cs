using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class removing_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finders_AspNetUsers_UserId",
                table: "Finders");

            migrationBuilder.DropIndex(
                name: "IX_Finders_UserId",
                table: "Finders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Finders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Finders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Finders_UserId",
                table: "Finders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finders_AspNetUsers_UserId",
                table: "Finders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

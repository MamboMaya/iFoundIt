using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class fixSecretDetails2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecretDetails",
                table: "Items");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "SecretDetails",
                table: "Items",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}

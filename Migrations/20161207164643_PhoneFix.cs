using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class PhoneFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Finders",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Finders",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Finders",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Finders",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Finders",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "Finders",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Finders",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Finders",
                nullable: true);
        }
    }
}

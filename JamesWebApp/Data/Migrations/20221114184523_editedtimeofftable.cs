using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JamesWebApp.Data.Migrations
{
    public partial class editedtimeofftable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOne",
                table: "TimeOff",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTwo",
                table: "TimeOff",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Vacation",
                table: "TimeOff",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOne",
                table: "TimeOff");

            migrationBuilder.DropColumn(
                name: "DateTwo",
                table: "TimeOff");

            migrationBuilder.DropColumn(
                name: "Vacation",
                table: "TimeOff");
        }
    }
}

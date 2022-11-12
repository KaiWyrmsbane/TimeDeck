using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JamesWebApp.Data.Migrations
{
    public partial class changedDateTimetoTimeSpanTimeWorked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TimeWorked",
                table: "TimeClock",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeWorked",
                table: "TimeClock",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }
    }
}

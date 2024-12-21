using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wpf页面设计WebAPI.Migrations
{
    public partial class ikkk1106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "AccountInfo");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "WaitInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "WaitInfos");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "AccountInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

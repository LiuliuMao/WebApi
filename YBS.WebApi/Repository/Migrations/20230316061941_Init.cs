using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "RoleInfo");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MenuInfo");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "UserInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "RoleInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "MenuInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "RoleInfo");

            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "MenuInfo");

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "UserInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "RoleInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "MenuInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}

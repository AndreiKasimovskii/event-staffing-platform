using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStaffingPlatform.Host.Migrations
{
    /// <inheritdoc />
    public partial class FixUpdateDateConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "positions");

            migrationBuilder.AlterColumn<string>(
                name: "requirements",
                table: "positions",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "create_date",
                table: "positions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_date",
                table: "positions");

            migrationBuilder.AlterColumn<string>(
                name: "requirements",
                table: "positions",
                type: "jsonb",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "positions",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}

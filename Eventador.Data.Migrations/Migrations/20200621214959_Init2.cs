using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventador.Data.Migrations.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_type",
                table: "users");

            migrationBuilder.DropColumn(
                name: "additional_info",
                table: "events");

            migrationBuilder.DropColumn(
                name: "event_status",
                table: "events");

            migrationBuilder.DropColumn(
                name: "event_type",
                table: "events");

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "author_id",
                table: "events",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "events",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "users");

            migrationBuilder.DropColumn(
                name: "status",
                table: "events");

            migrationBuilder.DropColumn(
                name: "type",
                table: "events");

            migrationBuilder.AddColumn<int>(
                name: "user_type",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "author_id",
                table: "events",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "additional_info",
                table: "events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "event_status",
                table: "events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "event_type",
                table: "events",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

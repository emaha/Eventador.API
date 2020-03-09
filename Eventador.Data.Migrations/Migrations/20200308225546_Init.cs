using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eventador.Data.Migrations.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    additional_info = table.Column<string>(nullable: true),
                    title_image_url = table.Column<string>(nullable: true),
                    image_urls = table.Column<string[]>(nullable: true),
                    event_type = table.Column<int>(nullable: false),
                    event_status = table.Column<int>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    region_id = table.Column<int>(nullable: false),
                    lat = table.Column<float>(nullable: true),
                    lon = table.Column<float>(nullable: true),
                    create_date = table.Column<DateTime>(nullable: false),
                    author_id = table.Column<int>(nullable: false),
                    access_type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "marks",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<int>(nullable: false),
                    comment = table.Column<string>(nullable: true),
                    create_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    last_name = table.Column<string>(nullable: true),
                    first_name = table.Column<string>(nullable: true),
                    middle_name = table.Column<string>(nullable: true),
                    login = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    blocked = table.Column<bool>(nullable: false),
                    user_type = table.Column<int>(nullable: false),
                    organization_name = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    about_info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "marks");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorFromDatabase.Data.Migrations
{
    public partial class AddPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Core_Page",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(maxLength: 200, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Publicity = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    LastRequested = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_Page", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Core_Page");
        }
    }
}

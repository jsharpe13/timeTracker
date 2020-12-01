using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace timeTracker.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    start = table.Column<DateTime>(nullable: false),
                    end = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Description", "Name", "end", "start" },
                values: new object[] { 1, "Throwing a surprise Birthday Party for Jessie", "Jessie's Birthday Party", new DateTime(2021, 1, 15, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 15, 5, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}

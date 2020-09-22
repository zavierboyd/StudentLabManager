using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudentLabManager.Migrations
{
    public partial class TestJSON : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                }) ;

            migrationBuilder.AddColumn<string>(
               name: "group",
               table: "Schedule",
               nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "duration",
                table: "Schedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "exam",
                table: "Schedule",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "schedule",
                table: "Schedule",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Schedule"
                );

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Schedule").Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duration",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "exam",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "schedule",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "group",
                table: "Schedule");

            migrationBuilder.DropTable(
                name: "Schedule");
        }
            
    }
}

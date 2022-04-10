using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital_testtkask.Migrations
{
    public partial class DomainName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Domains");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Domains",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Domains");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Domains",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

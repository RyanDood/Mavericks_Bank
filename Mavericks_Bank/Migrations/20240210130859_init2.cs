using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mavericks_Bank.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "AppliedLoans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountNumber",
                table: "AppliedLoans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}

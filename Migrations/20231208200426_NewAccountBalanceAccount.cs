using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillingAPI.Migrations
{
    public partial class NewAccountBalanceAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AccountBalance",
                table: "Accounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountBalance",
                table: "Accounts");
        }
    }
}

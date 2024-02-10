using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillingAPI.Migrations
{
    public partial class InitialAmountAddedToTheDeposit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "InitialDepositAmount",
                table: "Deposits",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialDepositAmount",
                table: "Deposits");
        }
    }
}

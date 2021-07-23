using Microsoft.EntityFrameworkCore.Migrations;

namespace Airline.DL.Migrations
{
    public partial class flightstatusadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscountCode",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Discountamount",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountCode",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Discountamount",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Flights");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Airline.DL.Migrations
{
    public partial class flightmodule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineID = table.Column<int>(type: "int", nullable: false),
                    Flightname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Takeoff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Landing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numberofbusinessseats = table.Column<int>(type: "int", nullable: false),
                    Numberofnonbusinessseats = table.Column<int>(type: "int", nullable: false),
                    Numberofrows = table.Column<int>(type: "int", nullable: false),
                    Scheduling = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Totalcost = table.Column<int>(type: "int", nullable: false),
                    Meals = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}

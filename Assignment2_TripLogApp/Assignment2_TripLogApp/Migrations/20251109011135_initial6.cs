using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment2_TripLogApp.Migrations
{
    /// <inheritdoc />
    public partial class initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripLogs",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accommodation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccommodationPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccommodationEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDo1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDo2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDo3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripLogs", x => x.TripId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripLogs");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsService.Migrations
{
    /// <inheritdoc />
    public partial class addlogtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Logger",
                schema: "dbo",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excepction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Eventdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrackingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Logger", x => x.LogId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Logger",
                schema: "dbo");
        }
    }
}

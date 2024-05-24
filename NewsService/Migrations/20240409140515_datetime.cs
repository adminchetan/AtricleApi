using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsService.Migrations
{
    /// <inheritdoc />
    public partial class datetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                schema: "dbo",
                table: "tbl_PostMaster",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "dbo",
                table: "tbl_PostMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}

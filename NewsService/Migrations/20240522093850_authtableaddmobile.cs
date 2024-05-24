using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsService.Migrations
{
    /// <inheritdoc />
    public partial class authtableaddmobile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                schema: "dbo",
                table: "tbl_AuthMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileNumber",
                schema: "dbo",
                table: "tbl_AuthMaster");
        }
    }
}

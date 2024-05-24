using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsService.Migrations
{
    /// <inheritdoc />
    public partial class makeEmailUniqueofAuthtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "dbo",
                table: "tbl_AuthMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AuthMaster_Email",
                schema: "dbo",
                table: "tbl_AuthMaster",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_AuthMaster_Email",
                schema: "dbo",
                table: "tbl_AuthMaster");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "dbo",
                table: "tbl_AuthMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

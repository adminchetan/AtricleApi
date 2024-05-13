using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsService.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationtableuserchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_UserData",
                table: "tbl_UserData");

            migrationBuilder.RenameTable(
                name: "tbl_UserData",
                newName: "tbl_UserMaster",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_UserMaster",
                schema: "dbo",
                table: "tbl_UserMaster",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_UserMaster",
                schema: "dbo",
                table: "tbl_UserMaster");

            migrationBuilder.RenameTable(
                name: "tbl_UserMaster",
                schema: "dbo",
                newName: "tbl_UserData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_UserData",
                table: "tbl_UserData",
                column: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsService.Migrations
{
    /// <inheritdoc />
    public partial class prod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StateId",
                schema: "dbo",
                table: "tbl_PostMaster",
                newName: "SubCategoryId");

            migrationBuilder.RenameColumn(
                name: "CityId",
                schema: "dbo",
                table: "tbl_PostMaster",
                newName: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                schema: "dbo",
                table: "tbl_PostMaster",
                newName: "StateId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "dbo",
                table: "tbl_PostMaster",
                newName: "CityId");
        }
    }
}

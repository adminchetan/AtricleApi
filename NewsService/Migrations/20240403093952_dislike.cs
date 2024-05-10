using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsService.Migrations
{
    /// <inheritdoc />
    public partial class dislike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "likes",
                schema: "dbo",
                table: "tbl_PostMaster",
                newName: "LikesCounts");

            migrationBuilder.AddColumn<int>(
                name: "DislikeCounts",
                schema: "dbo",
                table: "tbl_PostMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DislikeCounts",
                schema: "dbo",
                table: "tbl_PostMaster");

            migrationBuilder.RenameColumn(
                name: "LikesCounts",
                schema: "dbo",
                table: "tbl_PostMaster",
                newName: "likes");
        }
    }
}

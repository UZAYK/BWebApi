using Microsoft.EntityFrameworkCore.Migrations;

namespace BWebApi.Migrations
{
    public partial class addmigrationTableBlogsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetBlogs_Categories_CategoryId",
                table: "GetBlogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetBlogs",
                table: "GetBlogs");

            migrationBuilder.RenameTable(
                name: "GetBlogs",
                newName: "Blogs");

            migrationBuilder.RenameIndex(
                name: "IX_GetBlogs_CategoryId",
                table: "Blogs",
                newName: "IX_Blogs_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Categories_CategoryId",
                table: "Blogs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Categories_CategoryId",
                table: "Blogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "GetBlogs");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_CategoryId",
                table: "GetBlogs",
                newName: "IX_GetBlogs_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetBlogs",
                table: "GetBlogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GetBlogs_Categories_CategoryId",
                table: "GetBlogs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

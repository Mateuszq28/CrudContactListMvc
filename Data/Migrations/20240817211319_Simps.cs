using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudContactListMvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class Simps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SimpId",
                table: "Subcategory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SimpId",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SimpId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Simp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simp", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subcategory_SimpId",
                table: "Subcategory",
                column: "SimpId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_SimpId",
                table: "Contact",
                column: "SimpId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_SimpId",
                table: "Category",
                column: "SimpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Simp_SimpId",
                table: "Category",
                column: "SimpId",
                principalTable: "Simp",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Simp_SimpId",
                table: "Contact",
                column: "SimpId",
                principalTable: "Simp",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategory_Simp_SimpId",
                table: "Subcategory",
                column: "SimpId",
                principalTable: "Simp",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Simp_SimpId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Simp_SimpId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategory_Simp_SimpId",
                table: "Subcategory");

            migrationBuilder.DropTable(
                name: "Simp");

            migrationBuilder.DropIndex(
                name: "IX_Subcategory_SimpId",
                table: "Subcategory");

            migrationBuilder.DropIndex(
                name: "IX_Contact_SimpId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Category_SimpId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "SimpId",
                table: "Subcategory");

            migrationBuilder.DropColumn(
                name: "SimpId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "SimpId",
                table: "Category");
        }
    }
}

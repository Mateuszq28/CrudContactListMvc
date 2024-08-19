using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudContactListMvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class Spa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpaId",
                table: "Subcategory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpaId",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpaId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Spa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spa", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subcategory_SpaId",
                table: "Subcategory",
                column: "SpaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_SpaId",
                table: "Contact",
                column: "SpaId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_SpaId",
                table: "Category",
                column: "SpaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Spa_SpaId",
                table: "Category",
                column: "SpaId",
                principalTable: "Spa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Spa_SpaId",
                table: "Contact",
                column: "SpaId",
                principalTable: "Spa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategory_Spa_SpaId",
                table: "Subcategory",
                column: "SpaId",
                principalTable: "Spa",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Spa_SpaId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Spa_SpaId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategory_Spa_SpaId",
                table: "Subcategory");

            migrationBuilder.DropTable(
                name: "Spa");

            migrationBuilder.DropIndex(
                name: "IX_Subcategory_SpaId",
                table: "Subcategory");

            migrationBuilder.DropIndex(
                name: "IX_Contact_SpaId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Category_SpaId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "SpaId",
                table: "Subcategory");

            migrationBuilder.DropColumn(
                name: "SpaId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "SpaId",
                table: "Category");
        }
    }
}

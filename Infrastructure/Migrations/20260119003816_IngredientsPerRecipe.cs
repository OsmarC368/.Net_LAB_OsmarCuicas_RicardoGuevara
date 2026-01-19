using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IngredientsPerRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Recipes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MeasureIdIPR",
                table: "IngredientsPerRecipe",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsPerRecipe_MeasureIdIPR",
                table: "IngredientsPerRecipe",
                column: "MeasureIdIPR");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsPerRecipe_Measures_MeasureIdIPR",
                table: "IngredientsPerRecipe",
                column: "MeasureIdIPR",
                principalTable: "Measures",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsPerRecipe_Measures_MeasureIdIPR",
                table: "IngredientsPerRecipe");

            migrationBuilder.DropIndex(
                name: "IX_IngredientsPerRecipe_MeasureIdIPR",
                table: "IngredientsPerRecipe");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MeasureIdIPR",
                table: "IngredientsPerRecipe");
        }
    }
}
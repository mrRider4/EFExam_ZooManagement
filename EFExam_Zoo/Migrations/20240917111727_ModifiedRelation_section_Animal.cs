using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFExam_Zoo.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedRelation_section_Animal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Section_AnimalId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Animal");

            migrationBuilder.CreateIndex(
                name: "IX_Section_AnimalId",
                table: "Section",
                column: "AnimalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Section_AnimalId",
                table: "Section");

            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "Animal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Section_AnimalId",
                table: "Section",
                column: "AnimalId",
                unique: true,
                filter: "[AnimalId] IS NOT NULL");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneYonetim.Migrations
{
    /// <inheritdoc />
    public partial class ResimGuncelleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResimYolu",
                table: "Kitaplar",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResimYolu",
                table: "Kitaplar");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneYonetim.Migrations
{
    /// <inheritdoc />
    public partial class SQLiteGecis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kitaplar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: false),
                    Yazar = table.Column<string>(type: "TEXT", nullable: false),
                    SayfaSayisi = table.Column<int>(type: "INTEGER", nullable: false),
                    Tur = table.Column<string>(type: "TEXT", nullable: false),
                    BasimYili = table.Column<int>(type: "INTEGER", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitaplar", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kitaplar");
        }
    }
}

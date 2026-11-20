using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace lab_app.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Isbn = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NumPages = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Author = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Isbn", "NumPages", "Price", "PublicationDate", "Title" },
                values: new object[,]
                {
                    { 1, "Stanisław Lem", "978-83-08-06534-1", 204, 39.90m, new DateTime(1961, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solaris" },
                    { 2, "Bolesław Prus", "978-83-7327-000-8", 720, 29.50m, new DateTime(1890, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lalka" },
                    { 3, "Stanisław Lem", "978-83-08-06535-8", 320, 34.90m, new DateTime(1965, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cyberiada" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Isbn",
                table: "Books",
                column: "Isbn",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}

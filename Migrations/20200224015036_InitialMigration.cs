using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BandAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Founded = table.Column<DateTime>(nullable: true),
                    MainGenre = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: true),
                    BandID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Albums_Bands_BandID",
                        column: x => x.BandID,
                        principalTable: "Bands",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bands",
                columns: new[] { "ID", "Founded", "MainGenre", "Name" },
                values: new object[,]
                {
                    { new Guid("6b1eea43-5597-45a6-bdea-e68c60564247"), null, "Heavy Metal", "Metallica" },
                    { new Guid("a052a63d-fa53-44d5-a197-83089818a676"), null, "Rock", "Guns N Roses" },
                    { new Guid("cb554ed6-8fa7-4b8d-8d90-55cc6a3e0074"), null, "Disco", "ABBA" },
                    { new Guid("8e2f0a16-4c09-44c7-ba56-8dc62dfd792d"), null, "Alternative", "Oasis" },
                    { new Guid("cab51058-0996-4221-ba63-b841004e89dd"), null, "Pop", "A-ha" }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "ID", "BandID", "Description", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("dc4ccabe-29aa-42c4-9f80-18caea50adf5"), new Guid("6b1eea43-5597-45a6-bdea-e68c60564247"), "One of the best heavy metal albums ever", null, "Master Of Puppets" },
                    { new Guid("e5b6e8bf-5956-4329-a1b3-b1d48eea33ad"), new Guid("a052a63d-fa53-44d5-a197-83089818a676"), "Amazing Rock album with raw sound", null, "Appetite for Destruction" },
                    { new Guid("380c545c-9665-4043-baf2-34a3edefd373"), new Guid("cb554ed6-8fa7-4b8d-8d90-55cc6a3e0074"), "Very groovy album", null, "Waterloo" },
                    { new Guid("0e9a4ab5-4ae6-4ca3-ae7b-5f813e022527"), new Guid("8e2f0a16-4c09-44c7-ba56-8dc62dfd792d"), "Arguably the best albums by Oasis", null, "Be Here Now" },
                    { new Guid("8d2744ff-1134-4f36-a300-043febdc64b8"), new Guid("cab51058-0996-4221-ba63-b841004e89dd"), "Awesome Debut album by A-Ha", null, "Hunting Hight and Low" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_BandID",
                table: "Albums",
                column: "BandID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Bands");
        }
    }
}

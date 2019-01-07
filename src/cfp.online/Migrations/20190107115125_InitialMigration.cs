using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace cfp.online.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Poposals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConferenceName = table.Column<string>(nullable: false),
                    Website = table.Column<string>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Region = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Approved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poposals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Poposals");
        }
    }
}

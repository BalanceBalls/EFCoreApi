using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreApi.Migrations
{
    public partial class AddPairs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pairs",
                columns: table => new
                {
                    PairId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PairName = table.Column<string>(nullable: false),
                    BuyPrice = table.Column<decimal>(nullable: true),
                    SellPrice = table.Column<decimal>(nullable: true),
                    LastTradePrice = table.Column<decimal>(nullable: true),
                    HighPrice = table.Column<decimal>(nullable: true),
                    LowPrice = table.Column<decimal>(nullable: true),
                    Volume = table.Column<decimal>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pairs", x => x.PairId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pairs");
        }
    }
}

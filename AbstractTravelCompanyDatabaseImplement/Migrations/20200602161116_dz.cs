using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AbstractTravelCompanyDatabaseImplement.Migrations
{
    public partial class dz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourComponents_Tours_ComponentId",
                table: "TourComponents");

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreComponents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoreId = table.Column<int>(nullable: false),
                    ComponentId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreComponents_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreComponents_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourComponents_TourId",
                table: "TourComponents",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreComponents_ComponentId",
                table: "StoreComponents",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreComponents_StoreId",
                table: "StoreComponents",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourComponents_Tours_TourId",
                table: "TourComponents",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourComponents_Tours_TourId",
                table: "TourComponents");

            migrationBuilder.DropTable(
                name: "StoreComponents");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_TourComponents_TourId",
                table: "TourComponents");

            migrationBuilder.AddForeignKey(
                name: "FK_TourComponents_Tours_ComponentId",
                table: "TourComponents",
                column: "ComponentId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

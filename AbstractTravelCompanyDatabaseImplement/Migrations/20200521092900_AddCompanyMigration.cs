using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AbstractTravelCompanyDatabaseImplement.Migrations
{
    public partial class AddCompanyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Tours_OrderID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_TourComponents_Components_ComponentID",
                table: "TourComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_TourComponents_Tours_ComponentID",
                table: "TourComponents");

            migrationBuilder.DropIndex(
                name: "IX_TourComponents_ComponentID",
                table: "TourComponents");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ComponentID",
                table: "TourComponents");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Orders");

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
                name: "IX_TourComponents_ComponentId",
                table: "TourComponents",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_TourComponents_TourId",
                table: "TourComponents",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TourId",
                table: "Orders",
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
                name: "FK_Orders_Tours_TourId",
                table: "Orders",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourComponents_Components_ComponentId",
                table: "TourComponents",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Orders_Tours_TourId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_TourComponents_Components_ComponentId",
                table: "TourComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_TourComponents_Tours_TourId",
                table: "TourComponents");

            migrationBuilder.DropTable(
                name: "StoreComponents");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_TourComponents_ComponentId",
                table: "TourComponents");

            migrationBuilder.DropIndex(
                name: "IX_TourComponents_TourId",
                table: "TourComponents");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TourId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ComponentID",
                table: "TourComponents",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "Orders",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourComponents_ComponentID",
                table: "TourComponents",
                column: "ComponentID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderID",
                table: "Orders",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Tours_OrderID",
                table: "Orders",
                column: "OrderID",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TourComponents_Components_ComponentID",
                table: "TourComponents",
                column: "ComponentID",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TourComponents_Tours_ComponentID",
                table: "TourComponents",
                column: "ComponentID",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

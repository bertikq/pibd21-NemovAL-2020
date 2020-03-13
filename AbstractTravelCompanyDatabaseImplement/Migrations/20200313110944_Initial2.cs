using Microsoft.EntityFrameworkCore.Migrations;

namespace AbstractTravelCompanyDatabaseImplement.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_OrderID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponents_Components_ComponentID",
                table: "ProductComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponents_Products_ComponentID",
                table: "ProductComponents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductComponents",
                table: "ProductComponents");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Tours");

            migrationBuilder.RenameTable(
                name: "ProductComponents",
                newName: "TourComponents");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComponents_ComponentID",
                table: "TourComponents",
                newName: "IX_TourComponents_ComponentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tours",
                table: "Tours",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourComponents",
                table: "TourComponents",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tours",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourComponents",
                table: "TourComponents");

            migrationBuilder.RenameTable(
                name: "Tours",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "TourComponents",
                newName: "ProductComponents");

            migrationBuilder.RenameIndex(
                name: "IX_TourComponents_ComponentID",
                table: "ProductComponents",
                newName: "IX_ProductComponents_ComponentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductComponents",
                table: "ProductComponents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_OrderID",
                table: "Orders",
                column: "OrderID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponents_Components_ComponentID",
                table: "ProductComponents",
                column: "ComponentID",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponents_Products_ComponentID",
                table: "ProductComponents",
                column: "ComponentID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

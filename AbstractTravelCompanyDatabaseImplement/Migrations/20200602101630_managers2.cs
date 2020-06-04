using Microsoft.EntityFrameworkCore.Migrations;

namespace AbstractTravelCompanyDatabaseImplement.Migrations
{
    public partial class managers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourComponents_Tours_ComponentId",
                table: "TourComponents");

            migrationBuilder.CreateIndex(
                name: "IX_TourComponents_TourId",
                table: "TourComponents",
                column: "TourId");

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

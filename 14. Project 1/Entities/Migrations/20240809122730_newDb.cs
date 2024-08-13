using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class newDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TblPersons_CountryID",
                table: "TblPersons",
                column: "CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPersons_TblCountries_CountryID",
                table: "TblPersons",
                column: "CountryID",
                principalTable: "TblCountries",
                principalColumn: "CountryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblPersons_TblCountries_CountryID",
                table: "TblPersons");

            migrationBuilder.DropIndex(
                name: "IX_TblPersons_CountryID",
                table: "TblPersons");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class SP_AllPersons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string allPersons = @"
            CREATE PROCEDURE [dbo].[GetAllPersons]
            AS BEGIN
                SELECT PersonID, PersonName, Email, DateOfBirth, Gender, CountryID, ReceiveNewsLetters FROM [dbo].[TblPersons]
            END";

            migrationBuilder.Sql(allPersons);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string allPersons = @"
            DROP PROCEDURE [dbo].[GetAllPersons]";
            migrationBuilder.Sql(allPersons);
        }
    }
}

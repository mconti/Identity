using Microsoft.EntityFrameworkCore.Migrations;

namespace conti.maurizio.identity.Migrations
{
    public partial class aggiunteroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ee41a14-7ec6-4297-a030-58952d8b528d", "30bccd0f-a727-472c-827c-a1d1a47c71e8", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "467696ff-3966-4b0e-9124-1c52cc9c07ec", "031735fb-c26d-4321-a823-fc64f7462f00", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ee41a14-7ec6-4297-a030-58952d8b528d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "467696ff-3966-4b0e-9124-1c52cc9c07ec");
        }
    }
}

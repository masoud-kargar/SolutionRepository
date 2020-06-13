using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebRepository.Migrations
{
    public partial class ttes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("2e447743-4206-4e1a-8fff-7f4281708915"));

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Family", "Name" },
                values: new object[] { new Guid("63ca8342-403e-4ff3-8229-cb9984868712"), "Kargar", "Masoud" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("63ca8342-403e-4ff3-8229-cb9984868712"));

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Family", "Name" },
                values: new object[] { new Guid("2e447743-4206-4e1a-8fff-7f4281708915"), "Kargar", "Masoud" });
        }
    }
}

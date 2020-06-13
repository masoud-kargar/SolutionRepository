using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace WebRepository.Migrations {
    public partial class MigPerson : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    Family = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Family", "Name" },
                values: new object[] { new Guid("2079fe16-6d10-4ac7-b517-46cd5e11a102"), "Kargar", "Masoud" });
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}

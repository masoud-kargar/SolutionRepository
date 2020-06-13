using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebRepository.Migrations
{
    public partial class MigAddAl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("2079fe16-6d10-4ac7-b517-46cd5e11a102"));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    Icon = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogNews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    ImgName = table.Column<string>(maxLength: 100, nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    houseSize = table.Column<int>(nullable: false),
                    bedroomNumber = table.Column<byte>(nullable: false),
                    HomeMaxprice = table.Column<int>(nullable: false),
                    HomeWages = table.Column<int>(nullable: false),
                    bedWCNumber = table.Column<byte>(nullable: false),
                    ParkingNumber = table.Column<byte>(nullable: false),
                    Possibilities = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    MinDescription = table.Column<string>(maxLength: 1000, nullable: false),
                    Visit = table.Column<int>(nullable: false),
                    ShowSlider = table.Column<bool>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogNews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogNews_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 1000, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Website = table.Column<string>(maxLength: 100, nullable: true),
                    Comment = table.Column<string>(maxLength: 500, nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    GetBlogNewId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageComments_BlogNews_GetBlogNewId",
                        column: x => x.GetBlogNewId,
                        principalTable: "BlogNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Family", "Name" },
                values: new object[] { new Guid("8e989e15-8c47-4ed2-9c6b-f8df3b088483"), "Kargar", "Masoud" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogNews_CategoryId",
                table: "BlogNews",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PageComments_GetBlogNewId",
                table: "PageComments",
                column: "GetBlogNewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageComments");

            migrationBuilder.DropTable(
                name: "BlogNews");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("8e989e15-8c47-4ed2-9c6b-f8df3b088483"));

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Family", "Name" },
                values: new object[] { new Guid("2079fe16-6d10-4ac7-b517-46cd5e11a102"), "Kargar", "Masoud" });
        }
    }
}

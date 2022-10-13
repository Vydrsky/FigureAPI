using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Figure.DataAccess.Migrations
{
    public partial class AddFigureEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("21265c05-4300-4853-8789-6406c9e65469"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("3594e1c9-2674-4396-8fa0-4d32435a94d1"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("eb312f32-85fe-4f57-9ee3-1e6049fc0864"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("faaefd49-bf97-41c3-9931-93049b53665a"));

            migrationBuilder.CreateTable(
                name: "Figures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Figures", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ArchivedAt", "CreatedAt", "Description", "Email", "IsArchived", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("39f98726-b356-49e5-b286-d2e97b55e4e2"), null, new DateTime(2022, 10, 11, 20, 4, 1, 489, DateTimeKind.Local).AddTicks(3504), "description1", "adsfdfg", false, "test1", "9345435" },
                    { new Guid("cc654e95-324f-425b-854f-9727e1d7102a"), null, new DateTime(2022, 10, 11, 20, 4, 1, 489, DateTimeKind.Local).AddTicks(3511), "description1", "adsfdfg", false, "test1", "9345435" },
                    { new Guid("e588a894-ee46-4b73-821e-e78ef15ff4a7"), null, new DateTime(2022, 10, 11, 20, 4, 1, 489, DateTimeKind.Local).AddTicks(3508), "description1", "adsfdfg", false, "test1", "9345435" },
                    { new Guid("f30d244b-7451-4589-a2ef-d96513d0aa97"), null, new DateTime(2022, 10, 11, 20, 4, 1, 489, DateTimeKind.Local).AddTicks(3417), "description1", "adsfdfg", false, "test1", "9345435" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Figures");

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("39f98726-b356-49e5-b286-d2e97b55e4e2"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("cc654e95-324f-425b-854f-9727e1d7102a"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("e588a894-ee46-4b73-821e-e78ef15ff4a7"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f30d244b-7451-4589-a2ef-d96513d0aa97"));

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ArchivedAt", "CreatedAt", "Description", "Email", "IsArchived", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("21265c05-4300-4853-8789-6406c9e65469"), null, new DateTime(2022, 10, 4, 14, 39, 51, 350, DateTimeKind.Local).AddTicks(4868), "description1", "adsfdfg", false, "test1", "9345435" },
                    { new Guid("3594e1c9-2674-4396-8fa0-4d32435a94d1"), null, new DateTime(2022, 10, 4, 14, 39, 51, 350, DateTimeKind.Local).AddTicks(4872), "description1", "adsfdfg", false, "test1", "9345435" },
                    { new Guid("eb312f32-85fe-4f57-9ee3-1e6049fc0864"), null, new DateTime(2022, 10, 4, 14, 39, 51, 350, DateTimeKind.Local).AddTicks(4865), "description1", "adsfdfg", false, "test1", "9345435" },
                    { new Guid("faaefd49-bf97-41c3-9931-93049b53665a"), null, new DateTime(2022, 10, 4, 14, 39, 51, 350, DateTimeKind.Local).AddTicks(4822), "description1", "adsfdfg", false, "test1", "9345435" }
                });
        }
    }
}

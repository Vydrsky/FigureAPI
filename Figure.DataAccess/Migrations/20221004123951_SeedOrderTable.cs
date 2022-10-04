using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Figure.DataAccess.Migrations
{
    public partial class SeedOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Figure.DataAccess.Migrations
{
    public partial class OrderExtendedWithSurname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ArchivedAt", "CreatedAt", "Description", "Email", "IsArchived", "Name", "PhoneNumber", "Surname" },
                values: new object[,]
                {
                    { new Guid("2ba4a1eb-2bcc-4c22-a168-bbff52abbe22"), null, new DateTime(2022, 10, 15, 16, 4, 40, 831, DateTimeKind.Local).AddTicks(4470), "description1", "adsfdfg", false, "test1", "9345435", "test1" },
                    { new Guid("5a4abdd1-f058-4c5c-9b3c-c32382b2779d"), null, new DateTime(2022, 10, 15, 16, 4, 40, 831, DateTimeKind.Local).AddTicks(4654), "description1", "adsfdfg", false, "test1", "9345435", "test1" },
                    { new Guid("7c377932-6aff-4013-aa50-fd8348d374ac"), null, new DateTime(2022, 10, 15, 16, 4, 40, 831, DateTimeKind.Local).AddTicks(4661), "description1", "adsfdfg", false, "test1", "9345435", "test1" },
                    { new Guid("d841c21b-7f03-4395-bb1d-2146328ba025"), null, new DateTime(2022, 10, 15, 16, 4, 40, 831, DateTimeKind.Local).AddTicks(4657), "description1", "adsfdfg", false, "test1", "9345435", "test1" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("2ba4a1eb-2bcc-4c22-a168-bbff52abbe22"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("5a4abdd1-f058-4c5c-9b3c-c32382b2779d"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("7c377932-6aff-4013-aa50-fd8348d374ac"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("d841c21b-7f03-4395-bb1d-2146328ba025"));

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

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
    }
}

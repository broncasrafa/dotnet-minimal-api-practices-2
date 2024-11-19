using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Student.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Seed_Datas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7af0054e-c8a1-4724-8f21-e9b4bf14b10b", null, "User", "USER" },
                    { "fb512387-6e99-4c02-9aed-44e8daaec17e", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Course",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Credits", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 19, 20, 45, 32, 774, DateTimeKind.Utc).AddTicks(5941), null, 3, "Minimal API Development", null, null },
                    { 2, new DateTime(2024, 11, 19, 20, 45, 32, 774, DateTimeKind.Utc).AddTicks(6624), null, 5, "Spring Boot Development", null, null },
                    { 3, new DateTime(2024, 11, 19, 20, 45, 32, 774, DateTimeKind.Utc).AddTicks(6626), null, 4, "Ultimate .NET API Development", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7af0054e-c8a1-4724-8f21-e9b4bf14b10b");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb512387-6e99-4c02-9aed-44e8daaec17e");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Course",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Course",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Course",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

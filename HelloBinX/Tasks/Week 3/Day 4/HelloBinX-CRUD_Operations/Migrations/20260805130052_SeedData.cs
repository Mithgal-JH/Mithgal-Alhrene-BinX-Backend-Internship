using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HelloBinX_EFCore.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "AuthorName", "Bio" },
                values: new object[,]
                {
                    { 1, "Robert Martin", "Software Engineer and Author" },
                    { 2, "Martin Fowler", "Software Architect" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "BookName", "Price", "PublishedYear" },
                values: new object[,]
                {
                    { 1, 1, "Clean Code", 50m, 2008 },
                    { 2, 2, "Refactoring", 70m, 2018 }
                });

            migrationBuilder.InsertData(
                table: "PhoneNumbers",
                columns: new[] { "PhoneNumberId", "AuthorId", "Phone" },
                values: new object[,]
                {
                    { 1, 1, "0599000001" },
                    { 2, 2, "0599000002" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "BookId", "Comment", "CreatedAt", "Rating" },
                values: new object[,]
                {
                    { 1, 1, "Excellent book", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)5 },
                    { 2, 2, "Very useful", new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PhoneNumbers",
                keyColumn: "PhoneNumberId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PhoneNumbers",
                keyColumn: "PhoneNumberId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);
        }
    }
}

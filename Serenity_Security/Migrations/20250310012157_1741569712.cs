using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serenity_Security.Migrations
{
    /// <inheritdoc />
    public partial class _1741569712 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ef28448-0f98-40fc-864d-a9a11186b9ca", "AQAAAAIAAYagAAAAEAgNov5+p4BWBLjlDSBIwaELD3MIG2jIktKA7cYjwD2Gj2KCIQh2J3u+lg2MbWj2cw==", "6e88d516-2c38-4d28-9310-c752cdce2274" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d78f3520-9583-4fbc-8529-6bffc1568c1b", "AQAAAAIAAYagAAAAED/YbUGLKHnOYQqy/cB8BI6Ypjpz9i+uTJoiot/ctzhpBeZnSblPaocqIfUxABBw/Q==", "4908f64d-fbb3-4bf7-a03b-4f49e1d9b743" });
        }
    }
}

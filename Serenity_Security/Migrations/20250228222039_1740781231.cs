using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serenity_Security.Migrations
{
    /// <inheritdoc />
    public partial class _1740781231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Reports",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bec914a2-165c-4a49-b327-23504e122f28", "AQAAAAIAAYagAAAAEIDqLhwkc54VPWFpiKapKB+8VTNjidjWJI76Z2h4/ywOLUj32FyMIFCDfnopKxStSA==", "ddca9444-322e-4e9a-b109-49a447e43bb7" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsCompleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsCompleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsCompleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Reports");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82323fa4-7706-470f-aa81-1567fb843336", "AQAAAAIAAYagAAAAELj45FLrJivoVNmRd0iehb47u9MQvjnQX6q0KG4AAbu4PsF9gR8CKAyMuY/azV/Gkg==", "fc72b376-4e13-44cd-a724-6098d58bdcda" });
        }
    }
}

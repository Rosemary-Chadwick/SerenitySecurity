using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serenity_Security.Migrations
{
    /// <inheritdoc />
    public partial class newmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96d99113-4b73-4333-81d1-f1bf9392f54d", "AQAAAAIAAYagAAAAENDiM5ymu/DoLlSZTdSQztPdRgO9ocoRsE/jp1x004CialU9WwQkyw/vfayGGq/QSA==", "20212b71-ab3f-4ee2-b646-52f5abe2184f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bec914a2-165c-4a49-b327-23504e122f28", "AQAAAAIAAYagAAAAEIDqLhwkc54VPWFpiKapKB+8VTNjidjWJI76Z2h4/ywOLUj32FyMIFCDfnopKxStSA==", "ddca9444-322e-4e9a-b109-49a447e43bb7" });
        }
    }
}

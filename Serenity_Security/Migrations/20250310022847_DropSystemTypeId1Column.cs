using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serenity_Security.Migrations
{
    /// <inheritdoc />
    public partial class DropSystemTypeId1Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "SystemTypeId1", table: "Assets");

            // Keep the existing user updates if needed
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[]
                {
                    "9ec20efc-e53e-4e3c-8475-ca91cad36757",
                    "AQAAAAIAAYagAAAAEAPzfPo6DJvqX1X+s9sOwqLXUQqMfs4RLCLcOy905UNCzb8QxWtkVuYRC811cGYJ6Q==",
                    "2427e465-8901-4476-9a59-9b33bda6e396",
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(name: "SystemTypeId1", table: "Assets", nullable: true);

            // Keep the existing user updates
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[]
                {
                    "5ef28448-0f98-40fc-864d-a9a11186b9ca",
                    "AQAAAAIAAYagAAAAEAgNov5+p4BWBLjlDSBIwaELD3MIG2jIktKA7cYjwD2Gj2KCIQh2J3u+lg2MbWj2cw==",
                    "6e88d516-2c38-4d28-9310-c752cdce2274",
                }
            );
        }
    }
}

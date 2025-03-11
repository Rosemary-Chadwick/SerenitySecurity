using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serenity_Security.Migrations
{
    /// <inheritdoc />
    public partial class AddReferencesColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "References",
                table: "Vulnerabilities",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d78f3520-9583-4fbc-8529-6bffc1568c1b", "AQAAAAIAAYagAAAAED/YbUGLKHnOYQqy/cB8BI6Ypjpz9i+uTJoiot/ctzhpBeZnSblPaocqIfUxABBw/Q==", "4908f64d-fbb3-4bf7-a03b-4f49e1d9b743" });

            migrationBuilder.UpdateData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 3,
                column: "VerificationSteps",
                value: "1. Install the latest macOS security update via System Preferences > Software Update\r\n        2. Verify the installed version in \"About This Mac\"\r\n        3. Restart the system completely\r\n        4. Check system logs for WindowServer errors: log show --predicate \"process == \\\"WindowServer\\\"\" --last 1h\r\n        5. Run Apple Diagnostics by restarting and holding D during startup\r\n        6. Verify fix by checking for advisory reference in installed security updates\r\n        7. Monitor system stability and report any graphical glitches or crashes");

            migrationBuilder.UpdateData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 4,
                column: "VerificationSteps",
                value: "1. Install the latest Windows security update via Windows Update\r\n        2. Verify the installation of KB5034202 or newer in Update History\r\n        3. Restart the system completely\r\n        4. Check Event Viewer for Hyper-V-related errors\r\n        5. Run PowerShell command: Get-HotFix -Id KB5034202\r\n        6. For Hyper-V servers, ensure all VMs are updated and running with secure configurations\r\n        7. Test by running: Get-VMSecurity on all virtual machines to verify security settings\r\n        8. Apply latest firmware and driver updates for virtualization hardware");

            migrationBuilder.UpdateData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 6,
                column: "VerificationSteps",
                value: "1. Update DD-WRT firmware to r48000 or later through the web interface\r\n        2. Verify the installed version in the router admin panel under Status > Router\r\n        3. After update, perform a hard reset (30-30-30 reset) to ensure clean configuration\r\n        4. Secure the admin interface with a strong password\r\n        5. Disable remote administration if not needed\r\n        6. Change default SSH and admin ports\r\n        7. Verify proper input sanitization by testing forms with special characters\r\n        8. Enable logging and monitor for suspicious activities");

            migrationBuilder.UpdateData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 9,
                column: "VerificationSteps",
                value: "1. Update all web browsers to their latest versions:\r\n           - Chrome 116.0.5845.187 or later\r\n           - Firefox 117.0.1 or later\r\n           - Safari 16.6 or later\r\n        2. Verify browser versions through their respective About pages\r\n        3. Enable automatic updates for all browsers\r\n        4. Update operating system with latest security patches\r\n        5. Scan for other applications using WebP libraries (image editors, media players)\r\n        6. Update vulnerable applications through their official channels\r\n        7. Consider using browser extensions that block untrusted image loading\r\n        8. Monitor security advisories for related vulnerabilities");

            migrationBuilder.UpdateData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "References",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "References",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "References",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "References",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 5,
                column: "References",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 6,
                column: "References",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 7,
                column: "References",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 8,
                column: "References",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 9,
                column: "References",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "References",
                table: "Vulnerabilities");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96d99113-4b73-4333-81d1-f1bf9392f54d", "AQAAAAIAAYagAAAAENDiM5ymu/DoLlSZTdSQztPdRgO9ocoRsE/jp1x004CialU9WwQkyw/vfayGGq/QSA==", "20212b71-ab3f-4ee2-b646-52f5abe2184f" });

            migrationBuilder.UpdateData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 3,
                column: "VerificationSteps",
                value: "1. Install the latest macOS security update via System Preferences > Software Update\n        2. Verify the installed version in \"About This Mac\"\n        3. Restart the system completely\n        4. Check system logs for WindowServer errors: log show --predicate \"process == \\\"WindowServer\\\"\" --last 1h\n        5. Run Apple Diagnostics by restarting and holding D during startup\n        6. Verify fix by checking for advisory reference in installed security updates\n        7. Monitor system stability and report any graphical glitches or crashes");

            migrationBuilder.UpdateData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 4,
                column: "VerificationSteps",
                value: "1. Install the latest Windows security update via Windows Update\n        2. Verify the installation of KB5034202 or newer in Update History\n        3. Restart the system completely\n        4. Check Event Viewer for Hyper-V-related errors\n        5. Run PowerShell command: Get-HotFix -Id KB5034202\n        6. For Hyper-V servers, ensure all VMs are updated and running with secure configurations\n        7. Test by running: Get-VMSecurity on all virtual machines to verify security settings\n        8. Apply latest firmware and driver updates for virtualization hardware");

            migrationBuilder.UpdateData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 6,
                column: "VerificationSteps",
                value: "1. Update DD-WRT firmware to r48000 or later through the web interface\n        2. Verify the installed version in the router admin panel under Status > Router\n        3. After update, perform a hard reset (30-30-30 reset) to ensure clean configuration\n        4. Secure the admin interface with a strong password\n        5. Disable remote administration if not needed\n        6. Change default SSH and admin ports\n        7. Verify proper input sanitization by testing forms with special characters\n        8. Enable logging and monitor for suspicious activities");

            migrationBuilder.UpdateData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 9,
                column: "VerificationSteps",
                value: "1. Update all web browsers to their latest versions:\n           - Chrome 116.0.5845.187 or later\n           - Firefox 117.0.1 or later\n           - Safari 16.6 or later\n        2. Verify browser versions through their respective About pages\n        3. Enable automatic updates for all browsers\n        4. Update operating system with latest security patches\n        5. Scan for other applications using WebP libraries (image editors, media players)\n        6. Update vulnerable applications through their official channels\n        7. Consider using browser extensions that block untrusted image loading\n        8. Monitor security advisories for related vulnerabilities");
        }
    }
}

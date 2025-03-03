using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Serenity_Security.Migrations
{
    /// <inheritdoc />
    public partial class _1740756810 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_SystemType_SystemTypeId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_UserProfiles_UserId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_Asset_AssetId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportVulnerability_Report_ReportId",
                table: "ReportVulnerability");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportVulnerability_Vulnerability_VulnerabilityId",
                table: "ReportVulnerability");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_IdentityUserId",
                table: "UserProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vulnerability",
                table: "Vulnerability");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemType",
                table: "SystemType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportVulnerability",
                table: "ReportVulnerability");

            migrationBuilder.DropIndex(
                name: "IX_ReportVulnerability_ReportId",
                table: "ReportVulnerability");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Report",
                table: "Report");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asset",
                table: "Asset");

            migrationBuilder.RenameTable(
                name: "Vulnerability",
                newName: "Vulnerabilities");

            migrationBuilder.RenameTable(
                name: "SystemType",
                newName: "SystemTypes");

            migrationBuilder.RenameTable(
                name: "ReportVulnerability",
                newName: "ReportVulnerabilities");

            migrationBuilder.RenameTable(
                name: "Report",
                newName: "Reports");

            migrationBuilder.RenameTable(
                name: "Asset",
                newName: "Assets");

            migrationBuilder.RenameIndex(
                name: "IX_ReportVulnerability_VulnerabilityId",
                table: "ReportVulnerabilities",
                newName: "IX_ReportVulnerabilities_VulnerabilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_AssetId",
                table: "Reports",
                newName: "IX_Reports_AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Asset_UserId",
                table: "Assets",
                newName: "IX_Assets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Asset_SystemTypeId",
                table: "Assets",
                newName: "IX_Assets_SystemTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ReportVulnerabilities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "SystemTypeId1",
                table: "Assets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vulnerabilities",
                table: "Vulnerabilities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemTypes",
                table: "SystemTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportVulnerabilities",
                table: "ReportVulnerabilities",
                columns: new[] { "ReportId", "VulnerabilityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RemediationChecklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FixedVersion = table.Column<string>(type: "text", nullable: true),
                    VerificationSteps = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    VulnerabilityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemediationChecklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemediationChecklists_Vulnerabilities_VulnerabilityId",
                        column: x => x.VulnerabilityId,
                        principalTable: "Vulnerabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                column: "NormalizedName",
                value: "ADMIN");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3aabc98-e3cb-5b64-b632-5ffa72b70b46", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "a6dd741f-b24f-4dad-9dd6-06de30b1e97e", "Rosmary.Chadwick@proton.me", "AQAAAAIAAYagAAAAEPylsbJ7ZFWRGo30iW0ygCdaMtJN03dnRdnTLZPtzj1YBfVDLSvojae5IRDdJ5a/kQ==", "52180a05-4961-44fa-afb2-85b659bdb1ad", "Rosemary" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7eb05375-f2a3-4ecf-92b5-4dbd11831839", 0, "7419d6a6-1423-4728-b496-f2ade613ff98", "jason.turner@example.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEOOVZ60P/OEqkihliLjCJs/kftXR2ZsQc8qgw/U5jNjTZ1z0fySj/k59+PSm3l1Gfw==", null, false, "87334d95-529e-4393-8aaa-16978b2e1753", false, "JasonT" },
                    { "9f86ba56-ea82-4b9a-b593-7878b5d8916e", 0, "48b4cefd-7a06-4adc-977a-2a6f055aae01", "samantha.brooks@example.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEKDUZmY9gNw0nyB5zxziUURZn5AAu16xjFogawwTLjUpynoIeC8FZqO51LJgWPiWvQ==", null, false, "d3d3d967-6707-4bbe-8446-1e2234815512", false, "SamanthaB" }
                });

            migrationBuilder.InsertData(
                table: "SystemTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Desktop Computer" },
                    { 2, "Laptop" },
                    { 3, "Home Router" },
                    { 4, "Smart Home Hub" },
                    { 5, "General Purpose" }
                });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "FirstName", "IsAdmin", "LastName", "Username" },
                values: new object[] { new DateTime(2024, 1, 26, 9, 14, 32, 0, DateTimeKind.Unspecified), "Rosmary.Chadwick@proton.me", "Rosemary", true, "Chadwick", "Rosemary" });

            migrationBuilder.InsertData(
                table: "Vulnerabilities",
                columns: new[] { "Id", "CveId", "CvsScore", "Description", "PublishedAt", "SeverityLevel" },
                values: new object[,]
                {
                    { 1, "CVE-2023-38408", 8.8m, "Deserialization of Untrusted Data vulnerability in Apache Pulsar allows an attacker with admin access to successfully run remote code. This issue affects Apache Pulsar versions prior to 2.10.5, versions prior to 2.11.2, and versions prior to 3.0.1.", new DateTime(2024, 1, 15, 14, 30, 22, 0, DateTimeKind.Unspecified), "HIGH" },
                    { 2, "CVE-2023-50164", 5.3m, "A vulnerability in OpenSSH server could allow a remote attacker to discover valid usernames due to differences in authentication failures when using public key authentication. This vulnerability affects OpenSSH versions before 9.6.", new DateTime(2024, 1, 18, 10, 45, 15, 0, DateTimeKind.Unspecified), "MEDIUM" },
                    { 3, "CVE-2023-29491", 7.8m, "A security vulnerability in macOS could allow local attackers to escalate privileges by exploiting a memory corruption issue in the WindowServer component.", new DateTime(2024, 1, 20, 9, 22, 18, 0, DateTimeKind.Unspecified), "HIGH" },
                    { 4, "CVE-2024-21626", 8.2m, "A potential vulnerability in Microsoft Windows Hyper-V could allow an attacker to escape from a guest virtual machine to the host, potentially leading to escalation of privilege.", new DateTime(2024, 1, 22, 16, 10, 34, 0, DateTimeKind.Unspecified), "HIGH" },
                    { 5, "CVE-2023-22527", 6.5m, "When handling a malicious JNDI URI, Redis may perform an unintended request to an external server. This issue affects Redis versions before 6.2.14, 7.0.x before 7.0.15, and 7.2.x before 7.2.4.", new DateTime(2024, 1, 25, 11, 30, 45, 0, DateTimeKind.Unspecified), "MEDIUM" },
                    { 6, "CVE-2023-50868", 7.2m, "A vulnerability in DD-WRT allows attackers to execute unauthorized commands via the web interface due to insufficient input validation.", new DateTime(2024, 1, 27, 8, 15, 30, 0, DateTimeKind.Unspecified), "HIGH" },
                    { 7, "CVE-2023-0297", 5.4m, "Home Assistant Core has a cross-site scripting vulnerability in the Logbook component where user-controlled input was not properly sanitized.", new DateTime(2024, 1, 28, 14, 40, 22, 0, DateTimeKind.Unspecified), "MEDIUM" },
                    { 8, "CVE-2023-51767", 7.0m, "A race condition in the Linux kernel's IPv6 subsystem could lead to memory corruption and potential local privilege escalation.", new DateTime(2024, 1, 30, 10, 25, 18, 0, DateTimeKind.Unspecified), "HIGH" },
                    { 9, "CVE-2023-4863", 8.8m, "Heap buffer overflow in WebP in Google Chrome and other applications could allow a remote attacker to potentially exploit heap corruption via a crafted HTML page.", new DateTime(2024, 1, 31, 9, 15, 27, 0, DateTimeKind.Unspecified), "HIGH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d3aabc98-e3cb-5b64-b632-5ffa72b70b46", "7eb05375-f2a3-4ecf-92b5-4dbd11831839" },
                    { "d3aabc98-e3cb-5b64-b632-5ffa72b70b46", "9f86ba56-ea82-4b9a-b593-7878b5d8916e" }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "CreatedAt", "IpAddress", "IsActive", "OsVersion", "SystemName", "SystemTypeId", "SystemTypeId1", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 28, 10, 15, 22, 0, DateTimeKind.Unspecified), "192.168.1.100", true, "Windows 11 Pro 22H2", "Primary-Workstation", 1, null, 1 },
                    { 2, new DateTime(2024, 1, 28, 10, 30, 45, 0, DateTimeKind.Unspecified), "192.168.1.101", true, "macOS Ventura 13.5", "DevLaptop", 2, null, 1 },
                    { 3, new DateTime(2024, 1, 28, 11, 5, 12, 0, DateTimeKind.Unspecified), "192.168.1.1", true, "DD-WRT v3.0-r47479", "HomeRouter", 3, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "RemediationChecklists",
                columns: new[] { "Id", "CreatedAt", "Description", "FixedVersion", "IsCompleted", "VerificationSteps", "VulnerabilityId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 1, 9, 40, 22, 0, DateTimeKind.Unspecified), "This vulnerability allows attackers with admin access to execute arbitrary code through deserialization of untrusted data. Update to a patched version and implement proper input validation and authentication controls.", "Apache Pulsar 2.10.5, 2.11.2, or 3.0.1+", false, "1. Update Apache Pulsar to version 2.10.5, 2.11.2, or 3.0.1 or higher\n        2. Verify the installed version using: bin/pulsar version\n        3. Check configuration files for unsafe deserialization settings\n        4. Restart the Pulsar service: systemctl restart pulsar\n        5. Verify service is running with: systemctl status pulsar\n        6. Test the service by running a secure client connection and attempting to publish/consume messages\n        7. Review logs for any deserialization warnings or errors: grep -r \"deserialization\" /var/log/pulsar/", 1 },
                    { 2, new DateTime(2024, 2, 2, 15, 55, 33, 0, DateTimeKind.Unspecified), "This vulnerability allows attackers to enumerate valid usernames by measuring timing differences during authentication failures. Update to the latest version and configure sshd to use consistent timing for all authentication responses.", "OpenSSH 9.6+", false, "1. Update OpenSSH to version 9.6 or later\n        2. Check the installed version using: ssh -V\n        3. Edit /etc/ssh/sshd_config to add: UseDNS no\n        4. Set appropriate authentication attempt limits with: MaxAuthTries 4\n        5. Restart the SSH service: systemctl restart sshd\n        6. Verify timing consistency with multiple login attempts using different usernames\n        7. Check authentication logs for patterns: grep \"authentication failure\" /var/log/auth.log", 2 },
                    { 3, new DateTime(2024, 2, 1, 10, 55, 45, 0, DateTimeKind.Unspecified), "This vulnerability in macOS WindowServer component could allow local attackers to elevate privileges through memory corruption. Install all available security updates and limit access to privileged accounts.", "macOS Ventura 13.5+, Sonoma 14.2+", false, "1. Install the latest macOS security update via System Preferences > Software Update\n        2. Verify the installed version in \"About This Mac\"\n        3. Restart the system completely\n        4. Check system logs for WindowServer errors: log show --predicate \"process == \\\"WindowServer\\\"\" --last 1h\n        5. Run Apple Diagnostics by restarting and holding D during startup\n        6. Verify fix by checking for advisory reference in installed security updates\n        7. Monitor system stability and report any graphical glitches or crashes", 3 },
                    { 4, new DateTime(2024, 2, 1, 9, 42, 33, 0, DateTimeKind.Unspecified), "This vulnerability could allow guest-to-host virtual machine escapes in Hyper-V, leading to privilege escalation. Apply all security updates and consider disabling Hyper-V if not needed.", "Latest Windows Security Update KB5034202", false, "1. Install the latest Windows security update via Windows Update\n        2. Verify the installation of KB5034202 or newer in Update History\n        3. Restart the system completely\n        4. Check Event Viewer for Hyper-V-related errors\n        5. Run PowerShell command: Get-HotFix -Id KB5034202\n        6. For Hyper-V servers, ensure all VMs are updated and running with secure configurations\n        7. Test by running: Get-VMSecurity on all virtual machines to verify security settings\n        8. Apply latest firmware and driver updates for virtualization hardware", 4 },
                    { 5, new DateTime(2024, 2, 3, 11, 35, 22, 0, DateTimeKind.Unspecified), "This vulnerability allows attackers to force Redis to perform unintended external server requests through malicious JNDI URIs. Update to a patched version and ensure Redis is not exposed to untrusted networks.", "Redis 6.2.14, 7.0.15, or 7.2.4+", false, "1. Update Redis to version 6.2.14, 7.0.15, or 7.2.4 or higher\n        2. Verify the installed version using: redis-cli info server | grep redis_version\n        3. Check and modify Redis configuration to disable or restrict JNDI URI handling\n        4. Restart the Redis service: systemctl restart redis\n        5. Verify service is running with: systemctl status redis\n        6. Test with a monitoring tool to ensure JNDI requests are properly handled\n        7. Review Redis logs for suspicious connection attempts: grep -r \"jndi\" /var/log/redis/", 5 },
                    { 6, new DateTime(2024, 2, 1, 11, 25, 45, 0, DateTimeKind.Unspecified), "This vulnerability allows remote attackers to execute unauthorized commands via the web interface due to insufficient input validation. Update firmware immediately and restrict administration access.", "DD-WRT r48000+", false, "1. Update DD-WRT firmware to r48000 or later through the web interface\n        2. Verify the installed version in the router admin panel under Status > Router\n        3. After update, perform a hard reset (30-30-30 reset) to ensure clean configuration\n        4. Secure the admin interface with a strong password\n        5. Disable remote administration if not needed\n        6. Change default SSH and admin ports\n        7. Verify proper input sanitization by testing forms with special characters\n        8. Enable logging and monitor for suspicious activities", 6 },
                    { 7, new DateTime(2024, 2, 2, 16, 20, 33, 0, DateTimeKind.Unspecified), "This cross-site scripting vulnerability in Home Assistant Core's Logbook component could allow attackers to inject malicious scripts. Update to a patched version and use proper input sanitization.", "Home Assistant Core 2023.2.0+", false, "1. Update Home Assistant Core to version 2023.2.0 or later\n        2. Verify the installed version on the About page in Home Assistant\n        3. Restart the Home Assistant service\n        4. Check logs for any XSS-related warnings or errors\n        5. Clear browser cache and cookies after updating\n        6. Test the Logbook component with properly sanitized inputs\n        7. Consider implementing Content Security Policy (CSP) headers for additional protection\n        8. Periodically check for updates and security advisories", 7 },
                    { 8, new DateTime(2024, 2, 2, 15, 57, 45, 0, DateTimeKind.Unspecified), "This race condition in the Linux kernel's IPv6 subsystem could lead to memory corruption and local privilege escalation. Apply kernel updates as soon as possible and limit access to local user accounts.", "Kernel 6.1.67, 6.6.8, or distribution-specific patches", false, "1. Update the Linux kernel to version 6.1.67, 6.6.8 or later using the package manager\n        2. Verify the installed kernel version with: uname -r\n        3. Apply all pending security updates: apt update && apt upgrade -y\n        4. Reboot the system to apply the kernel updates\n        5. Check system logs for IPv6-related errors: grep -i ipv6 /var/log/syslog\n        6. Consider temporarily disabling IPv6 if updates cannot be applied immediately:\n           echo \"net.ipv6.conf.all.disable_ipv6 = 1\" >> /etc/sysctl.conf && sysctl -p\n        7. Test IPv6 functionality after updates to ensure proper operation\n        8. Monitor system for unusual behavior or performance issues", 8 },
                    { 9, new DateTime(2024, 2, 1, 10, 57, 33, 0, DateTimeKind.Unspecified), "This heap buffer overflow vulnerability in WebP handling could allow remote attackers to execute arbitrary code or cause denial of service by crafting malicious WebP images. Update all browsers and applications that process WebP images.", "Chrome 116.0.5845.187+, Firefox 117.0.1+, Safari 16.6+", false, "1. Update all web browsers to their latest versions:\n           - Chrome 116.0.5845.187 or later\n           - Firefox 117.0.1 or later\n           - Safari 16.6 or later\n        2. Verify browser versions through their respective About pages\n        3. Enable automatic updates for all browsers\n        4. Update operating system with latest security patches\n        5. Scan for other applications using WebP libraries (image editors, media players)\n        6. Update vulnerable applications through their official channels\n        7. Consider using browser extensions that block untrusted image loading\n        8. Monitor security advisories for related vulnerabilities", 9 }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IdentityUserId", "IsAdmin", "LastName", "Username" },
                values: new object[,]
                {
                    { 2, new DateTime(2024, 1, 28, 14, 22, 45, 0, DateTimeKind.Unspecified), "jason.turner@example.com", "Jason", "7eb05375-f2a3-4ecf-92b5-4dbd11831839", false, "Turner", "JasonT" },
                    { 3, new DateTime(2024, 1, 30, 11, 8, 17, 0, DateTimeKind.Unspecified), "samantha.brooks@example.com", "Samantha", "9f86ba56-ea82-4b9a-b593-7878b5d8916e", false, "Brooks", "SamanthaB" }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "CreatedAt", "IpAddress", "IsActive", "OsVersion", "SystemName", "SystemTypeId", "SystemTypeId1", "UserId" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 1, 29, 14, 22, 45, 0, DateTimeKind.Unspecified), "192.168.2.100", true, "Windows 10 Home 21H2", "Gaming-PC", 1, null, 2 },
                    { 5, new DateTime(2024, 1, 29, 15, 45, 33, 0, DateTimeKind.Unspecified), "192.168.2.101", true, "Ubuntu 22.04 LTS", "Work-Laptop", 2, null, 2 },
                    { 6, new DateTime(2024, 1, 29, 16, 10, 27, 0, DateTimeKind.Unspecified), "192.168.2.150", true, "HomeOS 4.2", "SmartHome-Hub", 4, null, 2 },
                    { 7, new DateTime(2024, 1, 31, 9, 45, 12, 0, DateTimeKind.Unspecified), "192.168.3.100", true, "macOS Sonoma 14.2", "Personal-Laptop", 2, null, 3 },
                    { 8, new DateTime(2024, 1, 31, 10, 22, 33, 0, DateTimeKind.Unspecified), "192.168.3.101", true, "Windows 11 Home 23H2", "Home-Desktop", 1, null, 3 },
                    { 9, new DateTime(2024, 1, 31, 11, 15, 27, 0, DateTimeKind.Unspecified), "192.168.3.150", true, "Debian 12", "MediaServer", 5, null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "AssetId", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 2, 1, 9, 30, 12, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2024, 2, 1, 10, 45, 22, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new DateTime(2024, 2, 1, 11, 15, 33, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ReportVulnerabilities",
                columns: new[] { "ReportId", "VulnerabilityId", "DiscoveredAt", "Id" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2024, 2, 1, 9, 35, 22, 0, DateTimeKind.Unspecified), 0 },
                    { 2, 3, new DateTime(2024, 2, 1, 10, 50, 33, 0, DateTimeKind.Unspecified), 0 },
                    { 2, 9, new DateTime(2024, 2, 1, 10, 52, 15, 0, DateTimeKind.Unspecified), 0 },
                    { 3, 6, new DateTime(2024, 2, 1, 11, 20, 45, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "AssetId", "CreatedAt" },
                values: new object[,]
                {
                    { 4, 4, new DateTime(2024, 2, 2, 14, 22, 30, 0, DateTimeKind.Unspecified) },
                    { 5, 5, new DateTime(2024, 2, 2, 15, 45, 12, 0, DateTimeKind.Unspecified) },
                    { 6, 6, new DateTime(2024, 2, 2, 16, 10, 45, 0, DateTimeKind.Unspecified) },
                    { 7, 7, new DateTime(2024, 2, 3, 9, 15, 22, 0, DateTimeKind.Unspecified) },
                    { 8, 8, new DateTime(2024, 2, 3, 10, 30, 45, 0, DateTimeKind.Unspecified) },
                    { 9, 9, new DateTime(2024, 2, 3, 11, 25, 18, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ReportVulnerabilities",
                columns: new[] { "ReportId", "VulnerabilityId", "DiscoveredAt", "Id" },
                values: new object[,]
                {
                    { 4, 4, new DateTime(2024, 2, 2, 14, 25, 33, 0, DateTimeKind.Unspecified), 0 },
                    { 4, 9, new DateTime(2024, 2, 2, 14, 27, 45, 0, DateTimeKind.Unspecified), 0 },
                    { 5, 2, new DateTime(2024, 2, 2, 15, 50, 22, 0, DateTimeKind.Unspecified), 0 },
                    { 5, 8, new DateTime(2024, 2, 2, 15, 52, 33, 0, DateTimeKind.Unspecified), 0 },
                    { 6, 7, new DateTime(2024, 2, 2, 16, 15, 45, 0, DateTimeKind.Unspecified), 0 },
                    { 7, 3, new DateTime(2024, 2, 3, 9, 20, 33, 0, DateTimeKind.Unspecified), 0 },
                    { 7, 9, new DateTime(2024, 2, 3, 9, 22, 45, 0, DateTimeKind.Unspecified), 0 },
                    { 8, 4, new DateTime(2024, 2, 3, 10, 35, 22, 0, DateTimeKind.Unspecified), 0 },
                    { 9, 5, new DateTime(2024, 2, 3, 11, 30, 45, 0, DateTimeKind.Unspecified), 0 },
                    { 9, 8, new DateTime(2024, 2, 3, 11, 32, 33, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_IdentityUserId",
                table: "UserProfiles",
                column: "IdentityUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_SystemTypeId1",
                table: "Assets",
                column: "SystemTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_RemediationChecklists_VulnerabilityId",
                table: "RemediationChecklists",
                column: "VulnerabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_SystemTypes_SystemTypeId",
                table: "Assets",
                column: "SystemTypeId",
                principalTable: "SystemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_SystemTypes_SystemTypeId1",
                table: "Assets",
                column: "SystemTypeId1",
                principalTable: "SystemTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_UserProfiles_UserId",
                table: "Assets",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Assets_AssetId",
                table: "Reports",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportVulnerabilities_Reports_ReportId",
                table: "ReportVulnerabilities",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportVulnerabilities_Vulnerabilities_VulnerabilityId",
                table: "ReportVulnerabilities",
                column: "VulnerabilityId",
                principalTable: "Vulnerabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_SystemTypes_SystemTypeId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_SystemTypes_SystemTypeId1",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_UserProfiles_UserId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Assets_AssetId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportVulnerabilities_Reports_ReportId",
                table: "ReportVulnerabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportVulnerabilities_Vulnerabilities_VulnerabilityId",
                table: "ReportVulnerabilities");

            migrationBuilder.DropTable(
                name: "RemediationChecklists");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_IdentityUserId",
                table: "UserProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vulnerabilities",
                table: "Vulnerabilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemTypes",
                table: "SystemTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportVulnerabilities",
                table: "ReportVulnerabilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_SystemTypeId1",
                table: "Assets");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d3aabc98-e3cb-5b64-b632-5ffa72b70b46", "7eb05375-f2a3-4ecf-92b5-4dbd11831839" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d3aabc98-e3cb-5b64-b632-5ffa72b70b46", "9f86ba56-ea82-4b9a-b593-7878b5d8916e" });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 4, 9 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 7, 9 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 9, 5 });

            migrationBuilder.DeleteData(
                table: "ReportVulnerabilities",
                keyColumns: new[] { "ReportId", "VulnerabilityId" },
                keyValues: new object[] { 9, 8 });

            migrationBuilder.DeleteData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3aabc98-e3cb-5b64-b632-5ffa72b70b46");

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Vulnerabilities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SystemTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SystemTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SystemTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SystemTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SystemTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7eb05375-f2a3-4ecf-92b5-4dbd11831839");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f86ba56-ea82-4b9a-b593-7878b5d8916e");

            migrationBuilder.DropColumn(
                name: "SystemTypeId1",
                table: "Assets");

            migrationBuilder.RenameTable(
                name: "Vulnerabilities",
                newName: "Vulnerability");

            migrationBuilder.RenameTable(
                name: "SystemTypes",
                newName: "SystemType");

            migrationBuilder.RenameTable(
                name: "ReportVulnerabilities",
                newName: "ReportVulnerability");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "Report");

            migrationBuilder.RenameTable(
                name: "Assets",
                newName: "Asset");

            migrationBuilder.RenameIndex(
                name: "IX_ReportVulnerabilities_VulnerabilityId",
                table: "ReportVulnerability",
                newName: "IX_ReportVulnerability_VulnerabilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_AssetId",
                table: "Report",
                newName: "IX_Report_AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_UserId",
                table: "Asset",
                newName: "IX_Asset_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_SystemTypeId",
                table: "Asset",
                newName: "IX_Asset_SystemTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ReportVulnerability",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vulnerability",
                table: "Vulnerability",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemType",
                table: "SystemType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportVulnerability",
                table: "ReportVulnerability",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Report",
                table: "Report",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset",
                table: "Asset",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                column: "NormalizedName",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "ea938af0-755b-46a3-9877-c62ef27c0afa", "admina@strator.comx", "AQAAAAIAAYagAAAAEDZHQ/Qw6l8la1bB3+xVKmkJNyDbRE0K1gtElPA89fxNXxy7QCx4MDzms0OXTZhHVQ==", "0708d188-8bc4-4e5c-8c16-cbaed49e8cf5", "Administrator" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "FirstName", "IsAdmin", "LastName", "Username" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admina", false, "Strator", null });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_IdentityUserId",
                table: "UserProfiles",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportVulnerability_ReportId",
                table: "ReportVulnerability",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_SystemType_SystemTypeId",
                table: "Asset",
                column: "SystemTypeId",
                principalTable: "SystemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_UserProfiles_UserId",
                table: "Asset",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Asset_AssetId",
                table: "Report",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportVulnerability_Report_ReportId",
                table: "ReportVulnerability",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportVulnerability_Vulnerability_VulnerabilityId",
                table: "ReportVulnerability",
                column: "VulnerabilityId",
                principalTable: "Vulnerability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Serenity_Security.Migrations
{
    /// <inheritdoc />
    public partial class _1740758659 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d3aabc98-e3cb-5b64-b632-5ffa72b70b46", "7eb05375-f2a3-4ecf-92b5-4dbd11831839" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d3aabc98-e3cb-5b64-b632-5ffa72b70b46", "9f86ba56-ea82-4b9a-b593-7878b5d8916e" });

            migrationBuilder.DeleteData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RemediationChecklists",
                keyColumn: "Id",
                keyValue: 8);

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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3aabc98-e3cb-5b64-b632-5ffa72b70b46");

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
                values: new object[] { "82323fa4-7706-470f-aa81-1567fb843336", "admina@strator.comx", "AQAAAAIAAYagAAAAELj45FLrJivoVNmRd0iehb47u9MQvjnQX6q0KG4AAbu4PsF9gR8CKAyMuY/azV/Gkg==", "fc72b376-4e13-44cd-a724-6098d58bdcda", "Administrator" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "FirstName", "IsAdmin", "LastName", "Username" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admina", false, "Strator", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "RemediationChecklists",
                columns: new[] { "Id", "CreatedAt", "Description", "FixedVersion", "IsCompleted", "VerificationSteps", "VulnerabilityId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 1, 9, 40, 22, 0, DateTimeKind.Unspecified), "This vulnerability allows attackers with admin access to execute arbitrary code through deserialization of untrusted data. Update to a patched version and implement proper input validation and authentication controls.", "Apache Pulsar 2.10.5, 2.11.2, or 3.0.1+", false, "1. Update Apache Pulsar to version 2.10.5, 2.11.2, or 3.0.1 or higher\n        2. Verify the installed version using: bin/pulsar version\n        3. Check configuration files for unsafe deserialization settings\n        4. Restart the Pulsar service: systemctl restart pulsar\n        5. Verify service is running with: systemctl status pulsar\n        6. Test the service by running a secure client connection and attempting to publish/consume messages\n        7. Review logs for any deserialization warnings or errors: grep -r \"deserialization\" /var/log/pulsar/", 1 },
                    { 2, new DateTime(2024, 2, 2, 15, 55, 33, 0, DateTimeKind.Unspecified), "This vulnerability allows attackers to enumerate valid usernames by measuring timing differences during authentication failures. Update to the latest version and configure sshd to use consistent timing for all authentication responses.", "OpenSSH 9.6+", false, "1. Update OpenSSH to version 9.6 or later\n        2. Check the installed version using: ssh -V\n        3. Edit /etc/ssh/sshd_config to add: UseDNS no\n        4. Set appropriate authentication attempt limits with: MaxAuthTries 4\n        5. Restart the SSH service: systemctl restart sshd\n        6. Verify timing consistency with multiple login attempts using different usernames\n        7. Check authentication logs for patterns: grep \"authentication failure\" /var/log/auth.log", 2 },
                    { 5, new DateTime(2024, 2, 3, 11, 35, 22, 0, DateTimeKind.Unspecified), "This vulnerability allows attackers to force Redis to perform unintended external server requests through malicious JNDI URIs. Update to a patched version and ensure Redis is not exposed to untrusted networks.", "Redis 6.2.14, 7.0.15, or 7.2.4+", false, "1. Update Redis to version 6.2.14, 7.0.15, or 7.2.4 or higher\n        2. Verify the installed version using: redis-cli info server | grep redis_version\n        3. Check and modify Redis configuration to disable or restrict JNDI URI handling\n        4. Restart the Redis service: systemctl restart redis\n        5. Verify service is running with: systemctl status redis\n        6. Test with a monitoring tool to ensure JNDI requests are properly handled\n        7. Review Redis logs for suspicious connection attempts: grep -r \"jndi\" /var/log/redis/", 5 },
                    { 7, new DateTime(2024, 2, 2, 16, 20, 33, 0, DateTimeKind.Unspecified), "This cross-site scripting vulnerability in Home Assistant Core's Logbook component could allow attackers to inject malicious scripts. Update to a patched version and use proper input sanitization.", "Home Assistant Core 2023.2.0+", false, "1. Update Home Assistant Core to version 2023.2.0 or later\n        2. Verify the installed version on the About page in Home Assistant\n        3. Restart the Home Assistant service\n        4. Check logs for any XSS-related warnings or errors\n        5. Clear browser cache and cookies after updating\n        6. Test the Logbook component with properly sanitized inputs\n        7. Consider implementing Content Security Policy (CSP) headers for additional protection\n        8. Periodically check for updates and security advisories", 7 },
                    { 8, new DateTime(2024, 2, 2, 15, 57, 45, 0, DateTimeKind.Unspecified), "This race condition in the Linux kernel's IPv6 subsystem could lead to memory corruption and local privilege escalation. Apply kernel updates as soon as possible and limit access to local user accounts.", "Kernel 6.1.67, 6.6.8, or distribution-specific patches", false, "1. Update the Linux kernel to version 6.1.67, 6.6.8 or later using the package manager\n        2. Verify the installed kernel version with: uname -r\n        3. Apply all pending security updates: apt update && apt upgrade -y\n        4. Reboot the system to apply the kernel updates\n        5. Check system logs for IPv6-related errors: grep -i ipv6 /var/log/syslog\n        6. Consider temporarily disabling IPv6 if updates cannot be applied immediately:\n           echo \"net.ipv6.conf.all.disable_ipv6 = 1\" >> /etc/sysctl.conf && sysctl -p\n        7. Test IPv6 functionality after updates to ensure proper operation\n        8. Monitor system for unusual behavior or performance issues", 8 }
                });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "FirstName", "IsAdmin", "LastName", "Username" },
                values: new object[] { new DateTime(2024, 1, 26, 9, 14, 32, 0, DateTimeKind.Unspecified), "Rosmary.Chadwick@proton.me", "Rosemary", true, "Chadwick", "Rosemary" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d3aabc98-e3cb-5b64-b632-5ffa72b70b46", "7eb05375-f2a3-4ecf-92b5-4dbd11831839" },
                    { "d3aabc98-e3cb-5b64-b632-5ffa72b70b46", "9f86ba56-ea82-4b9a-b593-7878b5d8916e" }
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
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serenity_Security.Models;

namespace Serenity_Security.Data;

public class Serenity_SecurityDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;

    // Define all DbSet properties together at the top
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<SystemType> SystemTypes { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Vulnerability> Vulnerabilities { get; set; }
    public DbSet<ReportVulnerability> ReportVulnerabilities { get; set; }
    public DbSet<RemediationChecklist> RemediationChecklists { get; set; }

    public Serenity_SecurityDbContext(
        DbContextOptions<Serenity_SecurityDbContext> context,
        IConfiguration config
    )
        : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Define Identity roles
        modelBuilder
            .Entity<IdentityRole>()
            .HasData(
                new IdentityRole
                {
                    Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },
                new IdentityRole
                {
                    Id = "d3aabc98-e3cb-5b64-b632-5ffa72b70b46",
                    Name = "User",
                    NormalizedName = "USER",
                }
            );

        // 2. Seed Identity users
        var adminUser = new IdentityUser
        {
            Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
            UserName = "Rosemary",
            Email = "Rosmary.Chadwick@proton.me",
            EmailConfirmed = true,
            NormalizedUserName = "ROSEMARY",
            NormalizedEmail = "ROSMARY.CHADWICK@PROTON.ME",
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(
                null,
                _configuration["AdminPassword"] ?? "Another2day!"
            ),
        };

        var userJason = new IdentityUser
        {
            Id = "7eb05375-f2a3-4ecf-92b5-4dbd11831839",
            UserName = "JasonT",
            Email = "jason.turner@example.com",
            EmailConfirmed = true,
            NormalizedUserName = "JASONT",
            NormalizedEmail = "JASON.TURNER@EXAMPLE.COM",
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "SecurePass123!"),
        };

        var userSamantha = new IdentityUser
        {
            Id = "9f86ba56-ea82-4b9a-b593-7878b5d8916e",
            UserName = "SamanthaB",
            Email = "samantha.brooks@example.com",
            EmailConfirmed = true,
            NormalizedUserName = "SAMANTHAB",
            NormalizedEmail = "SAMANTHA.BROOKS@EXAMPLE.COM",
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(
                null,
                "BrightMorning456!"
            ),
        };

        modelBuilder.Entity<IdentityUser>().HasData(adminUser, userJason, userSamantha);

        // 3. Assign users to roles
        modelBuilder
            .Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35", // Admin role
                    UserId = adminUser.Id,
                },
                new IdentityUserRole<string>
                {
                    RoleId = "d3aabc98-e3cb-5b64-b632-5ffa72b70b46", // User role
                    UserId = userJason.Id,
                },
                new IdentityUserRole<string>
                {
                    RoleId = "d3aabc98-e3cb-5b64-b632-5ffa72b70b46", // User role
                    UserId = userSamantha.Id,
                }
            );

        // 4. Seed User Profiles
        modelBuilder
            .Entity<UserProfile>()
            .HasData(
                new UserProfile
                {
                    Id = 1,
                    IdentityUserId = adminUser.Id,
                    FirstName = "Rosemary",
                    LastName = "Chadwick",
                    Email = "Rosmary.Chadwick@proton.me",
                    Username = "Rosemary",
                    IsAdmin = true,
                    CreatedAt = DateTime.Parse("2024-01-26 09:14:32"),
                },
                new UserProfile
                {
                    Id = 2,
                    IdentityUserId = userJason.Id,
                    FirstName = "Jason",
                    LastName = "Turner",
                    Email = "jason.turner@example.com",
                    Username = "JasonT",
                    IsAdmin = false,
                    CreatedAt = DateTime.Parse("2024-01-28 14:22:45"),
                },
                new UserProfile
                {
                    Id = 3,
                    IdentityUserId = userSamantha.Id,
                    FirstName = "Samantha",
                    LastName = "Brooks",
                    Email = "samantha.brooks@example.com",
                    Username = "SamanthaB",
                    IsAdmin = false,
                    CreatedAt = DateTime.Parse("2024-01-30 11:08:17"),
                }
            );

        // 5. Seed System Types
        modelBuilder
            .Entity<SystemType>()
            .HasData(
                new SystemType { Id = 1, Name = "Desktop Computer" },
                new SystemType { Id = 2, Name = "Laptop" },
                new SystemType { Id = 3, Name = "Home Router" },
                new SystemType { Id = 4, Name = "Smart Home Hub" },
                new SystemType { Id = 5, Name = "General Purpose" }
            );

        // 6. Seed Assets
        modelBuilder
            .Entity<Asset>()
            .HasData(
                // Rosemary's assets
                new Asset
                {
                    Id = 1,
                    UserId = 1,
                    SystemName = "Primary-Workstation",
                    IpAddress = "192.168.1.100",
                    OsVersion = "Windows 11 Pro 22H2",
                    SystemTypeId = 1,
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2024-01-28 10:15:22"),
                },
                new Asset
                {
                    Id = 2,
                    UserId = 1,
                    SystemName = "DevLaptop",
                    IpAddress = "192.168.1.101",
                    OsVersion = "macOS Ventura 13.5",
                    SystemTypeId = 2,
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2024-01-28 10:30:45"),
                },
                new Asset
                {
                    Id = 3,
                    UserId = 1,
                    SystemName = "HomeRouter",
                    IpAddress = "192.168.1.1",
                    OsVersion = "DD-WRT v3.0-r47479",
                    SystemTypeId = 3,
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2024-01-28 11:05:12"),
                },
                // Jason's assets
                new Asset
                {
                    Id = 4,
                    UserId = 2,
                    SystemName = "Gaming-PC",
                    IpAddress = "192.168.2.100",
                    OsVersion = "Windows 10 Home 21H2",
                    SystemTypeId = 1,
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2024-01-29 14:22:45"),
                },
                new Asset
                {
                    Id = 5,
                    UserId = 2,
                    SystemName = "Work-Laptop",
                    IpAddress = "192.168.2.101",
                    OsVersion = "Ubuntu 22.04 LTS",
                    SystemTypeId = 2,
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2024-01-29 15:45:33"),
                },
                new Asset
                {
                    Id = 6,
                    UserId = 2,
                    SystemName = "SmartHome-Hub",
                    IpAddress = "192.168.2.150",
                    OsVersion = "HomeOS 4.2",
                    SystemTypeId = 4,
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2024-01-29 16:10:27"),
                },
                // Samantha's assets
                new Asset
                {
                    Id = 7,
                    UserId = 3,
                    SystemName = "Personal-Laptop",
                    IpAddress = "192.168.3.100",
                    OsVersion = "macOS Sonoma 14.2",
                    SystemTypeId = 2,
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2024-01-31 09:45:12"),
                },
                new Asset
                {
                    Id = 8,
                    UserId = 3,
                    SystemName = "Home-Desktop",
                    IpAddress = "192.168.3.101",
                    OsVersion = "Windows 11 Home 23H2",
                    SystemTypeId = 1,
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2024-01-31 10:22:33"),
                },
                new Asset
                {
                    Id = 9,
                    UserId = 3,
                    SystemName = "MediaServer",
                    IpAddress = "192.168.3.150",
                    OsVersion = "Debian 12",
                    SystemTypeId = 5,
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2024-01-31 11:15:27"),
                }
            );

        // 7. Seed Vulnerabilities
        modelBuilder
            .Entity<Vulnerability>()
            .HasData(
                new Vulnerability
                {
                    Id = 1,
                    CveId = "CVE-2023-38408",
                    Description =
                        "Deserialization of Untrusted Data vulnerability in Apache Pulsar allows an attacker with admin access to successfully run remote code. This issue affects Apache Pulsar versions prior to 2.10.5, versions prior to 2.11.2, and versions prior to 3.0.1.",
                    CvsScore = 8.8m,
                    PublishedAt = DateTime.Parse("2024-01-15 14:30:22"),
                    SeverityLevel = "HIGH",
                },
                new Vulnerability
                {
                    Id = 2,
                    CveId = "CVE-2023-50164",
                    Description =
                        "A vulnerability in OpenSSH server could allow a remote attacker to discover valid usernames due to differences in authentication failures when using public key authentication. This vulnerability affects OpenSSH versions before 9.6.",
                    CvsScore = 5.3m,
                    PublishedAt = DateTime.Parse("2024-01-18 10:45:15"),
                    SeverityLevel = "MEDIUM",
                },
                new Vulnerability
                {
                    Id = 3,
                    CveId = "CVE-2023-29491",
                    Description =
                        "A security vulnerability in macOS could allow local attackers to escalate privileges by exploiting a memory corruption issue in the WindowServer component.",
                    CvsScore = 7.8m,
                    PublishedAt = DateTime.Parse("2024-01-20 09:22:18"),
                    SeverityLevel = "HIGH",
                },
                new Vulnerability
                {
                    Id = 4,
                    CveId = "CVE-2024-21626",
                    Description =
                        "A potential vulnerability in Microsoft Windows Hyper-V could allow an attacker to escape from a guest virtual machine to the host, potentially leading to escalation of privilege.",
                    CvsScore = 8.2m,
                    PublishedAt = DateTime.Parse("2024-01-22 16:10:34"),
                    SeverityLevel = "HIGH",
                },
                new Vulnerability
                {
                    Id = 5,
                    CveId = "CVE-2023-22527",
                    Description =
                        "When handling a malicious JNDI URI, Redis may perform an unintended request to an external server. This issue affects Redis versions before 6.2.14, 7.0.x before 7.0.15, and 7.2.x before 7.2.4.",
                    CvsScore = 6.5m,
                    PublishedAt = DateTime.Parse("2024-01-25 11:30:45"),
                    SeverityLevel = "MEDIUM",
                },
                new Vulnerability
                {
                    Id = 6,
                    CveId = "CVE-2023-50868",
                    Description =
                        "A vulnerability in DD-WRT allows attackers to execute unauthorized commands via the web interface due to insufficient input validation.",
                    CvsScore = 7.2m,
                    PublishedAt = DateTime.Parse("2024-01-27 08:15:30"),
                    SeverityLevel = "HIGH",
                },
                new Vulnerability
                {
                    Id = 7,
                    CveId = "CVE-2023-0297",
                    Description =
                        "Home Assistant Core has a cross-site scripting vulnerability in the Logbook component where user-controlled input was not properly sanitized.",
                    CvsScore = 5.4m,
                    PublishedAt = DateTime.Parse("2024-01-28 14:40:22"),
                    SeverityLevel = "MEDIUM",
                },
                new Vulnerability
                {
                    Id = 8,
                    CveId = "CVE-2023-51767",
                    Description =
                        "A race condition in the Linux kernel's IPv6 subsystem could lead to memory corruption and potential local privilege escalation.",
                    CvsScore = 7.0m,
                    PublishedAt = DateTime.Parse("2024-01-30 10:25:18"),
                    SeverityLevel = "HIGH",
                },
                new Vulnerability
                {
                    Id = 9,
                    CveId = "CVE-2023-4863",
                    Description =
                        "Heap buffer overflow in WebP in Google Chrome and other applications could allow a remote attacker to potentially exploit heap corruption via a crafted HTML page.",
                    CvsScore = 8.8m,
                    PublishedAt = DateTime.Parse("2024-01-31 09:15:27"),
                    SeverityLevel = "HIGH",
                }
            );

        // 8. Configure entity relationships
        modelBuilder
            .Entity<UserProfile>()
            .HasOne(up => up.IdentityUser)
            .WithOne()
            .HasForeignKey<UserProfile>(up => up.IdentityUserId);

        modelBuilder
            .Entity<Asset>()
            .HasOne(a => a.SystemType)
            .WithMany()
            .HasForeignKey(a => a.SystemTypeId);

        modelBuilder
            .Entity<Asset>()
            .HasOne(a => a.User)
            .WithMany(up => up.Assets)
            .HasForeignKey(a => a.UserId);

        modelBuilder
            .Entity<Report>()
            .HasOne(r => r.Asset)
            .WithMany(a => a.Reports)
            .HasForeignKey(r => r.AssetId);

        modelBuilder
            .Entity<ReportVulnerability>()
            .HasKey(rv => new { rv.ReportId, rv.VulnerabilityId });

        modelBuilder
            .Entity<ReportVulnerability>()
            .HasOne(rv => rv.Report)
            .WithMany(r => r.ReportVulnerabilities)
            .HasForeignKey(rv => rv.ReportId);

        modelBuilder
            .Entity<ReportVulnerability>()
            .HasOne(rv => rv.Vulnerability)
            .WithMany(v => v.ReportVulnerabilities)
            .HasForeignKey(rv => rv.VulnerabilityId);

        // 9. Seed Reports
        modelBuilder
            .Entity<Report>()
            .HasData(
                // Rosemary's reports
                new Report
                {
                    Id = 1,
                    AssetId = 1,
                    CreatedAt = DateTime.Parse("2024-02-01 09:30:12"),
                },
                new Report
                {
                    Id = 2,
                    AssetId = 2,
                    CreatedAt = DateTime.Parse("2024-02-01 10:45:22"),
                },
                new Report
                {
                    Id = 3,
                    AssetId = 3,
                    CreatedAt = DateTime.Parse("2024-02-01 11:15:33"),
                },
                // Jason's reports
                new Report
                {
                    Id = 4,
                    AssetId = 4,
                    CreatedAt = DateTime.Parse("2024-02-02 14:22:30"),
                },
                new Report
                {
                    Id = 5,
                    AssetId = 5,
                    CreatedAt = DateTime.Parse("2024-02-02 15:45:12"),
                },
                new Report
                {
                    Id = 6,
                    AssetId = 6,
                    CreatedAt = DateTime.Parse("2024-02-02 16:10:45"),
                },
                // Samantha's reports
                new Report
                {
                    Id = 7,
                    AssetId = 7,
                    CreatedAt = DateTime.Parse("2024-02-03 09:15:22"),
                },
                new Report
                {
                    Id = 8,
                    AssetId = 8,
                    CreatedAt = DateTime.Parse("2024-02-03 10:30:45"),
                },
                new Report
                {
                    Id = 9,
                    AssetId = 9,
                    CreatedAt = DateTime.Parse("2024-02-03 11:25:18"),
                }
            );

        // 10. Seed Report Vulnerabilities
        modelBuilder
            .Entity<ReportVulnerability>()
            .HasData(
                // Rosemary's workstation vulnerabilities
                new ReportVulnerability
                {
                    ReportId = 1,
                    VulnerabilityId = 4,
                    DiscoveredAt = DateTime.Parse("2024-02-01 09:35:22"),
                },
                // Rosemary's laptop vulnerabilities
                new ReportVulnerability
                {
                    ReportId = 2,
                    VulnerabilityId = 3,
                    DiscoveredAt = DateTime.Parse("2024-02-01 10:50:33"),
                },
                new ReportVulnerability
                {
                    ReportId = 2,
                    VulnerabilityId = 9,
                    DiscoveredAt = DateTime.Parse("2024-02-01 10:52:15"),
                },
                // Rosemary's router vulnerabilities
                new ReportVulnerability
                {
                    ReportId = 3,
                    VulnerabilityId = 6,
                    DiscoveredAt = DateTime.Parse("2024-02-01 11:20:45"),
                },
                // Jason's PC vulnerabilities
                new ReportVulnerability
                {
                    ReportId = 4,
                    VulnerabilityId = 4,
                    DiscoveredAt = DateTime.Parse("2024-02-02 14:25:33"),
                },
                new ReportVulnerability
                {
                    ReportId = 4,
                    VulnerabilityId = 9,
                    DiscoveredAt = DateTime.Parse("2024-02-02 14:27:45"),
                },
                // Jason's laptop vulnerabilities
                new ReportVulnerability
                {
                    ReportId = 5,
                    VulnerabilityId = 2,
                    DiscoveredAt = DateTime.Parse("2024-02-02 15:50:22"),
                },
                new ReportVulnerability
                {
                    ReportId = 5,
                    VulnerabilityId = 8,
                    DiscoveredAt = DateTime.Parse("2024-02-02 15:52:33"),
                },
                // Jason's hub vulnerabilities
                new ReportVulnerability
                {
                    ReportId = 6,
                    VulnerabilityId = 7,
                    DiscoveredAt = DateTime.Parse("2024-02-02 16:15:45"),
                },
                // Samantha's laptop vulnerabilities
                new ReportVulnerability
                {
                    ReportId = 7,
                    VulnerabilityId = 3,
                    DiscoveredAt = DateTime.Parse("2024-02-03 09:20:33"),
                },
                new ReportVulnerability
                {
                    ReportId = 7,
                    VulnerabilityId = 9,
                    DiscoveredAt = DateTime.Parse("2024-02-03 09:22:45"),
                },
                // Samantha's desktop vulnerabilities
                new ReportVulnerability
                {
                    ReportId = 8,
                    VulnerabilityId = 4,
                    DiscoveredAt = DateTime.Parse("2024-02-03 10:35:22"),
                },
                // Samantha's server vulnerabilities
                new ReportVulnerability
                {
                    ReportId = 9,
                    VulnerabilityId = 5,
                    DiscoveredAt = DateTime.Parse("2024-02-03 11:30:45"),
                },
                new ReportVulnerability
                {
                    ReportId = 9,
                    VulnerabilityId = 8,
                    DiscoveredAt = DateTime.Parse("2024-02-03 11:32:33"),
                }
            );

        // 11. Seed Remediation Checklists
        modelBuilder
            .Entity<RemediationChecklist>()
            .HasData(
                // CVE-2023-38408 (Apache Pulsar)
                new RemediationChecklist
                {
                    Id = 1,
                    VulnerabilityId = 1,
                    FixedVersion = "Apache Pulsar 2.10.5, 2.11.2, or 3.0.1+",
                    VerificationSteps =
                        @"1. Update Apache Pulsar to version 2.10.5, 2.11.2, or 3.0.1 or higher
2. Verify the installed version using: bin/pulsar version
3. Check configuration files for unsafe deserialization settings
4. Restart the Pulsar service: systemctl restart pulsar
5. Verify service is running with: systemctl status pulsar
6. Test the service by running a secure client connection and attempting to publish/consume messages
7. Review logs for any deserialization warnings or errors: grep -r ""deserialization"" /var/log/pulsar/",
                    Description =
                        "This vulnerability allows attackers with admin access to execute arbitrary code through deserialization of untrusted data. Update to a patched version and implement proper input validation and authentication controls.",
                    IsCompleted = false,
                    CreatedAt = DateTime.Parse("2024-02-01 09:40:22"),
                },
                // CVE-2023-50164 (OpenSSH)
                new RemediationChecklist
                {
                    Id = 2,
                    VulnerabilityId = 2,
                    FixedVersion = "OpenSSH 9.6+",
                    VerificationSteps =
                        @"1. Update OpenSSH to version 9.6 or later
2. Check the installed version using: ssh -V
3. Edit /etc/ssh/sshd_config to add: UseDNS no
4. Set appropriate authentication attempt limits with: MaxAuthTries 4
5. Restart the SSH service: systemctl restart sshd
6. Verify timing consistency with multiple login attempts using different usernames
7. Check authentication logs for patterns: grep ""authentication failure"" /var/log/auth.log",
                    Description =
                        "This vulnerability allows attackers to enumerate valid usernames by measuring timing differences during authentication failures. Update to the latest version and configure sshd to use consistent timing for all authentication responses.",
                    IsCompleted = false,
                    CreatedAt = DateTime.Parse("2024-02-02 15:55:33"),
                },
                // CVE-2023-29491 (macOS WindowServer)
                new RemediationChecklist
                {
                    Id = 3,
                    VulnerabilityId = 3,
                    FixedVersion = "macOS Ventura 13.5+, Sonoma 14.2+",
                    VerificationSteps =
                        @"1. Install the latest macOS security update via System Preferences > Software Update
2. Verify the installed version in ""About This Mac""
3. Restart the system completely
4. Check system logs for WindowServer errors: log show --predicate ""process == \""WindowServer\"""" --last 1h
5. Run Apple Diagnostics by restarting and holding D during startup
6. Verify fix by checking for advisory reference in installed security updates
7. Monitor system stability and report any graphical glitches or crashes",
                    Description =
                        "This vulnerability in macOS WindowServer component could allow local attackers to elevate privileges through memory corruption. Install all available security updates and limit access to privileged accounts.",
                    IsCompleted = false,
                    CreatedAt = DateTime.Parse("2024-02-01 10:55:45"),
                },
                // CVE-2024-21626 (Windows Hyper-V)
                new RemediationChecklist
                {
                    Id = 4,
                    VulnerabilityId = 4,
                    FixedVersion = "Latest Windows Security Update KB5034202",
                    VerificationSteps =
                        @"1. Install the latest Windows security update via Windows Update
2. Verify the installation of KB5034202 or newer in Update History
3. Restart the system completely
4. Check Event Viewer for Hyper-V-related errors
5. Run PowerShell command: Get-HotFix -Id KB5034202
6. For Hyper-V servers, ensure all VMs are updated and running with secure configurations
7. Test by running: Get-VMSecurity on all virtual machines to verify security settings
8. Apply latest firmware and driver updates for virtualization hardware",
                    Description =
                        "This vulnerability could allow guest-to-host virtual machine escapes in Hyper-V, leading to privilege escalation. Apply all security updates and consider disabling Hyper-V if not needed.",
                    IsCompleted = false,
                    CreatedAt = DateTime.Parse("2024-02-01 09:42:33"),
                },
                // CVE-2023-22527 (Redis)
                new RemediationChecklist
                {
                    Id = 5,
                    VulnerabilityId = 5,
                    FixedVersion = "Redis 6.2.14, 7.0.15, or 7.2.4+",
                    VerificationSteps =
                        @"1. Update Redis to version 6.2.14, 7.0.15, or 7.2.4 or higher
2. Verify the installed version using: redis-cli info server | grep redis_version
3. Check and modify Redis configuration to disable or restrict JNDI URI handling
4. Restart the Redis service: systemctl restart redis
5. Verify service is running with: systemctl status redis
6. Test with a monitoring tool to ensure JNDI requests are properly handled
7. Review Redis logs for suspicious connection attempts: grep -r ""jndi"" /var/log/redis/",
                    Description =
                        "This vulnerability allows attackers to force Redis to perform unintended external server requests through malicious JNDI URIs. Update to a patched version and ensure Redis is not exposed to untrusted networks.",
                    IsCompleted = false,
                    CreatedAt = DateTime.Parse("2024-02-03 11:35:22"),
                },
                // CVE-2023-50868 (DD-WRT)
                new RemediationChecklist
                {
                    Id = 6,
                    VulnerabilityId = 6,
                    FixedVersion = "DD-WRT r48000+",
                    VerificationSteps =
                        @"1. Update DD-WRT firmware to r48000 or later through the web interface
2. Verify the installed version in the router admin panel under Status > Router
3. After update, perform a hard reset (30-30-30 reset) to ensure clean configuration
4. Secure the admin interface with a strong password
5. Disable remote administration if not needed
6. Change default SSH and admin ports
7. Verify proper input sanitization by testing forms with special characters
8. Enable logging and monitor for suspicious activities",
                    Description =
                        "This vulnerability allows remote attackers to execute unauthorized commands via the web interface due to insufficient input validation. Update firmware immediately and restrict administration access.",
                    IsCompleted = false,
                    CreatedAt = DateTime.Parse("2024-02-01 11:25:45"),
                },
                // CVE-2023-0297 (Home Assistant)
                new RemediationChecklist
                {
                    Id = 7,
                    VulnerabilityId = 7,
                    FixedVersion = "Home Assistant Core 2023.2.0+",
                    VerificationSteps =
                        @"1. Update Home Assistant Core to version 2023.2.0 or later
2. Verify the installed version on the About page in Home Assistant
3. Restart the Home Assistant service
4. Check logs for any XSS-related warnings or errors
5. Clear browser cache and cookies after updating
6. Test the Logbook component with properly sanitized inputs
7. Consider implementing Content Security Policy (CSP) headers for additional protection
8. Periodically check for updates and security advisories",
                    Description =
                        "This cross-site scripting vulnerability in Home Assistant Core's Logbook component could allow attackers to inject malicious scripts. Update to a patched version and use proper input sanitization.",
                    IsCompleted = false,
                    CreatedAt = DateTime.Parse("2024-02-02 16:20:33"),
                },
                // CVE-2023-51767 (Linux kernel IPv6)
                new RemediationChecklist
                {
                    Id = 8,
                    VulnerabilityId = 8,
                    FixedVersion = "Kernel 6.1.67, 6.6.8, or distribution-specific patches",
                    VerificationSteps =
                        @"1. Update the Linux kernel to version 6.1.67, 6.6.8 or later using the package manager
2. Verify the installed kernel version with: uname -r
3. Apply all pending security updates: apt update && apt upgrade -y
4. Reboot the system to apply the kernel updates
5. Check system logs for IPv6-related errors: grep -i ipv6 /var/log/syslog
6. Consider temporarily disabling IPv6 if updates cannot be applied immediately:
   echo ""net.ipv6.conf.all.disable_ipv6 = 1"" >> /etc/sysctl.conf && sysctl -p
7. Test IPv6 functionality after updates to ensure proper operation
8. Monitor system for unusual behavior or performance issues",
                    Description =
                        "This race condition in the Linux kernel's IPv6 subsystem could lead to memory corruption and local privilege escalation. Apply kernel updates as soon as possible and limit access to local user accounts.",
                    IsCompleted = false,
                    CreatedAt = DateTime.Parse("2024-02-02 15:57:45"),
                },
                // CVE-2023-4863 (WebP)
                new RemediationChecklist
                {
                    Id = 9,
                    VulnerabilityId = 9,
                    FixedVersion = "Chrome 116.0.5845.187+, Firefox 117.0.1+, Safari 16.6+",
                    VerificationSteps =
                        @"1. Update all web browsers to their latest versions:
   - Chrome 116.0.5845.187 or later
   - Firefox 117.0.1 or later
   - Safari 16.6 or later
2. Verify browser versions through their respective About pages
3. Enable automatic updates for all browsers
4. Update operating system with latest security patches
5. Scan for other applications using WebP libraries (image editors, media players)
6. Update vulnerable applications through their official channels
7. Consider using browser extensions that block untrusted image loading
8. Monitor security advisories for related vulnerabilities",
                    Description =
                        "This heap buffer overflow vulnerability in WebP handling could allow remote attackers to execute arbitrary code or cause denial of service by crafting malicious WebP images. Update all browsers and applications that process WebP images.",
                    IsCompleted = false,
                    CreatedAt = DateTime.Parse("2024-02-01 10:57:33"),
                }
            );
    }
}

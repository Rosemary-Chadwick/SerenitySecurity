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

        modelBuilder
            .Entity<IdentityRole>()
            .HasData(
                new IdentityRole
                {
                    Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                    Name = "Admin",
                    NormalizedName = "admin",
                }
            );

        modelBuilder
            .Entity<IdentityUser>()
            .HasData(
                new IdentityUser
                {
                    Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                    UserName = "Administrator",
                    Email = "admina@strator.comx",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(
                        null,
                        _configuration["AdminPassword"]
                    ),
                }
            );

        modelBuilder
            .Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                    UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                }
            );
        modelBuilder
            .Entity<UserProfile>()
            .HasData(
                new UserProfile
                {
                    Id = 1,
                    IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                    FirstName = "Admina",
                    LastName = "Strator",
                    // Address = "101 Main Street",
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
            .HasOne(a => a.User)
            .WithMany(up => up.Assets)
            .HasForeignKey(a => a.UserId);

        modelBuilder
            .Entity<Asset>()
            .HasOne(a => a.SystemType)
            .WithMany(st => st.Assets) // Make sure SystemType has Assets collection
            .HasForeignKey(a => a.SystemTypeId)
            .OnDelete(DeleteBehavior.Restrict);

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
                new Report
                {
                    Id = 1,
                    AssetId = 1,
                    CreatedAt = DateTime.Parse("2024-02-01 09:30:12"),
                    IsCompleted = false,
                },
                new Report
                {
                    Id = 2,
                    AssetId = 2,
                    CreatedAt = DateTime.Parse("2024-02-01 10:45:22"),
                    IsCompleted = false,
                },
                new Report
                {
                    Id = 3,
                    AssetId = 3,
                    CreatedAt = DateTime.Parse("2024-02-01 11:15:33"),
                    IsCompleted = false,
                }
            );

        // 10. Seed Report Vulnerabilities
        modelBuilder
            .Entity<ReportVulnerability>()
            .HasData(
                // workstation vulnerabilities
                new ReportVulnerability
                {
                    ReportId = 1,
                    VulnerabilityId = 4,
                    DiscoveredAt = DateTime.Parse("2024-02-01 09:35:22"),
                },
                // laptop vulnerabilities
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
                // router vulnerabilities
                new ReportVulnerability
                {
                    ReportId = 3,
                    VulnerabilityId = 6,
                    DiscoveredAt = DateTime.Parse("2024-02-01 11:20:45"),
                }
            );

        // 11. Seed Remediation Checklists
        modelBuilder
            .Entity<RemediationChecklist>()
            .HasData(
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

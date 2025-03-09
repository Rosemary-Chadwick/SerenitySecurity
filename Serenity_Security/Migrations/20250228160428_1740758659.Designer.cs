﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Serenity_Security.Data;

#nullable disable

namespace Serenity_Security.Migrations
{
    [DbContext(typeof(Serenity_SecurityDbContext))]
    [Migration("20250228160428_1740758659")]
    partial class _1740758659
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                            Name = "Admin",
                            NormalizedName = "admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "82323fa4-7706-470f-aa81-1567fb843336",
                            Email = "admina@strator.comx",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAIAAYagAAAAELj45FLrJivoVNmRd0iehb47u9MQvjnQX6q0KG4AAbu4PsF9gR8CKAyMuY/azV/Gkg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "fc72b376-4e13-44cd-a724-6098d58bdcda",
                            TwoFactorEnabled = false,
                            UserName = "Administrator"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                            RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Serenity_Security.Models.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IpAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("OsVersion")
                        .HasColumnType("text");

                    b.Property<string>("SystemName")
                        .HasColumnType("text");

                    b.Property<int>("SystemTypeId")
                        .HasColumnType("integer");

                    b.Property<int?>("SystemTypeId1")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SystemTypeId");

                    b.HasIndex("SystemTypeId1");

                    b.HasIndex("UserId");

                    b.ToTable("Assets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 1, 28, 10, 15, 22, 0, DateTimeKind.Unspecified),
                            IpAddress = "192.168.1.100",
                            IsActive = true,
                            OsVersion = "Windows 11 Pro 22H2",
                            SystemName = "Primary-Workstation",
                            SystemTypeId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 1, 28, 10, 30, 45, 0, DateTimeKind.Unspecified),
                            IpAddress = "192.168.1.101",
                            IsActive = true,
                            OsVersion = "macOS Ventura 13.5",
                            SystemName = "DevLaptop",
                            SystemTypeId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 1, 28, 11, 5, 12, 0, DateTimeKind.Unspecified),
                            IpAddress = "192.168.1.1",
                            IsActive = true,
                            OsVersion = "DD-WRT v3.0-r47479",
                            SystemName = "HomeRouter",
                            SystemTypeId = 3,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Serenity_Security.Models.RemediationChecklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FixedVersion")
                        .HasColumnType("text");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<string>("VerificationSteps")
                        .HasColumnType("text");

                    b.Property<int>("VulnerabilityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VulnerabilityId");

                    b.ToTable("RemediationChecklists");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 2, 1, 10, 55, 45, 0, DateTimeKind.Unspecified),
                            Description = "This vulnerability in macOS WindowServer component could allow local attackers to elevate privileges through memory corruption. Install all available security updates and limit access to privileged accounts.",
                            FixedVersion = "macOS Ventura 13.5+, Sonoma 14.2+",
                            IsCompleted = false,
                            VerificationSteps = "1. Install the latest macOS security update via System Preferences > Software Update\n        2. Verify the installed version in \"About This Mac\"\n        3. Restart the system completely\n        4. Check system logs for WindowServer errors: log show --predicate \"process == \\\"WindowServer\\\"\" --last 1h\n        5. Run Apple Diagnostics by restarting and holding D during startup\n        6. Verify fix by checking for advisory reference in installed security updates\n        7. Monitor system stability and report any graphical glitches or crashes",
                            VulnerabilityId = 3
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2024, 2, 1, 9, 42, 33, 0, DateTimeKind.Unspecified),
                            Description = "This vulnerability could allow guest-to-host virtual machine escapes in Hyper-V, leading to privilege escalation. Apply all security updates and consider disabling Hyper-V if not needed.",
                            FixedVersion = "Latest Windows Security Update KB5034202",
                            IsCompleted = false,
                            VerificationSteps = "1. Install the latest Windows security update via Windows Update\n        2. Verify the installation of KB5034202 or newer in Update History\n        3. Restart the system completely\n        4. Check Event Viewer for Hyper-V-related errors\n        5. Run PowerShell command: Get-HotFix -Id KB5034202\n        6. For Hyper-V servers, ensure all VMs are updated and running with secure configurations\n        7. Test by running: Get-VMSecurity on all virtual machines to verify security settings\n        8. Apply latest firmware and driver updates for virtualization hardware",
                            VulnerabilityId = 4
                        },
                        new
                        {
                            Id = 6,
                            CreatedAt = new DateTime(2024, 2, 1, 11, 25, 45, 0, DateTimeKind.Unspecified),
                            Description = "This vulnerability allows remote attackers to execute unauthorized commands via the web interface due to insufficient input validation. Update firmware immediately and restrict administration access.",
                            FixedVersion = "DD-WRT r48000+",
                            IsCompleted = false,
                            VerificationSteps = "1. Update DD-WRT firmware to r48000 or later through the web interface\n        2. Verify the installed version in the router admin panel under Status > Router\n        3. After update, perform a hard reset (30-30-30 reset) to ensure clean configuration\n        4. Secure the admin interface with a strong password\n        5. Disable remote administration if not needed\n        6. Change default SSH and admin ports\n        7. Verify proper input sanitization by testing forms with special characters\n        8. Enable logging and monitor for suspicious activities",
                            VulnerabilityId = 6
                        },
                        new
                        {
                            Id = 9,
                            CreatedAt = new DateTime(2024, 2, 1, 10, 57, 33, 0, DateTimeKind.Unspecified),
                            Description = "This heap buffer overflow vulnerability in WebP handling could allow remote attackers to execute arbitrary code or cause denial of service by crafting malicious WebP images. Update all browsers and applications that process WebP images.",
                            FixedVersion = "Chrome 116.0.5845.187+, Firefox 117.0.1+, Safari 16.6+",
                            IsCompleted = false,
                            VerificationSteps = "1. Update all web browsers to their latest versions:\n           - Chrome 116.0.5845.187 or later\n           - Firefox 117.0.1 or later\n           - Safari 16.6 or later\n        2. Verify browser versions through their respective About pages\n        3. Enable automatic updates for all browsers\n        4. Update operating system with latest security patches\n        5. Scan for other applications using WebP libraries (image editors, media players)\n        6. Update vulnerable applications through their official channels\n        7. Consider using browser extensions that block untrusted image loading\n        8. Monitor security advisories for related vulnerabilities",
                            VulnerabilityId = 9
                        });
                });

            modelBuilder.Entity("Serenity_Security.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AssetId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.ToTable("Reports");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssetId = 1,
                            CreatedAt = new DateTime(2024, 2, 1, 9, 30, 12, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            AssetId = 2,
                            CreatedAt = new DateTime(2024, 2, 1, 10, 45, 22, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            AssetId = 3,
                            CreatedAt = new DateTime(2024, 2, 1, 11, 15, 33, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Serenity_Security.Models.ReportVulnerability", b =>
                {
                    b.Property<int>("ReportId")
                        .HasColumnType("integer");

                    b.Property<int>("VulnerabilityId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DiscoveredAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("ReportId", "VulnerabilityId");

                    b.HasIndex("VulnerabilityId");

                    b.ToTable("ReportVulnerabilities");

                    b.HasData(
                        new
                        {
                            ReportId = 1,
                            VulnerabilityId = 4,
                            DiscoveredAt = new DateTime(2024, 2, 1, 9, 35, 22, 0, DateTimeKind.Unspecified),
                            Id = 0
                        },
                        new
                        {
                            ReportId = 2,
                            VulnerabilityId = 3,
                            DiscoveredAt = new DateTime(2024, 2, 1, 10, 50, 33, 0, DateTimeKind.Unspecified),
                            Id = 0
                        },
                        new
                        {
                            ReportId = 2,
                            VulnerabilityId = 9,
                            DiscoveredAt = new DateTime(2024, 2, 1, 10, 52, 15, 0, DateTimeKind.Unspecified),
                            Id = 0
                        },
                        new
                        {
                            ReportId = 3,
                            VulnerabilityId = 6,
                            DiscoveredAt = new DateTime(2024, 2, 1, 11, 20, 45, 0, DateTimeKind.Unspecified),
                            Id = 0
                        });
                });

            modelBuilder.Entity("Serenity_Security.Models.SystemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SystemTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Desktop Computer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Laptop"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Home Router"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Smart Home Hub"
                        },
                        new
                        {
                            Id = 5,
                            Name = "General Purpose"
                        });
                });

            modelBuilder.Entity("Serenity_Security.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("IdentityUserId")
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdentityUserId")
                        .IsUnique();

                    b.ToTable("UserProfiles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Admina",
                            IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                            IsAdmin = false,
                            LastName = "Strator"
                        });
                });

            modelBuilder.Entity("Serenity_Security.Models.Vulnerability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CveId")
                        .HasColumnType("text");

                    b.Property<decimal>("CvsScore")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SeverityLevel")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Vulnerabilities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CveId = "CVE-2023-38408",
                            CvsScore = 8.8m,
                            Description = "Deserialization of Untrusted Data vulnerability in Apache Pulsar allows an attacker with admin access to successfully run remote code. This issue affects Apache Pulsar versions prior to 2.10.5, versions prior to 2.11.2, and versions prior to 3.0.1.",
                            PublishedAt = new DateTime(2024, 1, 15, 14, 30, 22, 0, DateTimeKind.Unspecified),
                            SeverityLevel = "HIGH"
                        },
                        new
                        {
                            Id = 2,
                            CveId = "CVE-2023-50164",
                            CvsScore = 5.3m,
                            Description = "A vulnerability in OpenSSH server could allow a remote attacker to discover valid usernames due to differences in authentication failures when using public key authentication. This vulnerability affects OpenSSH versions before 9.6.",
                            PublishedAt = new DateTime(2024, 1, 18, 10, 45, 15, 0, DateTimeKind.Unspecified),
                            SeverityLevel = "MEDIUM"
                        },
                        new
                        {
                            Id = 3,
                            CveId = "CVE-2023-29491",
                            CvsScore = 7.8m,
                            Description = "A security vulnerability in macOS could allow local attackers to escalate privileges by exploiting a memory corruption issue in the WindowServer component.",
                            PublishedAt = new DateTime(2024, 1, 20, 9, 22, 18, 0, DateTimeKind.Unspecified),
                            SeverityLevel = "HIGH"
                        },
                        new
                        {
                            Id = 4,
                            CveId = "CVE-2024-21626",
                            CvsScore = 8.2m,
                            Description = "A potential vulnerability in Microsoft Windows Hyper-V could allow an attacker to escape from a guest virtual machine to the host, potentially leading to escalation of privilege.",
                            PublishedAt = new DateTime(2024, 1, 22, 16, 10, 34, 0, DateTimeKind.Unspecified),
                            SeverityLevel = "HIGH"
                        },
                        new
                        {
                            Id = 5,
                            CveId = "CVE-2023-22527",
                            CvsScore = 6.5m,
                            Description = "When handling a malicious JNDI URI, Redis may perform an unintended request to an external server. This issue affects Redis versions before 6.2.14, 7.0.x before 7.0.15, and 7.2.x before 7.2.4.",
                            PublishedAt = new DateTime(2024, 1, 25, 11, 30, 45, 0, DateTimeKind.Unspecified),
                            SeverityLevel = "MEDIUM"
                        },
                        new
                        {
                            Id = 6,
                            CveId = "CVE-2023-50868",
                            CvsScore = 7.2m,
                            Description = "A vulnerability in DD-WRT allows attackers to execute unauthorized commands via the web interface due to insufficient input validation.",
                            PublishedAt = new DateTime(2024, 1, 27, 8, 15, 30, 0, DateTimeKind.Unspecified),
                            SeverityLevel = "HIGH"
                        },
                        new
                        {
                            Id = 7,
                            CveId = "CVE-2023-0297",
                            CvsScore = 5.4m,
                            Description = "Home Assistant Core has a cross-site scripting vulnerability in the Logbook component where user-controlled input was not properly sanitized.",
                            PublishedAt = new DateTime(2024, 1, 28, 14, 40, 22, 0, DateTimeKind.Unspecified),
                            SeverityLevel = "MEDIUM"
                        },
                        new
                        {
                            Id = 8,
                            CveId = "CVE-2023-51767",
                            CvsScore = 7.0m,
                            Description = "A race condition in the Linux kernel's IPv6 subsystem could lead to memory corruption and potential local privilege escalation.",
                            PublishedAt = new DateTime(2024, 1, 30, 10, 25, 18, 0, DateTimeKind.Unspecified),
                            SeverityLevel = "HIGH"
                        },
                        new
                        {
                            Id = 9,
                            CveId = "CVE-2023-4863",
                            CvsScore = 8.8m,
                            Description = "Heap buffer overflow in WebP in Google Chrome and other applications could allow a remote attacker to potentially exploit heap corruption via a crafted HTML page.",
                            PublishedAt = new DateTime(2024, 1, 31, 9, 15, 27, 0, DateTimeKind.Unspecified),
                            SeverityLevel = "HIGH"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Serenity_Security.Models.Asset", b =>
                {
                    b.HasOne("Serenity_Security.Models.SystemType", "SystemType")
                        .WithMany()
                        .HasForeignKey("SystemTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Serenity_Security.Models.SystemType", null)
                        .WithMany("Assets")
                        .HasForeignKey("SystemTypeId1");

                    b.HasOne("Serenity_Security.Models.UserProfile", "User")
                        .WithMany("Assets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SystemType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Serenity_Security.Models.RemediationChecklist", b =>
                {
                    b.HasOne("Serenity_Security.Models.Vulnerability", "Vulnerability")
                        .WithMany()
                        .HasForeignKey("VulnerabilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vulnerability");
                });

            modelBuilder.Entity("Serenity_Security.Models.Report", b =>
                {
                    b.HasOne("Serenity_Security.Models.Asset", "Asset")
                        .WithMany("Reports")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("Serenity_Security.Models.ReportVulnerability", b =>
                {
                    b.HasOne("Serenity_Security.Models.Report", "Report")
                        .WithMany("ReportVulnerabilities")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Serenity_Security.Models.Vulnerability", "Vulnerability")
                        .WithMany("ReportVulnerabilities")
                        .HasForeignKey("VulnerabilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");

                    b.Navigation("Vulnerability");
                });

            modelBuilder.Entity("Serenity_Security.Models.UserProfile", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithOne()
                        .HasForeignKey("Serenity_Security.Models.UserProfile", "IdentityUserId");

                    b.Navigation("IdentityUser");
                });

            modelBuilder.Entity("Serenity_Security.Models.Asset", b =>
                {
                    b.Navigation("Reports");
                });

            modelBuilder.Entity("Serenity_Security.Models.Report", b =>
                {
                    b.Navigation("ReportVulnerabilities");
                });

            modelBuilder.Entity("Serenity_Security.Models.SystemType", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("Serenity_Security.Models.UserProfile", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("Serenity_Security.Models.Vulnerability", b =>
                {
                    b.Navigation("ReportVulnerabilities");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220917070156_AddSeed")]
    partial class AddSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Carts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatorId = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a"
                        });
                });

            modelBuilder.Entity("Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Сategory has been deleted"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Laptops"
                        },
                        new
                        {
                            Id = 3,
                            Title = "Books"
                        },
                        new
                        {
                            Id = 4,
                            Title = "Footwear"
                        });
                });

            modelBuilder.Entity("Core.Entities.Characteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("WareId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WareId");

                    b.ToTable("Characteristics");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Color",
                            Value = "Pine Grey",
                            WareId = 7
                        },
                        new
                        {
                            Id = 2,
                            Name = "Processor",
                            Value = "Intel Core i7-1165G7",
                            WareId = 7
                        },
                        new
                        {
                            Id = 3,
                            Name = "RAM",
                            Value = "16 GB",
                            WareId = 7
                        },
                        new
                        {
                            Id = 4,
                            Name = "SSD",
                            Value = "1 TB",
                            WareId = 7
                        },
                        new
                        {
                            Id = 5,
                            Name = "Weight",
                            Value = "2.1 kg",
                            WareId = 8
                        },
                        new
                        {
                            Id = 6,
                            Name = "Color",
                            Value = "Gray",
                            WareId = 8
                        },
                        new
                        {
                            Id = 7,
                            Name = "Diagonal",
                            Value = "16.2",
                            WareId = 8
                        });
                });

            modelBuilder.Entity("Core.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CourierId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsAcceptedByClient")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAcceptedByCourier")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CartId")
                        .IsUnique();

                    b.HasIndex("CourierId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Core.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Core.Entities.Ware", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableCount")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("PhotoLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Wares");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvailableCount = 5,
                            CategoryId = 4,
                            Cost = 40450.0,
                            CreationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(3229), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a",
                            Description = "GIANVITO ROSSI",
                            PhotoLink = "3d67cfc9-341f-4c3d-8f4c-8da09f2ce953.jpeg",
                            Title = "Ankle Boots"
                        },
                        new
                        {
                            Id = 2,
                            AvailableCount = 3,
                            CategoryId = 4,
                            Cost = 17362.0,
                            CreationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4963), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a",
                            Description = "EYTYS",
                            PhotoLink = "f80bde70-20a4-45f2-b755-5282a46308e1.jpeg",
                            Title = "Halo Leather Sneaker"
                        },
                        new
                        {
                            Id = 3,
                            AvailableCount = 15,
                            CategoryId = 4,
                            Cost = 2770.0,
                            CreationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4968), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a",
                            Description = "CONVERSE",
                            PhotoLink = "9c2e3e64-07f5-44fe-97f8-ffc4fb02d52e.jpeg",
                            Title = "Chuck Taylor All Star"
                        },
                        new
                        {
                            Id = 4,
                            AvailableCount = 38,
                            CategoryId = 3,
                            Cost = 257.0,
                            CreationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4971), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a",
                            Description = "Whose truth is the lie? Stay up all night readingthe sensational psychological thriller that has readers obsessed,from the #1 New York Times bestselling author of It Ends With Us.",
                            PhotoLink = "0901295c-e213-4a9c-9463-5744499c839a.jpeg",
                            Title = "Verity"
                        },
                        new
                        {
                            Id = 5,
                            AvailableCount = 13,
                            CategoryId = 3,
                            Cost = 841.0,
                            CreationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4975), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a",
                            Description = "The Myth of Normal: Trauma, Illness, and Healing in a Toxic Culture",
                            PhotoLink = "8a16df30-8159-4499-ab51-456d87c9d113.jpeg",
                            Title = "The Myth of Normal"
                        },
                        new
                        {
                            Id = 6,
                            AvailableCount = 11,
                            CategoryId = 3,
                            Cost = 701.0,
                            CreationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4978), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a",
                            Description = "What If? 2: Additional Serious Scientific Answers toAbsurd Hypothetical Questions.",
                            PhotoLink = "d4a9e5ea-ae19-49b5-a406-55faf60ba19a.jpeg",
                            Title = "What If? 2"
                        },
                        new
                        {
                            Id = 7,
                            AvailableCount = 17,
                            CategoryId = 2,
                            Cost = 59999.0,
                            CreationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4981), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a",
                            Description = "OLED UP5401EA-KN094W (90NB0V41-M004V0)",
                            PhotoLink = "4e8967e8-c1d1-45e9-9a6e-758e980e0614.jpeg",
                            Title = "ASUS ZenBook 14 Flip OLED"
                        },
                        new
                        {
                            Id = 8,
                            AvailableCount = 19,
                            CategoryId = 2,
                            Cost = 114999.0,
                            CreationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4985), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a",
                            Description = "MK183UA/A",
                            PhotoLink = "91dd7d98-b00f-424e-8b2a-960eecf4ca2e.jpeg",
                            Title = "Apple MacBook Pro"
                        },
                        new
                        {
                            Id = 9,
                            AvailableCount = 19,
                            CategoryId = 2,
                            Cost = 42999.0,
                            CreationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4988), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a",
                            Description = "Chip Gold (MGND3)",
                            PhotoLink = "e8bb3505-7ca2-4d5d-b2df-9232767abae8.jpeg",
                            Title = "Apple MacBook Air"
                        });
                });

            modelBuilder.Entity("Core.Entities.WareCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DateOfAdding")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("WareId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("WareId");

                    b.ToTable("WareCarts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "6cbc16ff-d1e1-4780-a7c4-0f382f3fcc94",
                            ConcurrencyStamp = "6cbc16ff-d1e1-4780-a7c4-0f382f3fcc94",
                            Name = "Client",
                            NormalizedName = "CLIENT"
                        },
                        new
                        {
                            Id = "dcf03835-34c0-4a96-b296-cafb4d2b34c3",
                            ConcurrencyStamp = "dcf03835-34c0-4a96-b296-cafb4d2b34c3",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "879e1ab2-8ff2-45a9-917e-ef646376a045",
                            ConcurrencyStamp = "879e1ab2-8ff2-45a9-917e-ef646376a045",
                            Name = "Courier",
                            NormalizedName = "COURIER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "aac58995-671c-4cac-ae5c-48042350dbeb",
                            RoleId = "dcf03835-34c0-4a96-b296-cafb4d2b34c3"
                        },
                        new
                        {
                            UserId = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a",
                            RoleId = "6cbc16ff-d1e1-4780-a7c4-0f382f3fcc94"
                        },
                        new
                        {
                            UserId = "7fe48240-c8a2-4995-ac12-7d5b9f33c04e",
                            RoleId = "879e1ab2-8ff2-45a9-917e-ef646376a045"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhotoLink")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("RegistrationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasDiscriminator().HasValue("User");

                    b.HasData(
                        new
                        {
                            Id = "aac58995-671c-4cac-ae5c-48042350dbeb",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "35726dc0-4fd5-41f3-9511-4bdfcf507ec7",
                            Email = "marylou@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "MARYLOU@GMAIL.COM",
                            NormalizedUserName = "MARYLOU@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAED/FsUEo/0VIMBgSFHHJyuTrGY4P2XfgrvofRYRAydG/KkTww2PP/F7HbbocG9F6lw==",
                            PhoneNumber = "+380986734245",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "4fa3a724-3f12-494d-ac6a-6fbb19a3dbd3",
                            TwoFactorEnabled = false,
                            UserName = "marylou@gmail.com",
                            Name = "Mary",
                            RegistrationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 708, DateTimeKind.Unspecified).AddTicks(7925), new TimeSpan(0, 0, 0, 0, 0)),
                            Surname = "Lou"
                        },
                        new
                        {
                            Id = "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e32a3cb4-94f9-462e-9d25-4bdd37395db1",
                            Email = "etsukomami@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ETSUKOMAMI@GMAIL.COM",
                            NormalizedUserName = "ETSUKOMAMI@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEEZt44JF11xjGZ5O4luQAad0E90/PGsXgSJcLpaPWzxbsQvyswr05zKPCIioQWw6rg==",
                            PhoneNumber = "+380988931245",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2184aece-4edc-4634-864c-fef1842401f3",
                            TwoFactorEnabled = false,
                            UserName = "etsukomami@gmail.com",
                            Name = "Etsuko",
                            RegistrationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 710, DateTimeKind.Unspecified).AddTicks(2693), new TimeSpan(0, 0, 0, 0, 0)),
                            Surname = "Mami"
                        },
                        new
                        {
                            Id = "7fe48240-c8a2-4995-ac12-7d5b9f33c04e",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "7b647e02-18a3-44ee-b4af-bae87b19417f",
                            Email = "yuurimorishita@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "YUURIMORISHITA@GMAIL.COM",
                            NormalizedUserName = "YUURIMORISHITA@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEGKRW0jrfDWzgj7HdBT6t8ulhtqT8oZFlbvS0tpPFT24fpxQ59wWkaGNOhvRvh+dYQ==",
                            PhoneNumber = "+380988931245",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "b2750684-fa01-42eb-917c-7fb57382f75a",
                            TwoFactorEnabled = false,
                            UserName = "yuurimorishita@gmail.com",
                            Name = "Yuuri",
                            RegistrationDate = new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 710, DateTimeKind.Unspecified).AddTicks(3021), new TimeSpan(0, 0, 0, 0, 0)),
                            Surname = "Morishita"
                        });
                });

            modelBuilder.Entity("Core.Entities.Cart", b =>
                {
                    b.HasOne("Core.Entities.User", "Creator")
                        .WithMany("Carts")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Core.Entities.Characteristic", b =>
                {
                    b.HasOne("Core.Entities.Ware", "Ware")
                        .WithMany("Characteristics")
                        .HasForeignKey("WareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ware");
                });

            modelBuilder.Entity("Core.Entities.Order", b =>
                {
                    b.HasOne("Core.Entities.Cart", "Cart")
                        .WithOne("Order")
                        .HasForeignKey("Core.Entities.Order", "CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "Courier")
                        .WithMany("Orders")
                        .HasForeignKey("CourierId");

                    b.Navigation("Cart");

                    b.Navigation("Courier");
                });

            modelBuilder.Entity("Core.Entities.RefreshToken", b =>
                {
                    b.HasOne("Core.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.Ware", b =>
                {
                    b.HasOne("Core.Entities.Category", "Category")
                        .WithMany("Wares")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "Creator")
                        .WithMany("Wares")
                        .HasForeignKey("CreatorId");

                    b.Navigation("Category");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Core.Entities.WareCart", b =>
                {
                    b.HasOne("Core.Entities.Cart", "Cart")
                        .WithMany("WareCarts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Ware", "Ware")
                        .WithMany("WareCarts")
                        .HasForeignKey("WareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Ware");
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

            modelBuilder.Entity("Core.Entities.Cart", b =>
                {
                    b.Navigation("Order");

                    b.Navigation("WareCarts");
                });

            modelBuilder.Entity("Core.Entities.Category", b =>
                {
                    b.Navigation("Wares");
                });

            modelBuilder.Entity("Core.Entities.Ware", b =>
                {
                    b.Navigation("Characteristics");

                    b.Navigation("WareCarts");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Wares");
                });
#pragma warning restore 612, 618
        }
    }
}
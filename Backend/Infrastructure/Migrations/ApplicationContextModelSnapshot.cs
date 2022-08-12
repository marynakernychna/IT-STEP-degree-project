﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.HasKey("Id");

                    b.HasIndex("CartId")
                        .IsUnique();

                    b.HasIndex("CourierId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Core.Entities.Rate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Estimate")
                        .HasColumnType("real");

                    b.Property<string>("TargetUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("WareId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("TargetUserId");

                    b.HasIndex("WareId");

                    b.ToTable("Rates");
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

            modelBuilder.Entity("Core.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("TargetUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("WareId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("TargetUserId");

                    b.HasIndex("WareId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Core.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatorId")
                        .IsRequired()
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

                    b.Property<int?>("WareId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("WareId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Core.Entities.Ware", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableCount")
                        .HasColumnType("int");

                    b.Property<int?>("CartId")
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

                    b.HasIndex("CartId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Wares");
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
                            Id = "f008ba91-7336-4a6d-8217-5030db458fa5",
                            ConcurrencyStamp = "f008ba91-7336-4a6d-8217-5030db458fa5",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "81af2d7e-9df2-4f09-8131-72ffdab373b2",
                            ConcurrencyStamp = "81af2d7e-9df2-4f09-8131-72ffdab373b2",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
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
                            UserId = "24e4da3e-931b-4918-b259-85d174d8fcd3",
                            RoleId = "81af2d7e-9df2-4f09-8131-72ffdab373b2"
                        },
                        new
                        {
                            UserId = "22195e96-32e9-4740-bab9-6434e481f4f9",
                            RoleId = "f008ba91-7336-4a6d-8217-5030db458fa5"
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
                            Id = "24e4da3e-931b-4918-b259-85d174d8fcd3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e5a60e6a-9efc-4593-a916-f2c07fcafca0",
                            Email = "marylou@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "MARYLOU@GMAIL.COM",
                            NormalizedUserName = "MARYLOU@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAENvUrKeZM4WnQBC/azrdjuOIhu1B1SVOTRyypWv6YwjmfnLjR4d9zIZfe/A+/eyPcg==",
                            PhoneNumber = "+380986734245",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "634fcc81-9e99-425e-b0d2-b101ebe3a3ba",
                            TwoFactorEnabled = false,
                            UserName = "marylou@gmail.com",
                            Name = "Mary",
                            RegistrationDate = new DateTimeOffset(new DateTime(2022, 8, 12, 16, 49, 57, 716, DateTimeKind.Unspecified).AddTicks(5664), new TimeSpan(0, 0, 0, 0, 0)),
                            Surname = "Lou"
                        },
                        new
                        {
                            Id = "22195e96-32e9-4740-bab9-6434e481f4f9",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e14138cc-0197-46d6-a314-f4e02403ac52",
                            Email = "etsukomami@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ETSUKOMAMI@GMAIL.COM",
                            NormalizedUserName = "ETSUKOMAMI@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEPr9VxhZba26pGJwHMDWcXrEIE1/zPhE7OhFAzbQfYoVLMDaDpbKKAHlvZi9f48yTw==",
                            PhoneNumber = "+380988931245",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "cd8ff034-f309-470c-8895-1abe8a552771",
                            TwoFactorEnabled = false,
                            UserName = "etsukomami@gmail.com",
                            Name = "Etsuko",
                            RegistrationDate = new DateTimeOffset(new DateTime(2022, 8, 12, 16, 49, 57, 719, DateTimeKind.Unspecified).AddTicks(9342), new TimeSpan(0, 0, 0, 0, 0)),
                            Surname = "Mami"
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

            modelBuilder.Entity("Core.Entities.Rate", b =>
                {
                    b.HasOne("Core.Entities.User", "Creator")
                        .WithMany("Rates")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "TargetUser")
                        .WithMany()
                        .HasForeignKey("TargetUserId");

                    b.HasOne("Core.Entities.Ware", "Ware")
                        .WithMany("Rates")
                        .HasForeignKey("WareId");

                    b.Navigation("Creator");

                    b.Navigation("TargetUser");

                    b.Navigation("Ware");
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

            modelBuilder.Entity("Core.Entities.Report", b =>
                {
                    b.HasOne("Core.Entities.User", "Creator")
                        .WithMany("Reports")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "TargetUser")
                        .WithMany()
                        .HasForeignKey("TargetUserId");

                    b.HasOne("Core.Entities.Ware", "Ware")
                        .WithMany("Reports")
                        .HasForeignKey("WareId");

                    b.Navigation("Creator");

                    b.Navigation("TargetUser");

                    b.Navigation("Ware");
                });

            modelBuilder.Entity("Core.Entities.Review", b =>
                {
                    b.HasOne("Core.Entities.User", "Creator")
                        .WithMany("Reviews")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Ware", null)
                        .WithMany("Reviews")
                        .HasForeignKey("WareId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Core.Entities.Ware", b =>
                {
                    b.HasOne("Core.Entities.Cart", null)
                        .WithMany("Wares")
                        .HasForeignKey("CartId");

                    b.HasOne("Core.Entities.Category", "Category")
                        .WithMany("Wares")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "User")
                        .WithMany("Wares")
                        .HasForeignKey("CreatorId");

                    b.Navigation("Category");

                    b.Navigation("User");
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

                    b.Navigation("Wares");
                });

            modelBuilder.Entity("Core.Entities.Category", b =>
                {
                    b.Navigation("Wares");
                });

            modelBuilder.Entity("Core.Entities.Ware", b =>
                {
                    b.Navigation("Characteristics");

                    b.Navigation("Rates");

                    b.Navigation("Reports");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");

                    b.Navigation("Rates");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Reports");

                    b.Navigation("Reviews");

                    b.Navigation("Wares");
                });
#pragma warning restore 612, 618
        }
    }
}

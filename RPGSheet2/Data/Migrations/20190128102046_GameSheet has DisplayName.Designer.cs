﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RPGSheet2.Data;

namespace RPGSheet2.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190128102046_GameSheet has DisplayName")]
    partial class GameSheethasDisplayName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RPGSheet2.Models.Character", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsHidden");

                    b.Property<bool>("IsNPC");

                    b.Property<string>("Name");

                    b.Property<string>("OwnerID")
                        .IsRequired();

                    b.Property<int?>("gameID");

                    b.HasKey("ID");

                    b.HasIndex("gameID");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("RPGSheet2.Models.CharacterValue", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("characterID");

                    b.Property<int?>("gameSheetFieldID");

                    b.Property<bool>("value_bool");

                    b.Property<byte>("value_dropdown");

                    b.Property<float>("value_float");

                    b.Property<int>("value_int");

                    b.Property<int>("value_stat");

                    b.Property<string>("value_string");

                    b.HasKey("ID");

                    b.HasIndex("characterID");

                    b.HasIndex("gameSheetFieldID");

                    b.ToTable("CharacterValues");
                });

            modelBuilder.Entity("RPGSheet2.Models.DropdownValue", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Key");

                    b.Property<string>("Value");

                    b.Property<int?>("gameSheetFieldID");

                    b.HasKey("ID");

                    b.HasIndex("gameSheetFieldID");

                    b.ToTable("DropdownValues");
                });

            modelBuilder.Entity("RPGSheet2.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.Property<string>("OwnerID")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("SearchID");

                    b.Property<int?>("gameSheetID");

                    b.HasKey("ID");

                    b.HasIndex("gameSheetID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("RPGSheet2.Models.GameAccess", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("UserID")
                        .IsRequired();

                    b.Property<string>("UserName");

                    b.Property<int>("gameID");

                    b.HasKey("ID");

                    b.HasIndex("gameID");

                    b.ToTable("GameAccesses");
                });

            modelBuilder.Entity("RPGSheet2.Models.GameMessage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Message");

                    b.Property<string>("SenderID");

                    b.Property<DateTime>("SentTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 1, 28, 10, 20, 46, 307, DateTimeKind.Utc));

                    b.Property<int>("gameID");

                    b.HasKey("ID");

                    b.HasIndex("gameID");

                    b.ToTable("GameMessages");
                });

            modelBuilder.Entity("RPGSheet2.Models.GameSheet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayName");

                    b.Property<double>("Version");

                    b.Property<int?>("originalSheetID");

                    b.HasKey("ID");

                    b.HasIndex("originalSheetID");

                    b.ToTable("GameSheet");
                });

            modelBuilder.Entity("RPGSheet2.Models.GameSheetField", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayName");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsDropdown");

                    b.Property<int?>("OriginalFieldID");

                    b.Property<int?>("gameSheetID");

                    b.Property<float>("height");

                    b.Property<string>("hexColour");

                    b.Property<bool>("value_bool");

                    b.Property<float>("value_float");

                    b.Property<int>("value_int");

                    b.Property<int>("value_stat");

                    b.Property<int>("value_stat_divisor");

                    b.Property<int>("value_stat_midpoint");

                    b.Property<string>("value_string");

                    b.Property<float>("width");

                    b.Property<float>("xpos");

                    b.Property<float>("ypos");

                    b.HasKey("ID");

                    b.HasIndex("OriginalFieldID");

                    b.HasIndex("gameSheetID");

                    b.ToTable("GameSheetFields");
                });

            modelBuilder.Entity("RPGSheet2.Models.Sheet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("OwnerID");

                    b.Property<string>("SheetName")
                        .IsRequired();

                    b.Property<double>("Version");

                    b.HasKey("ID");

                    b.ToTable("Sheets");
                });

            modelBuilder.Entity("RPGSheet2.Models.SheetField", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayName");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsDropdown");

                    b.Property<float>("height");

                    b.Property<string>("hexColour");

                    b.Property<int?>("sheetID");

                    b.Property<bool>("value_bool");

                    b.Property<float>("value_float");

                    b.Property<int>("value_int");

                    b.Property<int>("value_stat");

                    b.Property<int>("value_stat_divisor");

                    b.Property<int>("value_stat_midpoint");

                    b.Property<string>("value_string");

                    b.Property<float>("width");

                    b.Property<float>("xpos");

                    b.Property<float>("ypos");

                    b.HasKey("ID");

                    b.HasIndex("sheetID");

                    b.ToTable("SheetFields");
                });

            modelBuilder.Entity("RPGSheet2.Models.UnhideChar", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserID")
                        .IsRequired();

                    b.Property<int>("characterID");

                    b.HasKey("ID");

                    b.HasIndex("characterID");

                    b.ToTable("UnHideFor");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RPGSheet2.Models.Character", b =>
                {
                    b.HasOne("RPGSheet2.Models.Game", "game")
                        .WithMany("Characters")
                        .HasForeignKey("gameID");
                });

            modelBuilder.Entity("RPGSheet2.Models.CharacterValue", b =>
                {
                    b.HasOne("RPGSheet2.Models.Character", "character")
                        .WithMany("Values")
                        .HasForeignKey("characterID");

                    b.HasOne("RPGSheet2.Models.GameSheetField", "gameSheetField")
                        .WithMany()
                        .HasForeignKey("gameSheetFieldID");
                });

            modelBuilder.Entity("RPGSheet2.Models.DropdownValue", b =>
                {
                    b.HasOne("RPGSheet2.Models.GameSheetField", "gameSheetField")
                        .WithMany("DropdownValues")
                        .HasForeignKey("gameSheetFieldID");
                });

            modelBuilder.Entity("RPGSheet2.Models.Game", b =>
                {
                    b.HasOne("RPGSheet2.Models.GameSheet", "gameSheet")
                        .WithMany()
                        .HasForeignKey("gameSheetID");
                });

            modelBuilder.Entity("RPGSheet2.Models.GameAccess", b =>
                {
                    b.HasOne("RPGSheet2.Models.Game", "game")
                        .WithMany("Accesses")
                        .HasForeignKey("gameID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RPGSheet2.Models.GameMessage", b =>
                {
                    b.HasOne("RPGSheet2.Models.Game", "game")
                        .WithMany("Messages")
                        .HasForeignKey("gameID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RPGSheet2.Models.GameSheet", b =>
                {
                    b.HasOne("RPGSheet2.Models.Sheet", "originalSheet")
                        .WithMany()
                        .HasForeignKey("originalSheetID");
                });

            modelBuilder.Entity("RPGSheet2.Models.GameSheetField", b =>
                {
                    b.HasOne("RPGSheet2.Models.SheetField", "OriginalField")
                        .WithMany()
                        .HasForeignKey("OriginalFieldID");

                    b.HasOne("RPGSheet2.Models.GameSheet", "gameSheet")
                        .WithMany("Fields")
                        .HasForeignKey("gameSheetID");
                });

            modelBuilder.Entity("RPGSheet2.Models.SheetField", b =>
                {
                    b.HasOne("RPGSheet2.Models.Sheet", "sheet")
                        .WithMany("Fields")
                        .HasForeignKey("sheetID");
                });

            modelBuilder.Entity("RPGSheet2.Models.UnhideChar", b =>
                {
                    b.HasOne("RPGSheet2.Models.Character", "character")
                        .WithMany("UnHideFor")
                        .HasForeignKey("characterID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

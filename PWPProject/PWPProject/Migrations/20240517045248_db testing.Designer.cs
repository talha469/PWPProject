﻿// <auto-generated />
using System;
using DALayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace PWPProject.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240517045248_db testing")]
    partial class dbtesting
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BLLayer.ApplicationStats", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MostBookmarkedVideo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MostVotedVideo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalUsers")
                        .HasColumnType("int");

                    b.Property<int>("TotalVideos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("Common.BusinessEntities.BookMark", b =>
                {
                    b.Property<int>("BookMarkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookMarkId"), 1L, 1);

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("bookMarkDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("isBookMarked")
                        .HasColumnType("bit");

                    b.HasKey("BookMarkId");

                    b.HasIndex("Id");

                    b.ToTable("BookMark");
                });

            modelBuilder.Entity("Common.BusinessEntities.Video", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("AvailableResolutions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDismiss")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEncoded")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSellingVideo")
                        .HasColumnType("bit");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("VideoExtension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VideoUploadedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Video");
                });

            modelBuilder.Entity("CommonLibrary.BusinessEntities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<DateTime>("AccountCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CommonLibrary.BusinessEntities.Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoteId"), 1L, 1);

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("VoteDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VoteType")
                        .HasColumnType("int");

                    b.HasKey("VoteId");

                    b.HasIndex("Id");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Common.BusinessEntities.BookMark", b =>
                {
                    b.HasOne("Common.BusinessEntities.Video", "Video")
                        .WithMany("BookMark")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Video");
                });

            modelBuilder.Entity("Common.BusinessEntities.Video", b =>
                {
                    b.HasOne("CommonLibrary.BusinessEntities.User", "User")
                        .WithMany("UploadedVideos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommonLibrary.BusinessEntities.Vote", b =>
                {
                    b.HasOne("Common.BusinessEntities.Video", "Video")
                        .WithMany("Vote")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Video");
                });

            modelBuilder.Entity("Common.BusinessEntities.Video", b =>
                {
                    b.Navigation("BookMark");

                    b.Navigation("Vote");
                });

            modelBuilder.Entity("CommonLibrary.BusinessEntities.User", b =>
                {
                    b.Navigation("UploadedVideos");
                });
#pragma warning restore 612, 618
        }
    }
}

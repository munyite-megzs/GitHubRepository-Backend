﻿// <auto-generated />
using System;
using GitRepositoryTracker.DButil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GitRepositoryTracker.Migrations
{
    [DbContext(typeof(GitRepoContext))]
    [Migration("20230227082921_Initialdataset")]
    partial class Initialdataset
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GitRepositoryTracker.Models.Repository", b =>
                {
                    b.Property<string>("RepositoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ForksCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("PushedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RepositoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StargazersCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RepositoryId");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("Repositories");

                    b.HasData(
                        new
                        {
                            RepositoryId = "MGEwOlJlcG9zaXRvcnkyODQ1NzgyMz==",
                            CreatedAt = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "This is an example repository",
                            ForksCount = 2,
                            PushedAt = new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RepositoryName = "Example Repository 1",
                            StargazersCount = 10,
                            UpdatedAt = new DateTime(2022, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Url = "https://github.com/example/repository1",
                            language = "C#"
                        },
                        new
                        {
                            RepositoryId = "MGFyOlJlcG9zaXRvcnkyODQ1NzgyMt==",
                            CreatedAt = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "This is another example repository",
                            ForksCount = 1,
                            PushedAt = new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RepositoryName = "Example Repository 2",
                            StargazersCount = 5,
                            UpdatedAt = new DateTime(2022, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Url = "https://github.com/example/repository2",
                            language = "python"
                        });
                });

            modelBuilder.Entity("GitRepositoryTracker.Models.RepositoryTopic", b =>
                {
                    b.Property<string>("RepositoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("RepositoryId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("RepositoryTopics");

                    b.HasData(
                        new
                        {
                            RepositoryId = "MGEwOlJlcG9zaXRvcnkyODQ1NzgyMz==",
                            TopicId = -1
                        },
                        new
                        {
                            RepositoryId = "MGEwOlJlcG9zaXRvcnkyODQ1NzgyMz==",
                            TopicId = -2
                        },
                        new
                        {
                            RepositoryId = "MGFyOlJlcG9zaXRvcnkyODQ1NzgyMt==",
                            TopicId = -3
                        });
                });

            modelBuilder.Entity("GitRepositoryTracker.Models.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TopicId"));

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TopicId");

                    b.HasIndex("TopicName")
                        .IsUnique();

                    b.ToTable("Topics");

                    b.HasData(
                        new
                        {
                            TopicId = -1,
                            TopicName = "C#"
                        },
                        new
                        {
                            TopicId = -2,
                            TopicName = "javaScript"
                        },
                        new
                        {
                            TopicId = -3,
                            TopicName = "python"
                        },
                        new
                        {
                            TopicId = -4,
                            TopicName = "careers"
                        },
                        new
                        {
                            TopicId = -5,
                            TopicName = "certification"
                        },
                        new
                        {
                            TopicId = -6,
                            TopicName = "community"
                        },
                        new
                        {
                            TopicId = -7,
                            TopicName = "curriculum"
                        },
                        new
                        {
                            TopicId = -8,
                            TopicName = "education"
                        },
                        new
                        {
                            TopicId = -9,
                            TopicName = "freecodecamp"
                        },
                        new
                        {
                            TopicId = -10,
                            TopicName = "hacktoberfest"
                        },
                        new
                        {
                            TopicId = -12,
                            TopicName = "learn-to-code"
                        });
                });

            modelBuilder.Entity("GitRepositoryTracker.Models.RepositoryTopic", b =>
                {
                    b.HasOne("GitRepositoryTracker.Models.Repository", "Repository")
                        .WithMany("RepositoryTopics")
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GitRepositoryTracker.Models.Topic", "Topic")
                        .WithMany("RepositoryTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repository");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("GitRepositoryTracker.Models.Repository", b =>
                {
                    b.Navigation("RepositoryTopics");
                });

            modelBuilder.Entity("GitRepositoryTracker.Models.Topic", b =>
                {
                    b.Navigation("RepositoryTopics");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Eventador.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eventador.Data.Migrations.Migrations
{
    [DbContext(typeof(EventadorDbContext))]
    partial class EventadorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Eventador.Domain.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccessType")
                        .HasColumnName("access_type")
                        .HasColumnType("integer");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnName("additional_info")
                        .HasColumnType("text");

                    b.Property<int>("AuthorId")
                        .HasColumnName("author_id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnName("change_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("create_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnName("end_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EventStatus")
                        .HasColumnName("event_status")
                        .HasColumnType("integer");

                    b.Property<int>("EventType")
                        .HasColumnName("event_type")
                        .HasColumnType("integer");

                    b.Property<string[]>("ImageUrls")
                        .HasColumnName("image_urls")
                        .HasColumnType("text[]");

                    b.Property<float?>("Lat")
                        .HasColumnName("lat")
                        .HasColumnType("real");

                    b.Property<float?>("Lon")
                        .HasColumnName("lon")
                        .HasColumnType("real");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("numeric");

                    b.Property<int>("RegionId")
                        .HasColumnName("region_id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnName("start_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .HasColumnName("title")
                        .HasColumnType("text");

                    b.Property<string>("TitleImageUrl")
                        .HasColumnName("title_image_url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("events");
                });

            modelBuilder.Entity("Eventador.Domain.Mark", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Comment")
                        .HasColumnName("comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("create_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Value")
                        .HasColumnName("value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("marks");
                });

            modelBuilder.Entity("Eventador.Domain.Participation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("create_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("EventId")
                        .HasColumnName("event_id")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("participations");
                });

            modelBuilder.Entity("Eventador.Domain.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AboutInfo")
                        .HasColumnName("about_info")
                        .HasColumnType("text");

                    b.Property<bool>("Blocked")
                        .HasColumnName("blocked")
                        .HasColumnType("boolean");

                    b.Property<string>("Firstname")
                        .HasColumnName("first_name")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .HasColumnName("login")
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnName("middle_name")
                        .HasColumnType("text");

                    b.Property<string>("OrganizationName")
                        .HasColumnName("organization_name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasColumnType("text");

                    b.Property<int>("UserType")
                        .HasColumnName("user_type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}

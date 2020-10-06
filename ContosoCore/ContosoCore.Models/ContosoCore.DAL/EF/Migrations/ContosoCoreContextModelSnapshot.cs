﻿// <auto-generated />
using ContosoCore.DAL.EF;
using ContosoCore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ContosoCore.DAL.EF.Migrations
{
    [DbContext(typeof(ContosoCoreContext))]
    partial class ContosoCoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContosoCore.Models.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Credits");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime?>("FechaModificacion");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Title");

                    b.Property<string>("UsuarioCreacion");

                    b.Property<string>("UsuarioModificacion");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ContosoCore.Models.Entities.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseID");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime?>("FechaModificacion");

                    b.Property<int?>("Grade");

                    b.Property<int>("StudentID");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("UsuarioCreacion");

                    b.Property<string>("UsuarioModificacion");

                    b.HasKey("Id");

                    b.HasIndex("CourseID");

                    b.HasIndex("StudentID");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("ContosoCore.Models.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EmrollmentDate");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime?>("FechaModificacion");

                    b.Property<string>("FirstMidName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("UsuarioCreacion");

                    b.Property<string>("UsuarioModificacion");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ContosoCore.Models.Entities.Enrollment", b =>
                {
                    b.HasOne("ContosoCore.Models.Entities.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContosoCore.Models.Entities.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

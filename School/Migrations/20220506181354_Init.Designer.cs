﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using School.Data;

#nullable disable

namespace School.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220506181354_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ClassStudent", b =>
                {
                    b.Property<int>("ClassesClassID")
                        .HasColumnType("int");

                    b.Property<int>("StudentsStudentID")
                        .HasColumnType("int");

                    b.HasKey("ClassesClassID", "StudentsStudentID");

                    b.HasIndex("StudentsStudentID");

                    b.ToTable("ClassStudent");
                });

            modelBuilder.Entity("ClassTeacher", b =>
                {
                    b.Property<int>("ClassesClassID")
                        .HasColumnType("int");

                    b.Property<int>("TeachersTeacherID")
                        .HasColumnType("int");

                    b.HasKey("ClassesClassID", "TeachersTeacherID");

                    b.HasIndex("TeachersTeacherID");

                    b.ToTable("ClassTeacher");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<int>("CoursesCourseID")
                        .HasColumnType("int");

                    b.Property<int>("StudentsStudentID")
                        .HasColumnType("int");

                    b.HasKey("CoursesCourseID", "StudentsStudentID");

                    b.HasIndex("StudentsStudentID");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("CourseTeacher", b =>
                {
                    b.Property<int>("CoursesCourseID")
                        .HasColumnType("int");

                    b.Property<int>("TeachersTeacherID")
                        .HasColumnType("int");

                    b.HasKey("CoursesCourseID", "TeachersTeacherID");

                    b.HasIndex("TeachersTeacherID");

                    b.ToTable("CourseTeacher");
                });

            modelBuilder.Entity("School.Model.Class", b =>
                {
                    b.Property<int>("ClassID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassID"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ClassID");

                    b.ToTable("Classes");

                    b.HasData(
                        new
                        {
                            ClassID = 1,
                            Title = "1A"
                        },
                        new
                        {
                            ClassID = 2,
                            Title = "1B"
                        },
                        new
                        {
                            ClassID = 3,
                            Title = "2A"
                        },
                        new
                        {
                            ClassID = 4,
                            Title = "2B"
                        });
                });

            modelBuilder.Entity("School.Model.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseID"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CourseID");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseID = 1,
                            Title = "Software Development 1"
                        },
                        new
                        {
                            CourseID = 2,
                            Title = "Software Development 2"
                        },
                        new
                        {
                            CourseID = 3,
                            Title = "Software Development 3"
                        },
                        new
                        {
                            CourseID = 4,
                            Title = "Mathematics 1"
                        },
                        new
                        {
                            CourseID = 5,
                            Title = "English 1"
                        },
                        new
                        {
                            CourseID = 6,
                            Title = "English 2"
                        });
                });

            modelBuilder.Entity("School.Model.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentID"), 1L, 1);

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StudentID");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentID = 1,
                            Email = "example@mail.com",
                            Name = "Simon",
                            PhoneNumber = "999",
                            Surname = "Johansson"
                        },
                        new
                        {
                            StudentID = 2,
                            Email = "example@mail.com",
                            Name = "Buddy",
                            PhoneNumber = "999",
                            Surname = "Johansson"
                        },
                        new
                        {
                            StudentID = 3,
                            Email = "example@mail.com",
                            Name = "Rebecca",
                            PhoneNumber = "999",
                            Surname = "Gerdin"
                        },
                        new
                        {
                            StudentID = 4,
                            Email = "example@mail.com",
                            Name = "Oskar",
                            PhoneNumber = "999",
                            Surname = "Björkman"
                        });
                });

            modelBuilder.Entity("School.Model.Teacher", b =>
                {
                    b.Property<int>("TeacherID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherID"), 1L, 1);

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TeacherID");

                    b.ToTable("Teachers");

                    b.HasData(
                        new
                        {
                            TeacherID = 1,
                            Email = "example@mail.com",
                            Name = "Elon",
                            PhoneNumber = "999",
                            Surname = "Musk"
                        },
                        new
                        {
                            TeacherID = 2,
                            Email = "example@mail.com",
                            Name = "Tobias",
                            PhoneNumber = "999",
                            Surname = "Landén"
                        },
                        new
                        {
                            TeacherID = 3,
                            Email = "example@mail.com",
                            Name = "Anas",
                            PhoneNumber = "999",
                            Surname = "Alhussain"
                        },
                        new
                        {
                            TeacherID = 4,
                            Email = "example@mail.com",
                            Name = "Reidar",
                            PhoneNumber = "999",
                            Surname = "Nilsen"
                        });
                });

            modelBuilder.Entity("ClassStudent", b =>
                {
                    b.HasOne("School.Model.Class", null)
                        .WithMany()
                        .HasForeignKey("ClassesClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("School.Model.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassTeacher", b =>
                {
                    b.HasOne("School.Model.Class", null)
                        .WithMany()
                        .HasForeignKey("ClassesClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("School.Model.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersTeacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("School.Model.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("School.Model.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseTeacher", b =>
                {
                    b.HasOne("School.Model.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("School.Model.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersTeacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
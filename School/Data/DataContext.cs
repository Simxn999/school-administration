using Microsoft.EntityFrameworkCore;
using School.Model;

namespace School.Data;

public class DataContext : DbContext
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Course> Courses => Set<Course>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source = SIMXN\\SQLEXPRESS; Initial Catalog = School; Integrated Security = True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Classes

        modelBuilder.Entity<Class>().HasData(new Class { ClassID = 1, Title = "1A", });
        modelBuilder.Entity<Class>().HasData(new Class { ClassID = 2, Title = "1B", });
        modelBuilder.Entity<Class>().HasData(new Class { ClassID = 3, Title = "2A", });
        modelBuilder.Entity<Class>().HasData(new Class { ClassID = 4, Title = "2B", });

        // Teachers

        modelBuilder.Entity<Teacher>()
                    .HasData(new Teacher
                    {
                        TeacherID = 1,
                        Name = "Elon",
                        Surname = "Musk",
                        Email = "example@mail.com",
                        PhoneNumber = "999",
                    });
        modelBuilder.Entity<Teacher>()
                    .HasData(new Teacher
                    {
                        TeacherID = 2,
                        Name = "Tobias",
                        Surname = "Landén",
                        Email = "example@mail.com",
                        PhoneNumber = "999",
                    });
        modelBuilder.Entity<Teacher>()
                    .HasData(new Teacher
                    {
                        TeacherID = 3,
                        Name = "Anas",
                        Surname = "Alhussain",
                        Email = "example@mail.com",
                        PhoneNumber = "999",
                    });
        modelBuilder.Entity<Teacher>()
                    .HasData(new Teacher
                    {
                        TeacherID = 4,
                        Name = "Reidar",
                        Surname = "Nilsen",
                        Email = "example@mail.com",
                        PhoneNumber = "999",
                    });

        // Courses

        modelBuilder.Entity<Course>().HasData(new Course { CourseID = 1, Title = "Software Development 1", });
        modelBuilder.Entity<Course>().HasData(new Course { CourseID = 2, Title = "Software Development 2", });
        modelBuilder.Entity<Course>().HasData(new Course { CourseID = 3, Title = "Software Development 3", });
        modelBuilder.Entity<Course>().HasData(new Course { CourseID = 4, Title = "Mathematics 1", });
        modelBuilder.Entity<Course>().HasData(new Course { CourseID = 5, Title = "English 1", });
        modelBuilder.Entity<Course>().HasData(new Course { CourseID = 6, Title = "English 2", });

        // Students

        modelBuilder.Entity<Student>()
                    .HasData(new Student
                    {
                        StudentID = 1,
                        Name = "Simon",
                        Surname = "Johansson",
                        Email = "example@mail.com",
                        PhoneNumber = "999",
                    });
        modelBuilder.Entity<Student>()
                    .HasData(new Student
                    {
                        StudentID = 2,
                        Name = "Buddy",
                        Surname = "Johansson",
                        Email = "example@mail.com",
                        PhoneNumber = "999",
                    });
        modelBuilder.Entity<Student>()
                    .HasData(new Student
                    {
                        StudentID = 3,
                        Name = "Rebecca",
                        Surname = "Gerdin",
                        Email = "example@mail.com",
                        PhoneNumber = "999",
                    });
        modelBuilder.Entity<Student>()
                    .HasData(new Student
                    {
                        StudentID = 4,
                        Name = "Oskar",
                        Surname = "Björkman",
                        Email = "example@mail.com",
                        PhoneNumber = "999",
                    });
    }
}
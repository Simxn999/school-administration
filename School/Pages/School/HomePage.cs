using Microsoft.EntityFrameworkCore;
using School.Data;

namespace School.Pages;

public class HomePage : Page
{
    readonly DataContext _context;

    public HomePage()
    {
        Console.Title = "School";
        Console.WriteLine("Loading...");

        _context = new DataContext();
        _context.Classes.Load();
        _context.Courses.Load();
        _context.Students.Load();
        _context.Teachers.Load();
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("Courses", OptionCourses);
        Options.Add("Classes", OptionClasses);
        Options.Add("Teachers", OptionTeachers);
        Options.Add("Students", OptionStudents);
        Options.Add("Manage School", OptionAdmin);
    }

    void OptionCourses()
    {
        new SelectCoursePage(_context).Run();
    }

    void OptionClasses()
    {
        new SelectClassPage(_context).Run();
    }

    void OptionTeachers()
    {
        new SelectTeacherPage(_context).Run();
    }

    void OptionStudents()
    {
        new SelectStudentPage(_context).Run();
    }

    void OptionAdmin()
    {
        new AdminPage(_context).Run();
    }
}
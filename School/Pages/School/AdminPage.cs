using School.Data;

namespace School.Pages;

public class AdminPage : Page
{
    readonly DataContext _context;

    public AdminPage(DataContext context)
    {
        _context = context;

        Title = "School Management";
    }

    protected override void UpdateOptions()
    {
        Options.Clear();
        Options.Add("Manage Courses", OptionCourses);
        Options.Add("Manage Classes", OptionClasses);
        Options.Add("Manage Teachers", OptionTeachers);
        Options.Add("Manage Students", OptionStudents);
    }

    void OptionCourses()
    {
        new ManageCoursesPage(_context).Run();
    }

    void OptionClasses()
    {
        new ManageClassesPage(_context).Run();
    }

    void OptionTeachers()
    {
        new ManageTeachersPage(_context).Run();
    }

    void OptionStudents()
    {
        new ManageStudentsPage(_context).Run();
    }
}
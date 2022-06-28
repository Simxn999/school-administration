using System.Text;
using School.Data;
using School.Model;
using static System.Console;

namespace School.Pages;

public class ManageCoursePage : Page
{
    readonly DataContext _context;
    readonly Course _course;

    public ManageCoursePage(DataContext context, int courseID)
    {
        _context = context;
        _course = _context.Courses.First(c => c.CourseID == courseID);

        Title = "Manage Course";

        Content = () => CoursePage.CourseContent(_context, courseID);
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("Change Title", OptionTitle);
        Options.Add("Add Teacher", OptionTeacherAdd);
        Options.Add("Remove Teacher", OptionTeacherRemove);
        Options.Add("Add Student", OptionStudentAdd);
        Options.Add("Remove Student", OptionStudentRemove);
        Options.Add("#delete#Remove Course", OptionCourseRemove);
    }

    void OptionTitle()
    {
        SetCursorPosition(Positions["Title"].Item1, Positions["Title"].Item2);
        Write(new StringBuilder().Insert(0, " ", (int)(WindowWidth * 0.75) - CursorLeft - 1).ToString());
        SetCursorPosition(Positions["Title"].Item1, Positions["Title"].Item2);
        var title = ReadLine()?.Trim() ?? "";
        if (!title.Any())
            return;

        _course.Title = title;
        _context.SaveChanges();
    }

    void OptionTeacherAdd()
    {
        var teachers = _context.Teachers.Where(t => !_course.Teachers!.Contains(t)).Select(t => new { t.TeacherID, t.FullName, }).ToList();
        if (!teachers.Any())
        {
            PrintError("There are no available teachers.");
            return;
        }

        var selectionPage = new SelectionPage(teachers.OrderBy(t => t.FullName).ToDictionary(key => key.TeacherID, value => value.FullName),
                                              "Select Teacher");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _course.Teachers!.Add(_context.Teachers.First(t => t.TeacherID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionTeacherRemove()
    {
        var teachers = _course.Teachers!.Select(t => new { t.TeacherID, t.FullName, }).ToList();
        if (!teachers.Any())
        {
            PrintError("There are no available teachers.");
            return;
        }

        var selectionPage = new SelectionPage(teachers.OrderBy(t => t.FullName).ToDictionary(key => key.TeacherID, value => value.FullName),
                                              "Select Teacher");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _course.Teachers!.Remove(_context.Teachers.First(t => t.TeacherID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionStudentAdd()
    {
        var students = _context.Students.Where(s => !_course.Students!.Contains(s)).Select(s => new { s.StudentID, s.FullName, }).ToList();
        if (!students.Any())
        {
            PrintError("There are no available students.");
            return;
        }

        var selectionPage = new SelectionPage(students.OrderBy(s => s.FullName).ToDictionary(key => key.StudentID, value => value.FullName),
                                              "Select Student");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _course.Students!.Add(_context.Students.First(s => s.StudentID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionStudentRemove()
    {
        var students = _course.Students!.Select(s => new { s.StudentID, s.FullName, }).ToList();
        if (!students.Any())
        {
            PrintError("There are no available students.");
            return;
        }

        var selectionPage = new SelectionPage(students.OrderBy(s => s.FullName).ToDictionary(key => key.StudentID, value => value.FullName),
                                              "Select Student");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _course.Students!.Remove(_context.Students.First(s => s.StudentID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionCourseRemove()
    {
        var page = new RemoveCoursePage(_context, _course.CourseID);
        page.Run();
        if (page.ReturnValue == 1)
            Exit = true;
    }
}
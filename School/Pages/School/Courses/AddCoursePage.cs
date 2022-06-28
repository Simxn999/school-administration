using System.Text;
using School.Data;
using School.Model;
using static System.Console;

namespace School.Pages;

public class AddCoursePage : Page
{
    readonly DataContext _context;
    readonly Course _course;

    public AddCoursePage(DataContext context)
    {
        _context = context;
        _course = new Course
        {
            Title = "",
            Teachers = new List<Teacher>(),
            Students = new List<Student>(),
        };

        Title = "Add Course";

        Content = CourseContent;
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("Set Title", OptionTitle);
        Options.Add("Add Teacher", OptionTeacherAdd);
        Options.Add("Remove Teacher", OptionTeacherRemove);
        Options.Add("Add Student", OptionStudentAdd);
        Options.Add("Remove Student", OptionStudentRemove);
        Options.Add("#enter#Confirm Course", OptionConfirm);
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
    }

    void OptionStudentAdd()
    {
        var students = _context.Students.Where(s => !_course.Students!.Contains(s)).Select(t => new { t.StudentID, t.FullName, }).ToList();
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
    }

    void OptionStudentRemove()
    {
        var students = _course.Students!.Select(t => new { t.StudentID, t.FullName, }).ToList();
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
    }

    void OptionConfirm()
    {
        try
        {
            if (!_course.Title.Any())
            {
                PrintError("Title has to be set!");
                return;
            }

            var newCourse = _context.Courses.Add(new Course
                                    {
                                        Title = _course.Title,
                                        Teachers = _course.Teachers,
                                        Students = _course.Students,
                                    })
                                    .Entity;
            _context.SaveChanges();

            new ManageCoursePage(_context, newCourse.CourseID).Run();
            Exit = true;
        }
        catch (Exception)
        {
            PrintError("Something went wrong!");
        }
    }

    void CourseContent()
    {
        Positions.Clear();

        PrintData("Title:", _course.Title);

        PrintDataList("Teachers:", _course.Teachers!.Select(s => s.FullName).ToList());

        PrintDataList("Students:", _course.Students!.Select(s => s.FullName).ToList());
    }
}
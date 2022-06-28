using System.Text;
using School.Data;
using School.Model;
using static System.Console;

namespace School.Pages;

public class ManageStudentPage : Page
{
    readonly DataContext _context;
    readonly Student _student;

    public ManageStudentPage(DataContext context, int studentID)
    {
        _context = context;
        _student = _context.Students.First(c => c.StudentID == studentID);
        _context.Entry(_student).Collection("Classes").Load();
        _context.Entry(_student).Collection("Courses").Load();

        Title = "Manage Student";

        Content = () => StudentPage.StudentContent(_context, studentID);
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("Change Name", OptionName);
        Options.Add("Change Surname", OptionSurname);
        Options.Add("Change Phone Number", OptionPhone);
        Options.Add("Change Email", OptionEmail);
        Options.Add("Add Class", OptionClassAdd);
        Options.Add("Remove Class", OptionClassRemove);
        Options.Add("Add Course", OptionCourseAdd);
        Options.Add("Remove Course", OptionCourseRemove);
        Options.Add("#delete#Remove Student", OptionStudentRemove);
    }

    void OptionName()
    {
        SetCursorPosition(Positions["Name"].Item1, Positions["Name"].Item2);
        Write(new StringBuilder().Insert(0, " ", (int)(WindowWidth * 0.75) - CursorLeft - 1).ToString());
        SetCursorPosition(Positions["Name"].Item1, Positions["Name"].Item2);
        var name = ReadLine()?.Trim() ?? "";
        if (!name.Any())
            return;

        _student.Name = name;
        _context.SaveChanges();
    }

    void OptionSurname()
    {
        SetCursorPosition(Positions["Surname"].Item1, Positions["Surname"].Item2);
        Write(new StringBuilder().Insert(0, " ", (int)(WindowWidth * 0.75) - CursorLeft - 1).ToString());
        SetCursorPosition(Positions["Surname"].Item1, Positions["Surname"].Item2);
        var surname = ReadLine()?.Trim() ?? "";
        if (!surname.Any())
            return;

        _student.Surname = surname;
        _context.SaveChanges();
    }

    void OptionPhone()
    {
        SetCursorPosition(Positions["Phone"].Item1, Positions["Phone"].Item2);
        Write(new StringBuilder().Insert(0, " ", (int)(WindowWidth * 0.75) - CursorLeft - 1).ToString());
        SetCursorPosition(Positions["Phone"].Item1, Positions["Phone"].Item2);
        var name = ReadLine()?.Trim() ?? "";
        if (!name.Any())
            return;

        _student.PhoneNumber = name;
        _context.SaveChanges();
    }

    void OptionEmail()
    {
        SetCursorPosition(Positions["Email"].Item1, Positions["Email"].Item2);
        Write(new StringBuilder().Insert(0, " ", (int)(WindowWidth * 0.75) - CursorLeft - 1).ToString());
        SetCursorPosition(Positions["Email"].Item1, Positions["Email"].Item2);
        var email = ReadLine()?.Trim() ?? "";
        if (!email.Any())
            return;

        _student.Email = email;
        _context.SaveChanges();
    }

    void OptionClassAdd()
    {
        var classes = _context.Classes.Where(c => !c.Students!.Contains(_student)).Select(c => new { c.ClassID, c.Title, }).ToList();
        if (!classes.Any())
        {
            PrintError("There are no available classes.");
            return;
        }

        var selectionPage = new SelectionPage(classes.OrderBy(s => s.Title).ToDictionary(key => key.ClassID, value => value.Title), "Select Class");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _student.Classes!.Add(_context.Classes.First(c => c.ClassID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionClassRemove()
    {
        var classes = _student.Classes!.Select(c => new { c.ClassID, c.Title, }).ToList();
        if (!classes.Any())
        {
            PrintError("There are no available classes.");
            return;
        }

        var selectionPage = new SelectionPage(classes.OrderBy(s => s.Title).ToDictionary(key => key.ClassID, value => value.Title), "Select Class");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _student.Classes!.Remove(_context.Classes.First(c => c.ClassID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionCourseAdd()
    {
        var courses = _context.Courses.Where(c => !c.Students!.Contains(_student)).Select(c => new { c.CourseID, c.Title, }).ToList();
        if (!courses.Any())
        {
            PrintError("There are no available courses.");
            return;
        }

        var selectionPage = new SelectionPage(courses.OrderBy(s => s.Title).ToDictionary(key => key.CourseID, value => value.Title), "Select Course");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _student.Courses!.Add(_context.Courses.First(c => c.CourseID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionCourseRemove()
    {
        var courses = _student.Courses!.Select(c => new { c.CourseID, c.Title, }).ToList();
        if (!courses.Any())
        {
            PrintError("There are no available courses.");
            return;
        }

        var selectionPage = new SelectionPage(courses.OrderBy(s => s.Title).ToDictionary(key => key.CourseID, value => value.Title), "Select Course");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _student.Courses!.Remove(_context.Courses.First(c => c.CourseID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionStudentRemove()
    {
        var page = new RemoveStudentPage(_context, _student.StudentID);
        page.Run();
        if (page.ReturnValue == 1)
            Exit = true;
    }
}
using System.Text;
using School.Data;
using School.Model;
using static System.Console;

namespace School.Pages;

public class ManageTeacherPage : Page
{
    readonly DataContext _context;
    readonly Teacher _teacher;

    public ManageTeacherPage(DataContext context, int teacherID)
    {
        _context = context;
        _teacher = _context.Teachers.First(c => c.TeacherID == teacherID);
        _context.Entry(_teacher).Collection("Classes").Load();
        _context.Entry(_teacher).Collection("Courses").Load();

        Title = "Manage Teacher";

        Content = () => TeacherPage.TeacherContent(_context, teacherID);
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
        Options.Add("#delete#Remove Teacher", OptionTeacherRemove);
    }

    void OptionName()
    {
        SetCursorPosition(Positions["Name"].Item1, Positions["Name"].Item2);
        Write(new StringBuilder().Insert(0, " ", (int)(WindowWidth * 0.75) - CursorLeft - 1).ToString());
        SetCursorPosition(Positions["Name"].Item1, Positions["Name"].Item2);
        var name = ReadLine()?.Trim() ?? "";
        if (!name.Any())
            return;

        _teacher.Name = name;
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

        _teacher.Surname = surname;
        _context.SaveChanges();
    }

    void OptionPhone()
    {
        SetCursorPosition(Positions["Phone"].Item1, Positions["Phone"].Item2);
        Write(new StringBuilder().Insert(0, " ", (int)(WindowWidth * 0.75) - CursorLeft - 1).ToString());
        SetCursorPosition(Positions["Phone"].Item1, Positions["Phone"].Item2);
        var phone = ReadLine()?.Trim() ?? "";
        if (!phone.Any())
            return;

        _teacher.PhoneNumber = phone;
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

        _teacher.Email = email;
        _context.SaveChanges();
    }

    void OptionClassAdd()
    {
        var classes = _context.Classes.Where(c => !c.Teachers!.Contains(_teacher)).Select(c => new { c.ClassID, c.Title, }).ToList();
        if (!classes.Any())
        {
            PrintError("There are no available classes.");
            return;
        }

        var selectionPage = new SelectionPage(classes.OrderBy(s => s.Title).ToDictionary(key => key.ClassID, value => value.Title), "Select Class");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _teacher.Classes!.Add(_context.Classes.First(c => c.ClassID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionClassRemove()
    {
        var classes = _teacher.Classes!.Select(c => new { c.ClassID, c.Title, }).ToList();
        if (!classes.Any())
        {
            PrintError("There are no available classes.");
            return;
        }

        var selectionPage = new SelectionPage(classes.OrderBy(s => s.Title).ToDictionary(key => key.ClassID, value => value.Title), "Select Class");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _teacher.Classes!.Remove(_context.Classes.First(c => c.ClassID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionCourseAdd()
    {
        var courses = _context.Courses.Where(c => !_teacher.Courses!.Contains(c)).Select(c => new { c.CourseID, c.Title, }).ToList();
        if (!courses.Any())
        {
            PrintError("There are no available courses.");
            return;
        }

        var selectionPage = new SelectionPage(courses.OrderBy(s => s.Title).ToDictionary(key => key.CourseID, value => value.Title), "Select Course");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _teacher.Courses!.Add(_context.Courses.First(c => c.CourseID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionCourseRemove()
    {
        var courses = _teacher.Courses!.Select(c => new { c.CourseID, c.Title, }).ToList();
        if (!courses.Any())
        {
            PrintError("There are no available courses.");
            return;
        }

        var selectionPage = new SelectionPage(courses.OrderBy(s => s.Title).ToDictionary(key => key.CourseID, value => value.Title), "Select Course");
        selectionPage.Run();
        if (selectionPage.ReturnValue == 0)
            return;

        _teacher.Courses!.Remove(_context.Courses.First(c => c.CourseID == selectionPage.ReturnValue));
        _context.SaveChanges();
    }

    void OptionTeacherRemove()
    {
        var page = new RemoveTeacherPage(_context, _teacher.TeacherID);
        page.Run();
        if (page.ReturnValue == 1)
            Exit = true;
    }
}
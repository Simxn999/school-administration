using System.Text;
using School.Data;
using School.Model;
using static System.Console;

namespace School.Pages;

public class AddStudentPage : Page
{
    readonly DataContext _context;
    readonly Student _student;

    public AddStudentPage(DataContext context)
    {
        _context = context;
        _student = new Student
        {
            Name = "",
            Surname = "",
            PhoneNumber = null,
            Email = null,
            Classes = new List<Class>(),
            Courses = new List<Course>(),
        };

        Title = "Add Student";

        Content = StudentContent;
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("Set Name", OptionName);
        Options.Add("Set Surname", OptionSurname);
        Options.Add("Set Phone Number", OptionPhone);
        Options.Add("Set Email", OptionEmail);
        Options.Add("Add Class", OptionClassAdd);
        Options.Add("Remove Class", OptionClassRemove);
        Options.Add("Add Course", OptionCourseAdd);
        Options.Add("Remove Course", OptionCourseRemove);
        Options.Add("#enter#Confirm Student", OptionConfirm);
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
        var phone = ReadLine()?.Trim() ?? "";
        if (!phone.Any())
            return;

        _student.PhoneNumber = phone;
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
        var classes = _context.Classes.Where(c => !_student.Classes!.Contains(c)).Select(c => new { c.ClassID, c.Title, }).ToList();
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
        var courses = _context.Courses.Where(c => !_student.Courses!.Contains(c)).Select(c => new { c.CourseID, c.Title, }).ToList();
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

    void OptionConfirm()
    {
        try
        {
            if (!_student.Name.Any() || !_student.Surname.Any())
            {
                PrintError("Name has to be set!");
                return;
            }

            var newStudent = _context.Students.Add(new Student
                                     {
                                         Name = _student.Name,
                                         Surname = _student.Surname,
                                         PhoneNumber = _student.PhoneNumber,
                                         Email = _student.Email,
                                         Classes = _student.Classes,
                                         Courses = _student.Courses,
                                     })
                                     .Entity;
            _context.SaveChanges();

            new ManageStudentPage(_context, newStudent.StudentID).Run();
            Exit = true;
        }
        catch (Exception)
        {
            PrintError("Something went wrong!");
        }
    }

    void StudentContent()
    {
        Positions.Clear();

        PrintPerson(_student);

        PrintDataList("Classes:", _student.Classes!.Select(c => c.Title).ToList());

        PrintDataList("Courses:", _student.Courses!.Select(c => c.Title).ToList());

        PrintDataList("Teachers:",
                      _context.Teachers.Where(teacher => _student.Classes!.Any(@class => @class.Teachers!.Contains(teacher)) ||
                                                         _student.Courses!.Any(course => course.Teachers!.Contains(teacher)))
                              .Select(teacher => teacher.FullName)
                              .ToList());
    }
}
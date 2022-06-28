using System.Text;
using School.Data;
using School.Model;
using static System.Console;

namespace School.Pages;

public class AddClassPage : Page
{
    readonly Class _class;
    readonly DataContext _context;

    public AddClassPage(DataContext context)
    {
        _context = context;
        _class = new Class
        {
            Title = "",
            Students = new List<Student>(),
            Teachers = new List<Teacher>(),
        };

        Title = "Add Class";

        Content = ClassContent;
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("Set Title", OptionTitle);
        Options.Add("Add Teacher", OptionTeacherAdd);
        Options.Add("Remove Teacher", OptionTeacherRemove);
        Options.Add("Add Student", OptionStudentAdd);
        Options.Add("Remove Student", OptionStudentRemove);
        Options.Add("#enter#Confirm Class", OptionConfirm);
    }

    void OptionTitle()
    {
        SetCursorPosition(Positions["Title"].Item1, Positions["Title"].Item2);
        Write(new StringBuilder().Insert(0, " ", (int)(WindowWidth * 0.75) - CursorLeft - 1).ToString());
        SetCursorPosition(Positions["Title"].Item1, Positions["Title"].Item2);
        var title = ReadLine()?.Trim() ?? "";
        if (!title.Any())
            return;

        _class.Title = title;
    }

    void OptionTeacherAdd()
    {
        var teachers = _context.Teachers.Where(t => !_class.Teachers!.Contains(t)).ToList();
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

        _class.Teachers!.Add(teachers.First(t => t.TeacherID == selectionPage.ReturnValue));
    }

    void OptionTeacherRemove()
    {
        var teachers = _class.Teachers!.ToList();
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

        _class.Teachers!.Remove(teachers.First(t => t.TeacherID == selectionPage.ReturnValue));
    }

    void OptionStudentAdd()
    {
        var students = _context.Students.Where(s => !_class.Students!.Contains(s)).ToList();
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

        _class.Students!.Add(students.First(s => s.StudentID == selectionPage.ReturnValue));
    }

    void OptionStudentRemove()
    {
        var students = _class.Students!.ToList();
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

        _class.Students!.Remove(students.First(s => s.StudentID == selectionPage.ReturnValue));
    }

    void OptionConfirm()
    {
        try
        {
            if (!_class.Title.Any())
            {
                PrintError("Title has to be set!");
                return;
            }

            var newClass = _context.Classes.Add(new Class
                                   {
                                       Title = _class.Title,
                                       Teachers = _class.Teachers,
                                       Students = _class.Students,
                                   })
                                   .Entity;
            _context.SaveChanges();

            new ManageClassPage(_context, newClass.ClassID).Run();
            Exit = true;
        }
        catch (Exception)
        {
            PrintError("Something went wrong!");
        }
    }

    void ClassContent()
    {
        Positions.Clear();

        PrintData("Title:", _class.Title);

        PrintDataList("Teachers:", _class.Teachers!.Select(s => s.FullName).ToList());

        PrintDataList("Students:", _class.Students!.Select(s => s.FullName).ToList());
    }
}
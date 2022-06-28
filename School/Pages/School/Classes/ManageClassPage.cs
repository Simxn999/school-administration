using System.Text;
using School.Data;
using School.Model;
using static System.Console;

namespace School.Pages;

public class ManageClassPage : Page
{
    readonly Class _class;
    readonly DataContext _context;

    public ManageClassPage(DataContext context, int classID)
    {
        _context = context;
        _class = _context.Classes.First(c => c.ClassID == classID);

        Title = "Manage Class";

        Content = () => ClassPage.ClassContent(_context, classID);
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("Change Title", OptionTitle);
        Options.Add("Add Teacher", OptionTeacherAdd);
        Options.Add("Remove Teacher", OptionTeacherRemove);
        Options.Add("Add Student", OptionStudentAdd);
        Options.Add("Remove Student", OptionStudentRemove);
        Options.Add("#delete#Remove Class", OptionClassRemove);
    }

    void OptionTitle()
    {
        SetCursorPosition(Positions["Title"].Item1, Positions["Title"].Item2);
        Write(new StringBuilder().Insert(0, " ", (int)(WindowWidth * 0.75) - CursorLeft - 1).ToString());
        SetCursorPosition(Positions["Title"].Item1, Positions["Title"].Item2);
        var title = ReadLine() ?? "";
        if (title.Trim().Length == 0)
        {
            _class.Title = "[UNDEFINED]";
            return;
        }

        _class.Title = title.Trim();
        _context.SaveChanges();
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
        _context.SaveChanges();
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
        _context.SaveChanges();
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
        _context.SaveChanges();
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
        _context.SaveChanges();
    }

    void OptionClassRemove()
    {
        var page = new RemoveClassPage(_context, _class.ClassID);
        page.Run();
        if (page.ReturnValue == 1)
            Exit = true;
    }
}
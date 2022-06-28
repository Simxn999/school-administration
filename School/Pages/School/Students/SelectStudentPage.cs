using School.Data;

namespace School.Pages;

public class SelectStudentPage : Page
{
    readonly DataContext _context;
    readonly string _state;

    public SelectStudentPage(DataContext context, string state = "default")
    {
        _context = context;
        _state = state;

        Title = "Select Student:";
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        switch (_state)
        {
            case "manage":
                foreach (var student in _context.Students)
                    Options.Add(student.FullName, () => { new ManageStudentPage(_context, student.StudentID).Run(); });
                break;

            case "remove":
                foreach (var student in _context.Students)
                    Options.Add(student.FullName, () => { new RemoveStudentPage(_context, student.StudentID).Run(); });
                break;

            default:
                foreach (var student in _context.Students)
                    Options.Add(student.FullName, () => { new StudentPage(_context, student.StudentID).Run(); });
                break;
        }
    }
}
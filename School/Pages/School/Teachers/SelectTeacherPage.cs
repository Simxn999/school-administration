using School.Data;

namespace School.Pages;

public class SelectTeacherPage : Page
{
    readonly DataContext _context;
    readonly string _state;

    public SelectTeacherPage(DataContext context, string state = "default")
    {
        _context = context;
        _state = state;

        Title = "Select Teacher:";
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        switch (_state)
        {
            case "manage":
                foreach (var teacher in _context.Teachers)
                    Options.Add(teacher.FullName, () => { new ManageTeacherPage(_context, teacher.TeacherID).Run(); });
                break;

            case "remove":
                foreach (var teacher in _context.Teachers)
                    Options.Add(teacher.FullName, () => { new RemoveTeacherPage(_context, teacher.TeacherID).Run(); });
                break;

            default:
                foreach (var teacher in _context.Teachers)
                    Options.Add(teacher.FullName, () => { new TeacherPage(_context, teacher.TeacherID).Run(); });
                break;
        }
    }
}
using School.Data;

namespace School.Pages;

public class RemoveTeacherPage : Page
{
    readonly DataContext _context;
    readonly int _teacherID;

    public RemoveTeacherPage(DataContext context, int teacherID)
    {
        _context = context;
        _teacherID = teacherID;

        Title = "Remove Teacher";

        Content = () => TeacherPage.TeacherContent(_context, _teacherID);
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("#enter#Confirm Removal of Teacher", OptionRemove);
    }

    void OptionRemove()
    {
        _context.Teachers.Remove(_context.Teachers.First(c => c.TeacherID == _teacherID));
        _context.SaveChanges();
        Exit = true;
        ReturnValue = 1;
    }
}
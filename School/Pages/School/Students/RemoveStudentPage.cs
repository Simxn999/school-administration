using School.Data;

namespace School.Pages;

public class RemoveStudentPage : Page
{
    readonly DataContext _context;
    readonly int _studentID;

    public RemoveStudentPage(DataContext context, int studentID)
    {
        _context = context;
        _studentID = studentID;

        Title = "Remove Student";

        Content = () => StudentPage.StudentContent(_context, _studentID);
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("#enter#Confirm Removal of Student", OptionRemove);
    }

    void OptionRemove()
    {
        _context.Students.Remove(_context.Students.First(c => c.StudentID == _studentID));
        _context.SaveChanges();
        Exit = true;
        ReturnValue = 1;
    }
}
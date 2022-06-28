using School.Data;

namespace School.Pages;

public class ManageStudentsPage : Page
{
    readonly DataContext _context;

    public ManageStudentsPage(DataContext context)
    {
        _context = context;
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("Add Student", OptionAdd);
        Options.Add("Manage Student", OptionManage);
        Options.Add("Remove Student", OptionRemove);
    }

    void OptionAdd()
    {
        new AddStudentPage(_context).Run();
    }

    void OptionManage()
    {
        new SelectStudentPage(_context, "manage").Run();
    }

    void OptionRemove()
    {
        new SelectStudentPage(_context, "remove").Run();
    }
}
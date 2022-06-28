using School.Data;

namespace School.Pages;

public class ManageTeachersPage : Page
{
    readonly DataContext _context;

    public ManageTeachersPage(DataContext context)
    {
        _context = context;
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("Add Teacher", OptionAdd);
        Options.Add("Manage Teacher", OptionManage);
        Options.Add("Remove Teacher", OptionRemove);
    }

    void OptionAdd()
    {
        new AddTeacherPage(_context).Run();
    }

    void OptionManage()
    {
        new SelectTeacherPage(_context, "manage").Run();
    }

    void OptionRemove()
    {
        new SelectTeacherPage(_context, "remove").Run();
    }
}
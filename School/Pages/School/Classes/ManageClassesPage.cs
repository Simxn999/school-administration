using School.Data;

namespace School.Pages;

public class ManageClassesPage : Page
{
    readonly DataContext _context;

    public ManageClassesPage(DataContext context)
    {
        _context = context;
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("Add Class", OptionAdd);
        Options.Add("Manage Classes", OptionManage);
        Options.Add("Remove Class", OptionRemove);
    }

    void OptionAdd()
    {
        new AddClassPage(_context).Run();
    }

    void OptionManage()
    {
        new SelectClassPage(_context, "manage").Run();
    }

    void OptionRemove()
    {
        new SelectClassPage(_context, "remove").Run();
    }
}
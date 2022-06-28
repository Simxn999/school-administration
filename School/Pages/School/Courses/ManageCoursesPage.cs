using School.Data;

namespace School.Pages;

public class ManageCoursesPage : Page
{
    readonly DataContext _context;

    public ManageCoursesPage(DataContext context)
    {
        _context = context;
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("Add Course", OptionAdd);
        Options.Add("Manage Course", OptionManage);
        Options.Add("Remove Course", OptionRemove);
    }

    void OptionAdd()
    {
        new AddCoursePage(_context).Run();
    }

    void OptionManage()
    {
        new SelectCoursePage(_context, "manage").Run();
    }

    void OptionRemove()
    {
        new SelectCoursePage(_context, "remove").Run();
    }
}
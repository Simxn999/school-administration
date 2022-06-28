using School.Data;

namespace School.Pages;

public class SelectClassPage : Page
{
    readonly DataContext _context;
    readonly string _state;

    public SelectClassPage(DataContext context, string state = "default")
    {
        _context = context;
        _state = state;

        Title = "Select Class:";
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        switch (_state)
        {
            case "manage":
                foreach (var @class in _context.Classes)
                    Options.Add(@class.Title, () => { new ManageClassPage(_context, @class.ClassID).Run(); });
                break;

            case "remove":
                foreach (var @class in _context.Classes)
                    Options.Add(@class.Title, () => { new RemoveClassPage(_context, @class.ClassID).Run(); });
                break;

            default:
                foreach (var @class in _context.Classes)
                    Options.Add(@class.Title, () => { new ClassPage(_context, @class.ClassID).Run(); });
                break;
        }
    }
}
using School.Data;

namespace School.Pages;

public class RemoveClassPage : Page
{
    readonly int _classID;
    readonly DataContext _context;

    public RemoveClassPage(DataContext context, int classID)
    {
        _context = context;
        _classID = classID;

        Title = "Remove Class";

        Content = () => ClassPage.ClassContent(_context, _classID);
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("#enter#Confirm Removal of Class", OptionRemove);
    }

    void OptionRemove()
    {
        _context.Classes.Remove(_context.Classes.First(c => c.ClassID == _classID));
        _context.SaveChanges();
        Exit = true;
        ReturnValue = 1;
    }
}
using School.Data;

namespace School.Pages;

public class RemoveCoursePage : Page
{
    readonly DataContext _context;
    readonly int _courseID;

    public RemoveCoursePage(DataContext context, int courseID)
    {
        _context = context;
        _courseID = courseID;

        Title = "Remove Course";

        Content = () => CoursePage.CourseContent(_context, _courseID);
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("#enter#Confirm Removal of Course", OptionRemove);
    }

    void OptionRemove()
    {
        _context.Courses.Remove(_context.Courses.First(c => c.CourseID == _courseID));
        _context.SaveChanges();
        Exit = true;
        ReturnValue = 1;
    }
}
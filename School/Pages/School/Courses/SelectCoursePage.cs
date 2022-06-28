using School.Data;

namespace School.Pages;

public class SelectCoursePage : Page
{
    readonly DataContext _context;
    readonly string _state;

    public SelectCoursePage(DataContext context, string state = "default")
    {
        _context = context;
        _state = state;

        Title = "Select Course:";
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        switch (_state)
        {
            case "manage":
                foreach (var course in _context.Courses)
                    Options.Add(course.Title, () => { new ManageCoursePage(_context, course.CourseID).Run(); });
                break;

            case "remove":
                foreach (var course in _context.Courses)
                    Options.Add(course.Title, () => { new RemoveCoursePage(_context, course.CourseID).Run(); });
                break;

            default:
                foreach (var course in _context.Courses)
                    Options.Add(course.Title, () => { new CoursePage(_context, course.CourseID).Run(); });
                break;
        }
    }
}
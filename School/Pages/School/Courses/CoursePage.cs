using School.Data;

namespace School.Pages;

public class CoursePage : Page
{
    public CoursePage(DataContext context, int courseID)
    {
        Title = "Course";

        Content = () => CourseContent(context, courseID);
    }

    public static void CourseContent(DataContext context, int courseID)
    {
        Positions.Clear();

        PrintData("Title:", context.Courses.Where(c => c.CourseID == courseID).Select(c => c.Title).FirstOrDefault());

        PrintDataList("Teachers:",
                      context.Courses.Where(c => c.CourseID == courseID).Select(c => c.Teachers!.Select(t => t.FullName).ToList()).FirstOrDefault());

        PrintDataList("Students:",
                      context.Courses.Where(c => c.CourseID == courseID).Select(c => c.Students!.Select(s => s.FullName).ToList()).FirstOrDefault());
    }
}
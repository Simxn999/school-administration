using School.Data;

namespace School.Pages;

public class ClassPage : Page
{
    public ClassPage(DataContext context, int classID)
    {
        Title = "Class";

        Content = () => ClassContent(context, classID);
    }

    public static void ClassContent(DataContext context, int classID)
    {
        Positions.Clear();

        PrintData("Title:", context.Classes.Where(@class => @class.ClassID == classID).Select(@class => @class.Title).FirstOrDefault());

        PrintDataList("Teachers:",
                      context.Classes.Where(@class => @class.ClassID == classID)
                             .Select(@class => @class.Teachers!.Select(teacher => teacher.FullName).ToList())
                             .FirstOrDefault());

        PrintDataList("Students:",
                      context.Classes.Where(@class => @class.ClassID == classID)
                             .Select(@class => @class.Students!.Select(student => student.FullName).ToList())
                             .FirstOrDefault());
    }
}
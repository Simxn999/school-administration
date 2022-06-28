using School.Data;

namespace School.Pages;

public class TeacherPage : Page
{
    public TeacherPage(DataContext context, int teacherID)
    {
        Title = "Teacher";
        Content = () => TeacherContent(context, teacherID);
    }

    public static void TeacherContent(DataContext context, int teacherID)
    {
        Positions.Clear();

        PrintPerson(context.Teachers.FirstOrDefault(t => t.TeacherID == teacherID));

        PrintDataList("Classes:",
                      context.Teachers.Where(t => t.TeacherID == teacherID).Select(t => t.Classes!.Select(c => c.Title).ToList()).FirstOrDefault());

        PrintDataList("Courses:",
                      context.Teachers.Where(t => t.TeacherID == teacherID).Select(t => t.Courses!.Select(c => c.Title).ToList()).FirstOrDefault());

        PrintDataList("Students:",
                      context.Students.Where(student =>
                                                 student.Classes!.Any(@class =>
                                                                          @class.Teachers!.Contains(context.Teachers.First(teacher =>
                                                                                                        teacher.TeacherID ==
                                                                                                        teacherID))) ||
                                                 student.Courses!.Any(course =>
                                                                          course.Teachers!.Contains(context.Teachers.First(teacher =>
                                                                                                        teacher.TeacherID ==
                                                                                                        teacherID))))
                             .Select(student => student.FullName)
                             .ToList());
    }
}
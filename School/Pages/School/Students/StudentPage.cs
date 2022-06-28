using School.Data;

namespace School.Pages;

public class StudentPage : Page
{
    public StudentPage(DataContext context, int studentID)
    {
        Title = "Student";
        Content = () => StudentContent(context, studentID);
    }

    public static void StudentContent(DataContext context, int studentID)
    {
        Positions.Clear();

        PrintPerson(context.Students.FirstOrDefault(student => student.StudentID == studentID));

        PrintDataList("Classes:",
                      context.Classes.Where(@class => @class.Students!.Contains(context.Students.First(student => student.StudentID == studentID)))
                             .Select(@class => @class.Title)
                             .ToList());

        PrintDataList("Courses:",
                      context.Courses.Where(@class => @class.Students!.Contains(context.Students.First(student => student.StudentID == studentID)))
                             .Select(@class => @class.Title)
                             .ToList());

        PrintDataList("Teachers:",
                      context.Teachers.Where(teacher =>
                                                 teacher.Classes!.Any(@class =>
                                                                          @class.Students!.Contains(context.Students.First(student =>
                                                                                                        student.StudentID ==
                                                                                                        studentID))) ||
                                                 teacher.Courses!.Any(course =>
                                                                          course.Students!.Contains(context.Students.First(student =>
                                                                                                        student.StudentID ==
                                                                                                        studentID))))
                             .Select(teacher => teacher.FullName)
                             .ToList());
    }
}
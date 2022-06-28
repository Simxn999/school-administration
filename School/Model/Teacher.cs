using System.ComponentModel.DataAnnotations;

namespace School.Model;

public class Teacher : Person
{
    [Key]
    public int TeacherID { get; set; }

    public virtual ICollection<Class>? Classes { get; set; }
    public virtual ICollection<Course>? Courses { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace School.Model;

public class Student : Person
{
    [Key]
    public int StudentID { get; set; }

    public virtual ICollection<Class>? Classes { get; set; }
    public virtual ICollection<Course>? Courses { get; set; }
}
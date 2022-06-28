using System.ComponentModel.DataAnnotations;

namespace School.Model;

public class Course
{
    public Course()
    {
        Title = "";
    }

    [Key]
    public int CourseID { get; set; }

    [MaxLength(50)]
    public string Title { get; set; }

    public virtual ICollection<Teacher>? Teachers { get; set; }
    public virtual ICollection<Student>? Students { get; set; }
}
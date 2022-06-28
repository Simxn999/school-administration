using System.ComponentModel.DataAnnotations;

namespace School.Model;

public class Class
{
    public Class()
    {
        Title = "";
    }

    public Class(int id, string title)
    {
        ClassID = id;
        Title = title;
    }

    [Key]
    public int ClassID { get; set; }

    [MaxLength(50)]
    public string Title { get; set; }

    public virtual ICollection<Teacher>? Teachers { get; set; }
    public virtual ICollection<Student>? Students { get; set; }
}
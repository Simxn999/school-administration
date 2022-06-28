using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Model;

public class Person
{
    public Person()
    {
        Name = "";
        Surname = "";
    }

    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(50)]
    public string Surname { get; set; }

    [MaxLength(32)]
    public string? PhoneNumber { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    [NotMapped]
    public string FullName => $"{Name} {Surname}";
}
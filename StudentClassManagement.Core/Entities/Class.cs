namespace StudentClassManagement.Core.Entities;

public class Class: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<StudentClass> StudentClasses { get; set; }
}
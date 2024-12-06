namespace StudentClassManagement.Core.Entities;

public class Student: BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailId { get; set; }
    public ICollection<StudentClass> StudentClasses { get; set; }
}
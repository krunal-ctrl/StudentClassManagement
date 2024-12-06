namespace StudentClassManagement.Core.DTOs;

public class StudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailId { get; set; }
    public List<int> ClassIds { get; set; }
}
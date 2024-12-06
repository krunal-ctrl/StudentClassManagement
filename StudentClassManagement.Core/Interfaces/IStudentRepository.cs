using StudentClassManagement.Core.Entities;

namespace StudentClassManagement.Core.Interfaces;

public interface IStudentRepository: IRepository<Student>
{
    Task AssignStudentToClass(int studentId, int classId);
    Task RemoveStudentFromClass(int studentId, int classId);
}
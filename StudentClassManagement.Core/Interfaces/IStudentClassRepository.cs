using StudentClassManagement.Core.Entities;

namespace StudentClassManagement.Core.Interfaces;

public interface IStudentClassRepository
{
    Task<StudentClass> GetByIdsAsync(int studentId, int classId);
    Task<IEnumerable<StudentClass>> GetByStudentIdAsync(int studentId);
    Task<IEnumerable<StudentClass>> GetByClassIdAsync(int classId);
    Task AddAsync(StudentClass studentClass);
    Task RemoveAsync(StudentClass studentClass);
}
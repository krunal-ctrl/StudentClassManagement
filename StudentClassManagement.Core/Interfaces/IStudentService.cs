using StudentClassManagement.Core.DTOs;
using StudentClassManagement.Core.Entities;

namespace StudentClassManagement.Core.Interfaces;

public interface IStudentService
{
    Task<StudentDto> GetByIdAsync(int id);
    Task<IEnumerable<StudentDto>> GetAllAsync(string search, int page, int size);
    Task AddAsync(StudentDto student);
    Task UpdateAsync(StudentDto student);
    Task DeleteAsync(int id);
    Task AssignStudentToClass(int studentId, int classId);
    Task RemoveStudentFromClass(int studentId, int classId);
    Task<IEnumerable<StudentDto>> BulkImportStudents(IEnumerable<StudentDto> students);
}
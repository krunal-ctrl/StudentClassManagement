using StudentClassManagement.Core.Entities;
using StudentClassManagement.Core.Interfaces;

namespace StudentClassManagement.Application.Services;

public class StudentClassService
{
    private readonly IStudentClassRepository _studentClassRepository;

    public StudentClassService(IStudentClassRepository studentClassRepository)
    {
        _studentClassRepository = studentClassRepository;
    }

    public async Task EnrollStudentInClassAsync(int studentId, int classId)
    {
        // Check if the student is already enrolled in this class
        var existingEnrollment = await _studentClassRepository.GetByIdsAsync(studentId, classId);
        if (existingEnrollment != null)
        {
            throw new InvalidOperationException("Student is already enrolled in this class.");
        }

        var studentClass = new StudentClass
        {
            StudentId = studentId,
            ClassId = classId
        };

        await _studentClassRepository.AddAsync(studentClass);
    }

    public async Task RemoveStudentFromClassAsync(int studentId, int classId)
    {
        // Find the enrollment
        var studentClass = await _studentClassRepository.GetByIdsAsync(studentId, classId);
        if (studentClass == null)
        {
            throw new InvalidOperationException("Student is not enrolled in this class.");
        }

        await _studentClassRepository.RemoveAsync(studentClass);
    }

    public async Task<IEnumerable<Student>> GetStudentsByClassAsync(int classId)
    {
        var studentClasses = await _studentClassRepository.GetByClassIdAsync(classId);
        return studentClasses.Select(sc => sc.Student);
    }

    public async Task<IEnumerable<Class>> GetClassesByStudentAsync(int studentId)
    {
        var studentClasses = await _studentClassRepository.GetByStudentIdAsync(studentId);
        return studentClasses.Select(sc => sc.Class);
    }
}
using Microsoft.EntityFrameworkCore;
using StudentClassManagement.Core.Entities;
using StudentClassManagement.Core.Interfaces;
using StudentClassManagement.Infrastructure.Data;

namespace StudentClassManagement.Infrastructure.Repositories;

public class StudentClassRepository: IStudentClassRepository
{
    private readonly ApplicationDbContext _context;

    public StudentClassRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<StudentClass> GetByIdsAsync(int studentId, int classId)
    {
        return await _context.StudentClasses
            .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.ClassId == classId);
    }

    public async Task<IEnumerable<StudentClass>> GetByStudentIdAsync(int studentId)
    {
        return await _context.StudentClasses
            .Where(sc => sc.StudentId == studentId)
            .Include(sc => sc.Class)
            .ToListAsync();
    }

    public async Task<IEnumerable<StudentClass>> GetByClassIdAsync(int classId)
    {
        return await _context.StudentClasses
            .Where(sc => sc.ClassId == classId)
            .Include(sc => sc.Student)
            .ToListAsync();
    }

    public async Task AddAsync(StudentClass studentClass)
    {
        _context.StudentClasses.Add(studentClass);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(StudentClass studentClass)
    {
        _context.StudentClasses.Remove(studentClass);
        await _context.SaveChangesAsync();
    }
}
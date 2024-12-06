using Microsoft.EntityFrameworkCore;
using StudentClassManagement.Core.Entities;
using StudentClassManagement.Core.Interfaces;
using StudentClassManagement.Infrastructure.Data;

namespace StudentClassManagement.Infrastructure.Repositories;

public class StudentRepository: BaseRepo<Student>, IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllAsync(string search, int page, int size)
    {
        var query = _context.Students.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search) || s.EmailId.Contains(search));
        }

        return await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
    }

    public Task AssignStudentToClass(int studentId, int classId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveStudentFromClass(int studentId, int classId)
    {
        throw new NotImplementedException();
    }
}
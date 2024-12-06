using Microsoft.EntityFrameworkCore;
using StudentClassManagement.Core.DTOs;
using StudentClassManagement.Core.Entities;
using StudentClassManagement.Core.Interfaces;
using StudentClassManagement.Infrastructure.Data;

namespace StudentClassManagement.Infrastructure.Repositories;

public class ClassRepository: BaseRepo<Class>, IClassRepository
{
    private readonly ApplicationDbContext _context;

    public ClassRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Class>> GetAllAsync(string search, int page, int size)
    {
        var query = _context.Classes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c => c.Name.Contains(search) || c.Description.Contains(search));
        }

        return await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
    }
}
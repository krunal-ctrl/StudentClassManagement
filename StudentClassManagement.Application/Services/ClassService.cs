using StudentClassManagement.Core.DTOs;
using StudentClassManagement.Core.Interfaces;

namespace StudentClassManagement.Application.Services;

public class ClassService: IClassService
{
    public Task<ClassDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ClassDto>> GetAllAsync(string search, int page, int size)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(ClassDto classDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ClassDto classDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
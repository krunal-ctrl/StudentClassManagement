using StudentClassManagement.Core.DTOs;

namespace StudentClassManagement.Core.Interfaces;

public interface IClassService
{
    Task<ClassDto> GetByIdAsync(int id);
    Task<IEnumerable<ClassDto>> GetAllAsync(string search, int page, int size);
    Task AddAsync(ClassDto classDto);
    Task UpdateAsync(ClassDto classDto);
    Task DeleteAsync(int id);
}
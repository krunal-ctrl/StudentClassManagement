using AutoMapper;
using StudentClassManagement.Core.DTOs;
using StudentClassManagement.Core.Entities;
using StudentClassManagement.Core.Interfaces;

namespace StudentClassManagement.Application.Services;

public class ClassService: IClassService
{
    private readonly IClassRepository _classRepository;
    private readonly IMapper _mapper;

    public ClassService(IClassRepository classRepository, IMapper mapper)
    {
        _classRepository = classRepository;
        _mapper = mapper;
    }  
    public async Task<ClassDto> GetByIdAsync(int id)
    {
        var classEntity = await _classRepository.GetByIdAsync(id);

        if (classEntity == null) return null;
        return _mapper.Map<ClassDto>(classEntity);
    }

    public async Task<IEnumerable<ClassDto>> GetAllAsync(string search, int page, int size)
    {
        var classes = await _classRepository.GetAllAsync(
            filter: c => string.IsNullOrEmpty(search) || c.Name.Contains(search),
            page: page,
            pageSize: size
        );
        
        return _mapper.Map<IEnumerable<ClassDto>>(classes);
    }

    public async Task AddAsync(ClassDto classDto)
    {
        var classEntity = _mapper.Map<Class>(classDto);

        await _classRepository.AddAsync(classEntity);
    }

    public async Task UpdateAsync(ClassDto classDto)
    {
        var classEntity = await _classRepository.GetByIdAsync(classDto.Id);

        if (classEntity == null) 
        {
            throw new KeyNotFoundException($"Class with ID {classDto.Id} not found.");
        }
        
        _mapper.Map(classDto, classEntity);

        await _classRepository.UpdateAsync(classEntity);
        await _classRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var classEntity = await _classRepository.GetByIdAsync(id);

        if (classEntity == null)
        {
            throw new KeyNotFoundException($"Class with ID {id} not found.");
        }

        await _classRepository.DeleteAsync(id);
    }
}
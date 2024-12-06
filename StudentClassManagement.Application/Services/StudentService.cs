using System.Net.Mail;
using System.Text.RegularExpressions;
using AutoMapper;
using StudentClassManagement.Core.DTOs;
using StudentClassManagement.Core.Entities;
using StudentClassManagement.Core.Interfaces;

namespace StudentClassManagement.Application.Services;

public class StudentService: IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IClassRepository _classRepository;
    private readonly IMapper _mapper;
    private readonly StudentClassService _studentClassService;

    public StudentService(IStudentRepository studentRepository, IClassRepository classRepository, IMapper mapper, StudentClassService studentClassService)
    {
        _studentRepository = studentRepository;
        _classRepository = classRepository;
        _mapper = mapper;
        _studentClassService = studentClassService;
    }

    public async Task<StudentDto> GetByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        return _mapper.Map<StudentDto>(student);
    }

    public async Task<IEnumerable<StudentDto>> GetAllAsync(string search, int page, int size)
    {
        var students = await _studentRepository.GetAllAsync(
            filter: s => string.IsNullOrEmpty(search) || s.FirstName.Contains(search) || s.LastName.Contains(search));
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }

    public async Task AddAsync(StudentDto student)
    {
        var studentEntity = _mapper.Map<Student>(student);
        await _studentRepository.AddAsync(studentEntity);
    }

    public async Task UpdateAsync(StudentDto student)
    {
        var studentEntity = _mapper.Map<Student>(student);
        await _studentRepository.UpdateAsync(studentEntity);
    }

    public async Task DeleteAsync(int id)
    {
        await _studentRepository.DeleteAsync(id);
    }

    public async Task AssignStudentToClass(int studentId, int classId)
    {
        var student = await _studentRepository.GetByIdAsync(studentId);
        if (student == null) throw new KeyNotFoundException("Student not found.");

        var cls = await _classRepository.GetByIdAsync(classId);
        if (cls == null) throw new KeyNotFoundException("Class not found.");

        await _studentClassService.EnrollStudentInClassAsync(studentId, classId);
    }

    public async Task RemoveStudentFromClass(int studentId, int classId)
    {
        var student = await _studentRepository.GetByIdAsync(studentId);
        if (student == null) throw new KeyNotFoundException("Student not found.");

        await _studentClassService.RemoveStudentFromClassAsync(studentId, classId);
    }

    public async Task<IEnumerable<StudentDto>> BulkImportStudents(IEnumerable<StudentDto> students)
    {
        var successfulStudents = new List<Student>();
        var failedStudents = new List<StudentDto>();

        foreach (var studentDto in students)
        {
            // Validate the student
            if (await ValidateStudentAsync(studentDto))
            {
                var student = new Student
                {
                    FirstName = studentDto.FirstName,
                    LastName = studentDto.LastName,
                    PhoneNumber = studentDto.PhoneNumber,
                    EmailId = studentDto.EmailId,
                    StudentClasses = studentDto.ClassIds.Select(classId => new StudentClass
                    {
                        ClassId = classId,
                    }).ToList()
                };

                // Add student to the list of successful imports
                successfulStudents.Add(student);
            }
            else
            {
                // Add student to the list of failed imports
                failedStudents.Add(studentDto);
            }
        }

        if (successfulStudents.Any())
        {
            // Save valid students to the database
            await _studentRepository.AddRangeAsync(successfulStudents);
        }

        return failedStudents;
    }

    private async Task<bool> ValidateStudentAsync(StudentDto studentDto)
    {
        // implement student validation
        return true;
    }
}
using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentClassManagement.Core.DTOs;
using StudentClassManagement.Core.Entities;
using StudentClassManagement.Core.Interfaces;

namespace StudentClassManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        return Ok(student);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var students = await _studentService.GetAllAsync(search, page, size);
        return Ok(students);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudentDto student)
    {
        await _studentService.AddAsync(student);
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] StudentDto student)
    {
        if (id != student.Id) return BadRequest("Student ID mismatch.");

        await _studentService.UpdateAsync(student);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{studentId}/assign-class/{classId}")]
    public async Task<IActionResult> AssignToClass(int studentId, int classId)
    {
        await _studentService.AssignStudentToClass(studentId, classId);
        return NoContent();
    }

    [HttpPost("{studentId}/remove-class/{classId}")]
    public async Task<IActionResult> RemoveFromClass(int studentId, int classId)
    {
        await _studentService.RemoveStudentFromClass(studentId, classId);
        return NoContent();
    }
    
    [HttpPost("bulk-import")]
    public async Task<IActionResult> ImportStudents(IFormFile file)
    {
        using var stream = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(stream, CultureInfo.InvariantCulture);
    
        var students = csv.GetRecords<StudentDto>().ToList();
        var failedStudents = await _studentService.BulkImportStudents(students);
        
        if (failedStudents.Any())
        {
            return BadRequest(new
            {
                message = "Some students failed to import.",
                failedStudents
            });
        }
        return Ok();
    }
}
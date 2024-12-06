using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentClassManagement.Application.Services;
using StudentClassManagement.Core.DTOs;

namespace StudentClassManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClassController : ControllerBase
{
    private readonly ClassService _classService;

    public ClassController(ClassService classService)
    {
        _classService = classService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetClasses([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var classes = await _classService.GetAllAsync(search, page, size);
        return Ok(classes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClass(int id)
    {
        var classEntity = await _classService.GetByIdAsync(id);
        return Ok(classEntity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClass([FromBody] ClassDto classDto)
    {
        await _classService.AddAsync(classDto);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClass(int id, [FromBody] ClassDto classDto)
    {
        await _classService.UpdateAsync(classDto);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClass(int id)
    {
        await _classService.DeleteAsync(id);
        return NoContent();
    }
}
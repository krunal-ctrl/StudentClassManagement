using StudentClassManagement.Core.Entities;

namespace StudentClassManagement.Infrastructure.Data;

public class DataSeeder
{
    private readonly ApplicationDbContext _context;

    public DataSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedDataAsync()
    {
        if (_context.Students.Any() || _context.Classes.Any()) return; // Skip if data exists
        
        // Seed Classes
        var classes = new List<Class>
        {
            new Class { Name = "Mathematics", Description = "Advanced mathematics course." },
            new Class { Name = "Science", Description = "Basic science course." },
            new Class { Name = "History", Description = "World history from the 18th century." },
            new Class { Name = "English", Description = "English literature and grammar." },
            new Class { Name = "Art", Description = "Creative art classes for beginners." },
            new Class { Name = "Physics", Description = "Understanding the fundamental laws of nature." },
            new Class { Name = "Chemistry", Description = "Chemical reactions and their applications." },
            new Class { Name = "Biology", Description = "Study of living organisms." },
            new Class { Name = "Geography", Description = "Geographical studies and mapping." },
            new Class { Name = "Philosophy", Description = "Introduction to philosophical thinking." }
        };
        
        await _context.Classes.AddRangeAsync(classes);
        await _context.SaveChangesAsync();
        
        // Seed Students
        var students = new List<Student>
        {
            new Student { FirstName = "John", LastName = "Doe", EmailId = "john.doe@example.com", PhoneNumber = "1234567890" },
            new Student { FirstName = "Jane", LastName = "Smith", EmailId = "jane.smith@example.com", PhoneNumber = "2345678901" },
            new Student { FirstName = "Alice", LastName = "Johnson", EmailId = "alice.johnson@example.com", PhoneNumber = "3456789012" },
            new Student { FirstName = "Bob", LastName = "Brown", EmailId = "bob.brown@example.com", PhoneNumber = "4567890123" },
            new Student { FirstName = "Charlie", LastName = "Davis", EmailId = "charlie.davis@example.com", PhoneNumber = "5678901234" },
            new Student { FirstName = "David", LastName = "Wilson", EmailId = "david.wilson@example.com", PhoneNumber = "6789012345" },
            new Student { FirstName = "Eve", LastName = "Martinez", EmailId = "eve.martinez@example.com", PhoneNumber = "7890123456" },
            new Student { FirstName = "Frank", LastName = "Garcia", EmailId = "frank.garcia@example.com", PhoneNumber = "8901234567" },
            new Student { FirstName = "Grace", LastName = "Lopez", EmailId = "grace.lopez@example.com", PhoneNumber = "9012345678" },
            new Student { FirstName = "Hannah", LastName = "Miller", EmailId = "hannah.miller@example.com", PhoneNumber = "0123456789" }
        };
        
        await _context.Students.AddRangeAsync(students);
        await _context.SaveChangesAsync();

        // Establish relationships between Students and Classes
        var studentClasses = new List<StudentClass>
        {
            new StudentClass { StudentId = 1, ClassId = 1 },  // John - Math
            new StudentClass { StudentId = 1, ClassId = 2 },  // John - Science
            new StudentClass { StudentId = 2, ClassId = 3 },  // Jane - History
            new StudentClass { StudentId = 3, ClassId = 4 },  // Alice - English
            new StudentClass { StudentId = 4, ClassId = 5 },  // Bob - Art
            new StudentClass { StudentId = 5, ClassId = 6 },  // Charlie - Physics
            new StudentClass { StudentId = 6, ClassId = 7 },  // David - Chemistry
            new StudentClass { StudentId = 7, ClassId = 8 },  // Eve - Biology
            new StudentClass { StudentId = 8, ClassId = 9 },  // Frank - Geography
            new StudentClass { StudentId = 9, ClassId = 10 }, // Grace - Philosophy
            new StudentClass { StudentId = 10, ClassId = 1 }, // Hannah - Math
            new StudentClass { StudentId = 2, ClassId = 4 },  // Jane - English
            new StudentClass { StudentId = 3, ClassId = 7 },  // Alice - Chemistry
            new StudentClass { StudentId = 4, ClassId = 9 }   // Bob - Geography
        };

        await _context.StudentClasses.AddRangeAsync(studentClasses);
        await _context.SaveChangesAsync();
    }
}

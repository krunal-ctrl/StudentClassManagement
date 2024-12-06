using Microsoft.EntityFrameworkCore;
using StudentClassManagement.Core.Entities;

namespace StudentClassManagement.Infrastructure.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<StudentClass> StudentClasses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships and constraints
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.EmailId).IsUnique();
            entity.HasIndex(e => e.PhoneNumber).IsUnique();
            entity.Property(e => e.PhoneNumber).HasMaxLength(10).IsRequired();
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(100);
        });
        
        modelBuilder.Entity<StudentClass>()
            .HasKey(sc => new { StudentId = sc.StudentId, ClassId = sc.ClassId });  // Composite primary key

        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentClasses)
            .HasForeignKey(sc => sc.StudentId)
            .OnDelete(DeleteBehavior.Cascade);  // Cascade delete if needed

        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.Class)
            .WithMany(c => c.StudentClasses)
            .HasForeignKey(sc => sc.ClassId)
            .OnDelete(DeleteBehavior.Cascade);  
    }
}
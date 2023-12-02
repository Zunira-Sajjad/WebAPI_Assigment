using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DbModels
{
    public class StudentDbContext : DbContext
    {
        public DbSet <StudentDbDto> studentDbDto { get; set; }
        public DbSet<StudentSubjectDbDto> studentSubjectDbDto { get; set; }
        public DbSet<SubjectDbDto> subjectDbDto { get; set; }

        public StudentDbContext(DbContextOptions<StudentDbContext> options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StudentCourse4;Trusted_Connection=True;",
                    builder => builder.MigrationsAssembly("MyWebApiStudentGPA"));
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary key for StudentDbDto
            modelBuilder.Entity<StudentDbDto>().HasKey(e => e.StudentId);

            // Configure one-to-many relationship between StudentDbDto and StudentSubjectDbDto
            modelBuilder.Entity<StudentDbDto>()
                .HasMany(s => s.StudentSubjects)
                .WithOne(ss => ss.studentDbDto)
                .HasForeignKey(ss => ss.studentID)
                .OnDelete(DeleteBehavior.Cascade); 

            // Configure primary key for SubjectDbDto
            modelBuilder.Entity<SubjectDbDto>().HasKey(e => e.SubjectId);

            // Configure one-to-many relationship between SubjectDbDto and StudentSubjectDbDto
            modelBuilder.Entity<SubjectDbDto>()
                .HasMany(sub => sub.StudentSubjects)
                .WithOne(ss => ss.SubjectDbDto)
                .HasForeignKey(ss => ss.subjectId)
                .OnDelete(DeleteBehavior.Cascade); 
        }


    }
}



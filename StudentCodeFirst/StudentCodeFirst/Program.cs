using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace StudentCodeFirst
{
    // ============================================
    // MODEL - Course class (Code-First)
    // ============================================
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Grade { get; set; }
        public int Credits { get; set; }

        // Foreign key
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public override string ToString()
        {
            return $"     Course: {CourseName} | Grade: {Grade} | Credits: {Credits}";
        }
    }

    // ============================================
    // MODEL - Student class (Code-First)
    // ============================================
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Program { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public double GPA { get; set; }

        // Navigation property
        public virtual List<Course> Courses { get; set; }

        public Student()
        {
            Courses = new List<Course>();
        }

        public override string ToString()
        {
            return $"[{StudentId}] {FirstName} {LastName} | Email: {Email} | Program: {Program} | Enrolled: {EnrollmentDate.ToShortDateString()} | GPA: {GPA}";
        }
    }

    // ============================================
    // DBCONTEXT - Database connection (Code-First)
    // ============================================
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("SchoolDB")
        {
            // Drops and recreates database if model changes
            Database.SetInitializer(new DropCreateDatabaseAlways<SchoolContext>());
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Student configuration
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Student>().HasKey(s => s.StudentId);
            modelBuilder.Entity<Student>().Property(s => s.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Student>().Property(s => s.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Student>().Property(s => s.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Student>().Property(s => s.Program).IsRequired().HasMaxLength(100);

            // Course configuration
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
            modelBuilder.Entity<Course>().Property(c => c.CourseName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Course>().Property(c => c.Grade).IsRequired().HasMaxLength(2);

            // Relationship: Student has many Courses
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithRequired(c => c.Student)
                .HasForeignKey(c => c.StudentId);

            base.OnModelCreating(modelBuilder);
        }
    }

    // ============================================
    // MAIN PROGRAM
    // ============================================
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("STUDENT DATABASE MANAGEMENT SYSTEM");
            PrintHeader("Powered by Entity Framework Code-First");

            using (var context = new SchoolContext())
            {
                // ---- ADD STUDENTS ----
                AddStudents(context);

                // ---- READ ALL STUDENTS ----
                ReadAllStudents(context);

                // ---- SEARCH/FILTER ----
                SearchStudents(context);

                // ---- UPDATE A STUDENT ----
                UpdateStudent(context);

                // ---- STATISTICS ----
                ShowStatistics(context);

                // ---- DELETE A STUDENT ----
                DeleteStudent(context);

                // ---- FINAL LIST ----
                PrintHeader("FINAL STUDENT LIST AFTER DELETION");
                ReadAllStudents(context);
            }

            Console.WriteLine("\n========================================");
            Console.WriteLine("   Press any key to exit...             ");
            Console.WriteLine("========================================");
            Console.ReadKey();
        }

        // ============================================
        // ADD STUDENTS
        // ============================================
        static void AddStudents(SchoolContext context)
        {
            PrintHeader("STEP 1: ADDING STUDENTS TO DATABASE");

            var students = new List<Student>
            {
                new Student
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@school.com",
                    Program = "Computer Programming",
                    EnrollmentDate = new DateTime(2024, 9, 1),
                    GPA = 3.8,
                    Courses = new List<Course>
                    {
                        new Course { CourseName = "C# Programming", Grade = "A", Credits = 3 },
                        new Course { CourseName = "Database Design", Grade = "A", Credits = 3 },
                        new Course { CourseName = "Web Development", Grade = "B+", Credits = 3 }
                    }
                },
                new Student
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "janesmith@school.com",
                    Program = "Web Development",
                    EnrollmentDate = new DateTime(2024, 9, 1),
                    GPA = 3.9,
                    Courses = new List<Course>
                    {
                        new Course { CourseName = "HTML & CSS", Grade = "A+", Credits = 3 },
                        new Course { CourseName = "JavaScript", Grade = "A", Credits = 3 },
                        new Course { CourseName = "ASP.NET MVC", Grade = "A", Credits = 3 }
                    }
                },
                new Student
                {
                    FirstName = "Mike",
                    LastName = "Johnson",
                    Email = "mikej@school.com",
                    Program = "Data Science",
                    EnrollmentDate = new DateTime(2025, 1, 15),
                    GPA = 3.5,
                    Courses = new List<Course>
                    {
                        new Course { CourseName = "Python", Grade = "A", Credits = 3 },
                        new Course { CourseName = "Machine Learning", Grade = "B+", Credits = 3 },
                        new Course { CourseName = "Statistics", Grade = "B", Credits = 3 }
                    }
                },
                new Student
                {
                    FirstName = "Sarah",
                    LastName = "Williams",
                    Email = "sarahw@school.com",
                    Program = "Computer Programming",
                    EnrollmentDate = new DateTime(2025, 1, 15),
                    GPA = 4.0,
                    Courses = new List<Course>
                    {
                        new Course { CourseName = "C# Programming", Grade = "A+", Credits = 3 },
                        new Course { CourseName = "Algorithms", Grade = "A+", Credits = 3 },
                        new Course { CourseName = "Software Engineering", Grade = "A+", Credits = 3 }
                    }
                },
                new Student
                {
                    FirstName = "Chris",
                    LastName = "Brown",
                    Email = "chrisb@school.com",
                    Program = "Cybersecurity",
                    EnrollmentDate = new DateTime(2024, 9, 1),
                    GPA = 3.6,
                    Courses = new List<Course>
                    {
                        new Course { CourseName = "Network Security", Grade = "A", Credits = 3 },
                        new Course { CourseName = "Ethical Hacking", Grade = "A-", Credits = 3 },
                        new Course { CourseName = "Cryptography", Grade = "B+", Credits = 3 }
                    }
                }
            };

            foreach (var student in students)
            {
                context.Students.Add(student);
                Console.WriteLine($"   Added: {student.FirstName} {student.LastName} ({student.Program})");
            }

            context.SaveChanges();
            Console.WriteLine("\n   All students saved to database successfully!\n");
        }

        // ============================================
        // READ ALL STUDENTS
        // ============================================
        static void ReadAllStudents(SchoolContext context)
        {
            PrintHeader("ALL STUDENTS IN DATABASE");

            var students = context.Students.Include(s => s.Courses).ToList();

            foreach (var s in students)
            {
                Console.WriteLine(s.ToString());
                foreach (var c in s.Courses)
                {
                    Console.WriteLine(c.ToString());
                }
                Console.WriteLine();
            }
        }

        // ============================================
        // SEARCH/FILTER STUDENTS
        // ============================================
        static void SearchStudents(SchoolContext context)
        {
            PrintHeader("SEARCH: COMPUTER PROGRAMMING STUDENTS");

            var results = context.Students
                .Where(s => s.Program == "Computer Programming")
                .OrderByDescending(s => s.GPA)
                .ToList();

            Console.WriteLine($"   Found {results.Count} student(s) in Computer Programming:\n");
            foreach (var s in results)
            {
                Console.WriteLine($"   {s.FirstName} {s.LastName} | GPA: {s.GPA}");
            }

            PrintHeader("SEARCH: STUDENTS WITH GPA ABOVE 3.7");

            var highGPA = context.Students
                .Where(s => s.GPA >= 3.7)
                .OrderByDescending(s => s.GPA)
                .ToList();

            Console.WriteLine($"   Found {highGPA.Count} student(s) with GPA >= 3.7:\n");
            foreach (var s in highGPA)
            {
                Console.WriteLine($"   {s.FirstName} {s.LastName} | GPA: {s.GPA} | Program: {s.Program}");
            }
        }

        // ============================================
        // UPDATE A STUDENT
        // ============================================
        static void UpdateStudent(SchoolContext context)
        {
            PrintHeader("UPDATE: UPDATING JOHN DOE'S GPA");

            var student = context.Students
                .FirstOrDefault(s => s.FirstName == "John" && s.LastName == "Doe");

            if (student != null)
            {
                Console.WriteLine($"   Before: {student.FirstName} {student.LastName} | GPA: {student.GPA}");
                student.GPA = 3.95;
                context.SaveChanges();
                Console.WriteLine($"   After:  {student.FirstName} {student.LastName} | GPA: {student.GPA}");
                Console.WriteLine("\n   Student updated successfully!");
            }
        }

        // ============================================
        // SHOW STATISTICS
        // ============================================
        static void ShowStatistics(SchoolContext context)
        {
            PrintHeader("DATABASE STATISTICS");

            Console.WriteLine($"   Total Students    : {context.Students.Count()}");
            Console.WriteLine($"   Total Courses     : {context.Courses.Count()}");
            Console.WriteLine($"   Average GPA       : {context.Students.Average(s => s.GPA):F2}");
            Console.WriteLine($"   Highest GPA       : {context.Students.Max(s => s.GPA)}");
            Console.WriteLine($"   Lowest GPA        : {context.Students.Min(s => s.GPA)}");

            Console.WriteLine("\n   Students per Program:");
            var byProgram = context.Students
                .GroupBy(s => s.Program)
                .Select(g => new { Program = g.Key, Count = g.Count(), AvgGPA = g.Average(s => s.GPA) })
                .ToList();

            foreach (var p in byProgram)
            {
                Console.WriteLine($"   - {p.Program}: {p.Count} student(s) | Avg GPA: {p.AvgGPA:F2}");
            }
        }

        // ============================================
        // DELETE A STUDENT
        // ============================================
        static void DeleteStudent(SchoolContext context)
        {
            PrintHeader("DELETE: REMOVING CHRIS BROWN FROM DATABASE");

            var student = context.Students
                .Include(s => s.Courses)
                .FirstOrDefault(s => s.FirstName == "Chris" && s.LastName == "Brown");

            if (student != null)
            {
                Console.WriteLine($"   Deleting: {student.FirstName} {student.LastName}...");
                context.Courses.RemoveRange(student.Courses);
                context.Students.Remove(student);
                context.SaveChanges();
                Console.WriteLine("   Student deleted successfully!");
            }
        }

        // ============================================
        // HELPER - Print formatted header
        // ============================================
        static void PrintHeader(string title)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine($"   {title}");
            Console.WriteLine("========================================\n");
        }
    }
}   
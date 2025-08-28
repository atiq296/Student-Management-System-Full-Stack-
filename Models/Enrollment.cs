namespace StudentManagementSystem.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        // Foreign Key to Student
        public int StudentId { get; set; }
        public Student Student { get; set; }

        // Foreign Key to Course
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        // Grade (optional)
        public string? Grade { get; set; }
    }
}

using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        // Foreign Key to Teacher
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        // Navigation Property (many students can enroll in one course)
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}

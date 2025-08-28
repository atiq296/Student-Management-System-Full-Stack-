using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

        // Navigation Property (One teacher can have many courses)
        public ICollection<Course>? Courses { get; set; }
    }
}

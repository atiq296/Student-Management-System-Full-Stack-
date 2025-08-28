using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CoursesController(ApplicationDbContext db) => _db = db;

        // GET: /api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAll()
            => await _db.Courses.Include(c => c.Teacher).ToListAsync();

        // GET: /api/courses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Course>> GetOne(int id)
        {
            var course = await _db.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.CourseId == id);
            return course is null ? NotFound() : course;
        }

        // POST: /api/courses
        [HttpPost]
        public async Task<ActionResult<Course>> Create(Course course)
        {
            // optional: validate Teacher exists
            var teacherExists = await _db.Teachers.AnyAsync(t => t.TeacherId == course.TeacherId);
            if (!teacherExists) return BadRequest("TeacherId is invalid.");

            _db.Courses.Add(course);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOne), new { id = course.CourseId }, course);
        }

        // PUT: /api/courses/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Course input)
        {
            if (id != input.CourseId) return BadRequest();
            _db.Entry(input).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/courses/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _db.Courses.FindAsync(id);
            if (course is null) return NotFound();
            _db.Courses.Remove(course);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

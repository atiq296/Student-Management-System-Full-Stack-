using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public EnrollmentsController(ApplicationDbContext db) => _db = db;

        // GET: /api/enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetAll()
            => await _db.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();

        // GET: /api/enrollments/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Enrollment>> GetOne(int id)
        {
            var enr = await _db.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.EnrollmentId == id);
            return enr is null ? NotFound() : enr;
        }

        // POST: /api/enrollments
        [HttpPost]
        public async Task<ActionResult<Enrollment>> Create(Enrollment enr)
        {
            var studentOk = await _db.Students.AnyAsync(s => s.Id == enr.StudentId);
            var courseOk  = await _db.Courses.AnyAsync(c => c.CourseId == enr.CourseId);
            if (!studentOk || !courseOk) return BadRequest("Invalid StudentId or CourseId.");

            _db.Enrollments.Add(enr);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOne), new { id = enr.EnrollmentId }, enr);
        }

        // PUT: /api/enrollments/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Enrollment input)
        {
            if (id != input.EnrollmentId) return BadRequest();
            _db.Entry(input).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/enrollments/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enr = await _db.Enrollments.FindAsync(id);
            if (enr is null) return NotFound();
            _db.Enrollments.Remove(enr);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

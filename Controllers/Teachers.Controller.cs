using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TeachersController(ApplicationDbContext db) => _db = db;

        // GET: /api/teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetAll()
            => await _db.Teachers.ToListAsync();

        // GET: /api/teachers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Teacher>> GetOne(int id)
        {
            var teacher = await _db.Teachers.FindAsync(id);
            return teacher is null ? NotFound() : teacher;
        }

        // POST: /api/teachers
        [HttpPost]
        public async Task<ActionResult<Teacher>> Create(Teacher teacher)
        {
            _db.Teachers.Add(teacher);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOne), new { id = teacher.TeacherId }, teacher);
        }

        // PUT: /api/teachers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Teacher input)
        {
            if (id != input.TeacherId) return BadRequest();
            _db.Entry(input).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/teachers/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _db.Teachers.FindAsync(id);
            if (teacher is null) return NotFound();
            _db.Teachers.Remove(teacher);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

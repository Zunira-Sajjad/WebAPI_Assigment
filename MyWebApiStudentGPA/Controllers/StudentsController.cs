using DL.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyWebApiStudentGPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }

        // POST /api/students
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDbDto student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.studentDbDto.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentId }, student);
        }

        // PUT /api/students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDbDto updatedStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedStudent.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(updatedStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE /api/students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.studentDbDto.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.studentDbDto.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // GET /api/students/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _context.studentDbDto.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _context.studentDbDto.ToListAsync();

            if (students == null || students.Count == 0)
            {
                return NotFound("No students found.");
            }

            return Ok(students);
        }



        private bool StudentExists(int id)
        {
            return _context.studentDbDto.Any(e => e.StudentId == id);
        }

    }
}

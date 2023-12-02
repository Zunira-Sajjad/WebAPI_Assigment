using DL.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyWebApiStudentGPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : Controller
    {
         private readonly StudentDbContext _context;

         public SubjectsController(StudentDbContext context)
         {
                 _context = context;
         }

        // POST /api/subjects
        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectDbDto subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.subjectDbDto.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubjectById), new { id = subject.SubjectId }, subject);
        }
        // PUT /api/subjects/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectDbDto updatedSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedSubject.SubjectId)
            {
                return BadRequest();
            }

            _context.Entry(updatedSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // DELETE /api/subjects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.subjectDbDto.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.subjectDbDto.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // GET /api/subjects
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _context.subjectDbDto.ToListAsync();

            if (subjects == null || subjects.Count == 0)
            {
                return NotFound("No subjects found.");
            }

            return Ok(subjects);
        }

  
        // GET /api/subjects/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var subject = await _context.subjectDbDto.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        private bool SubjectExists(int id)
        {
            return _context.subjectDbDto.Any(e => e.SubjectId == id);
        }
    }
}

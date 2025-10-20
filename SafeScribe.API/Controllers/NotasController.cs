using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeScribe.API.Helpers;
using SafeScribe.API.Models;
using System.Security.Claims;

namespace SafeScribe.API.Controllers
{
    [ApiController]
    [Route("api/v1/notas")]
    [Authorize] 
    public class NotasController : ControllerBase
    {
        private static readonly List<Note> _notes = new();

        [HttpPost] // Endpoint: POST /api/v1/notas
        [Authorize(Roles = $"{Roles.Admin},{Roles.Editor}")]
        public IActionResult CreateNote([FromBody] Note newNote)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var note = new Note
            {
                Id = _notes.Count + 1,
                Title = newNote.Title,
                Content = newNote.Content,
                CreatedAt = DateTime.UtcNow,
                UserId = userId 
            };

            _notes.Add(note);
            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        [HttpGet("{id}")] // Endpoint: GET /api/v1/notas/1
        public IActionResult GetNoteById(int id)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);
            if (note == null) return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userRole = User.FindFirstValue(ClaimTypes.Role)!;

            if (note.UserId != userId && userRole != Roles.Admin)
            {
                return Forbid(); 
            }

            return Ok(note);
        }

        [HttpDelete("{id}")] // Endpoint: DELETE /api/v1/notas/1
        [Authorize(Roles = Roles.Admin)]
        public IActionResult DeleteNote(int id)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);
            if (note == null) return NotFound();

            _notes.Remove(note);

            return NoContent();
        }
    }
}
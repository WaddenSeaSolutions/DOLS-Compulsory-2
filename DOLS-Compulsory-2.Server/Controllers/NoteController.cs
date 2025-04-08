using Microsoft.AspNetCore.Mvc;
using dols_compulsory_2.Server.Models;
using dols_compulsory_2.Server.Services;

namespace dols_compulsory_2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController(NoteService noteService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllNotes()
        {
            var notes = noteService.GetAll();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public IActionResult GetNoteById(int id)
        {
            var note = noteService.GetById(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] NoteDTO noteDto)
        {
            var newNote = noteService.Create(noteDto);
            return CreatedAtAction(nameof(GetNoteById), new { id = newNote.Id }, newNote);
        }

        [HttpGet("search")]
        public IActionResult SearchNotes([FromQuery] string query)
        {
            var results = noteService.Search(query);
            return Ok(results);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using dols_compulsory_2.Server.Models;
using dols_compulsory_2.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace dols_compulsory_2.Server.Controllers
{
    [ApiController]
    [Route("api/Note")]
    public class NoteController : ControllerBase 
    {
        private readonly NoteService _noteService; 

        public NoteController(NoteService noteService) 
        {
            _noteService = noteService;
        }

        [HttpGet]
        public IActionResult GetAllNotes()
        {
            var notes = _noteService.GetAll();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public IActionResult GetNoteById(int id)
        {
            var note = _noteService.GetById(id); 
            if (note == null)
                return NotFound();

            return Ok(note);
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] NoteDTO noteDto)
        {
            var newNote = _noteService.Create(noteDto); 
            if (newNote == null)
            {
                return NotFound();
            }
            return Ok(newNote);
        }

        [HttpGet("search")]
        public IActionResult SearchNotes([FromQuery] string query)
        {
            var results = _noteService.Search(query); 
            return Ok(results);
        }
    }
}
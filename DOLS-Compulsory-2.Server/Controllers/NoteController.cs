using Microsoft.AspNetCore.Mvc;
using dols_compulsory_2.Server.Models;
using dols_compulsory_2.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Flagsmith;
using DOLS_Compulsory_2.Server.Services;

namespace dols_compulsory_2.Server.Controllers
{
    [ApiController]
    [Route("api/Note")]
    public class NoteController : ControllerBase 
    {
        private readonly NoteService _noteService; 

        private readonly FeatureFlaggingService _featureFlaggingService;

        public NoteController(NoteService noteService, FeatureFlaggingService featureFlaggingService) 
        {
            _noteService = noteService;
            _featureFlaggingService = featureFlaggingService;
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
        public async Task<IActionResult> SearchNotes([FromQuery] string query)
        {
            bool isEnabled = await _featureFlaggingService.IsFeatureEnabled("search");
            if (isEnabled)
            {
                Console.WriteLine("Feature flag is enabled");
                return Ok("Feature flag is enabled");
            }
            return NotFound();
        }
    }
}
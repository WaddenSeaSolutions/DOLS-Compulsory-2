using Microsoft.AspNetCore.Mvc;
using dols_compulsory_2.Server.Models;
using dols_compulsory_2.Server.Services;
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
                return Ok(_noteService.Search(query));
            }
            return NotFound();
        }

        [HttpGet("feature-flag")]
        public async Task<IActionResult> GetFeatureFlagStatus([FromQuery] string featureName)
        {
            Console.WriteLine("Modtaget");
            Console.WriteLine(featureName);
            try
            {
                bool isEnabled = await _featureFlaggingService.IsFeatureEnabled(featureName);
                return Ok(new { featureName, isEnabled });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while checking the feature flag.", details = ex.Message });
            }
        }

        [HttpPost("init-db")]
        public async Task<IActionResult> InitializeDatabase()
        {
            try
            {
                await _noteService.InitializeDatabase();

                return Ok(new { message = "Database initialized successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during database initialization.", details = ex.Message });
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendNotas.Models;
using BackendNotas.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendNotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            var notes = await _noteService.GetNotesAsync();
            return Ok(notes);
        }

        [HttpPost]
        public async Task<ActionResult<Note>> CreateNoteAsync(Note note)
        {
            note.createdAt = DateTime.Now;
            await _noteService.CreateNoteAsync(note);
            return CreatedAtAction(nameof(GetNotes), new { id = note.id }, note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteAsync(string id)
        {
            await _noteService.DeleteNoteAsync(id);
            return NoContent();
        }
    }
}

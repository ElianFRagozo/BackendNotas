using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendNotas.Models;
using BackendNotas.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
    // Generar un nuevo ObjectId válido
    var newId = ObjectId.GenerateNewId().ToString();

    // Asignar el nuevo ObjectId a la nota
    note.id = newId;

    // Establecer la fecha de creación
    note.createdAt = DateTime.Now;

    // Llamar al servicio para crear la nota
    await _noteService.CreateNoteAsync(note);

    // Devolver la respuesta con el código 201 y la nota creada
    return CreatedAtAction(nameof(GetNotes), new { _id = note.id }, note);
}



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteAsync(string id)
        {
            await _noteService.DeleteNoteAsync(id);
            return NoContent();
        }


    }
}

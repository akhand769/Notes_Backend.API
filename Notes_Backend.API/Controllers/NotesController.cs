using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes_Backend.API.Data;
using Notes_Backend.API.Models.Entities;
using System;
using System.Threading.Tasks;

namespace Notes_Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly NotesDbContext _dbContext;

        public NotesController(NotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            // Retrieve all notes from the database
            var notes = await _dbContext.Notes.ToListAsync();
            return Ok(notes);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetNoteById(Guid id)
        {
            // Retrieve a note by its ID from the database
            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] Note note)
        {
            // Add a new note to the database
            note.Id = Guid.NewGuid();
            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateNote(Guid id, [FromBody] Note updatedNote)
        {
            // Update an existing note in the database
            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            note.Title = updatedNote.Title;
            note.Description = updatedNote.Description;
            note.IsVisible = updatedNote.IsVisible;

            await _dbContext.SaveChangesAsync();
            return Ok(note);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            // Delete a note from the database
            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

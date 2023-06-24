using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Notes_Backend.API.Data;
using Notes_Backend.API.Models.Entities;

namespace Notes_Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private NotesDbContext notesDbContext;
        public NotesController(NotesDbContext notesDbContext)
        {
            this.notesDbContext = notesDbContext;  
        }
        [HttpGet]
         public async Task<IActionResult> GetAllNotes()
        {
            //Get nodes from the database
            return Ok(await notesDbContext.Notes.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        //[ActionName("GetNoteById")]
        public async Task<IActionResult> GetNoteById(Guid id)
        {
             var note = await notesDbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
            if(note==null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(Note note)
        {
            note.Id = Guid.NewGuid();
            await notesDbContext.Notes.AddAsync(note);
            await notesDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNoteById),new {id = note.Id},note);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<ActionResult> UpdateNote(Guid id, Note updatedNote)
        {
            var note = await notesDbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            else
            {
                note.Title = updatedNote.Title;
                note.Description = updatedNote.Description; 
                note.IsVisible = updatedNote.IsVisible;
                await notesDbContext.SaveChangesAsync();
                return Ok(note);
            }
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            Note note = await notesDbContext.Notes.FirstOrDefaultAsync(x=>x.Id == id);
            if(note == null)
            {
                return NotFound();
            }
            notesDbContext.Notes.Remove(note);
            await notesDbContext.SaveChangesAsync();
            return Ok();
        }

    }
}

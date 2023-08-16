using Microsoft.EntityFrameworkCore;
using Notes_Backend.API.Models.Entities;

namespace Notes_Backend.API.Data
{
    // DbContext class for interacting with the database
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet representing the "Notes" table in the database
        public DbSet<Note> Notes { get; set; }
    }
}

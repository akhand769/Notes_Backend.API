using Microsoft.EntityFrameworkCore;
using Notes_Backend.API.Models.Entities;

namespace Notes_Backend.API.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Note> Notes { get; set; }
    }
}

namespace Notes_Backend.API.Models.Entities
{
    // Represents a Note entity in the database
    public class Note
    {
        // Unique identifier for the note
        public Guid Id { get; set; }

        // Title of the note
        public string Title { get; set; }

        // Description content of the note
        public string Description { get; set; }

        // Indicates whether the note is visible or not
        public bool IsVisible { get; set; }
    }
}

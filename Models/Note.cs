using System;

namespace Notes.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate {get; set; }
        public DateTime UpdatedDate {get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
    public class Note : BaseModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
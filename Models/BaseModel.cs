using System;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate {get; set; }
        public DateTime UpdatedDate {get; set; }
    }
}
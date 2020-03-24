using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Notes.Model;

namespace Notes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        public NoteController()
        {
            
        }

        [HttpGet]
        public IEnumerable<Note> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Note
            {
                Id = rng.Next(1,10),
                Title = "asdfas",
                Description = "asdfaasdf sd fa sdfa asdf sadf asdf asdf asdfasdfasd fasdf asdf asdf",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            })
            .ToArray();
        }
    }
}
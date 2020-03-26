using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notes.Models;
using Notes.Services;

namespace Notes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : BaseController<Note, INoteService>
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService) : base(noteService)
        {
            _noteService = noteService;
        }
        
        [HttpGet]
        public async override Task<IReadOnlyList<Note>> Get()
        {
            return await _noteService.GetAll();

            // var rng = new Random();
            // return Enumerable.Range(1, 5).Select(index => new Note
            // {
            //     Id = rng.Next(1,10),
            //     Title = "asdfas",
            //     Description = "asdfaasdf sd fa sdfa asdf sadf asdf asdf asdfasdfasd fasdf asdf asdf",
            //     CreatedDate = DateTime.Now,
            //     UpdatedDate = DateTime.Now
            // })
            // .ToList();
        }
    }
}
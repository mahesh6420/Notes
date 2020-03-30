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
    [Route("api/[controller]")]
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
        }
        
        [HttpPost]
        public async override Task<DataResult<Note>> Create(Note model)
        {
          return await _noteService.Create(model);
        }

         [HttpDelete("{id}")]
        public async override Task<DataResult> Delete(int id)
        {
            return await _noteService.Delete(id);
        }
    }
}
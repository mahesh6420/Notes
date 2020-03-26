using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Notes.Common.Enums;
using Notes.Models;
using Notes.Repositories;

namespace Notes.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repository;
        public NoteService(INoteRepository repository)
        {
            _repository = repository;
        }

        public async Task<DataResult> Create(Note viewModel)
        {
            viewModel.CreatedDate = DateTime.Now;
            viewModel.UpdatedDate = DateTime.Now;

            return await _repository.Create(viewModel);
        }

        public async Task<DataResult> Delete(int id)
        {
            Note note = await _repository.GetById<Note,int>(id);

            if(note == null) return new DataResult { Status = Status.Failed, Message = "Doesn't Exist."};

            return await _repository.Delete(note);
        }

        public async Task<IReadOnlyList<Note>> GetAll()
        {
            return await _repository.GetAllAsync<Note>().ToListAsync();
        }

        public async Task<Note> GetById(int key)
        {
            return await _repository.GetById<Note, int>(key);
        }

        public async Task<DataResult> Update(Note viewModel)
        {
            viewModel.UpdatedDate = DateTime.Now;

            return await _repository.Update(viewModel);
        }
    }
}
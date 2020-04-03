using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Models;
using Microsoft.EntityFrameworkCore;
using Notes.Common.Enums;
using Notes.Models;
using Notes.Repositories;
using System.Linq;
using Notes.ViewModels;

namespace Notes.Services
{
    public class NoteService : INoteService
    {
        DataResult _result = new DataResult();
        private readonly INoteRepository _repository;
        public NoteService(INoteRepository repository)
        {
            _repository = repository;
        }

        public async Task<DataResult<Note>> Create(Note viewModel)
        {
            viewModel.CreatedDate = DateTime.Now;
            viewModel.UpdatedDate = DateTime.Now;

            return await _repository.Create(viewModel);
        }

        public async Task<DataResult> Delete(int id)
        {
            Note note = await _repository.GetById<Note,int>(id);

            if(note == null) 
            {
                _result.Status = Status.Failed;
                _result.Message = "Doesn't Exist.";

                return _result;
            }

            return await _repository.Delete(note);
        }

        public async Task<IReadOnlyList<Note>> GetAll(QueryParamViewModel queryParam)
        {
            queryParam.SearchText = queryParam.SearchText ?? "";

            return await _repository.GetAllAsync<Note>()
            .Where(x => x.Title.Contains(queryParam.SearchText) || x.Description.Contains(queryParam.SearchText))
            .Skip(queryParam.Skip)
            .Take(queryParam.Take)
            .ToListAsync();
        }

        public async Task<Note> GetById(int key)
        {
            return await _repository.GetById<Note, int>(key);
        }
        
        public async Task<int> GetTotalCount()
        {
            return await _repository.GetAllAsync<Note>().CountAsync();
        }
        public async Task<DataResult<Note>> Update(Note viewModel)
        {
            var result = new DataResult<Note>();
            Note note = await _repository.GetById<Note,int>(viewModel.Id);

            if(note == null) 
            {
                result.Status = Status.Failed;
                result.Message = "Doesn't Exist.";

                return result;
            }


            note.Title = viewModel.Title;
            note.Description = viewModel.Description;
            note.UpdatedDate = DateTime.Now;
            
            return await _repository.Update(note);
        }
    }
}
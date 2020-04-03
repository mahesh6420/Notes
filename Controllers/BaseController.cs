using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notes.Services;
using Notes.Models;
using Notes.ViewModels;

namespace Notes.Controllers
{
    public class BaseController<T, TService> : ControllerBase where T : class where TService : class
    {
        public TService _service;
        public BaseController(TService service)
        {
            _service = service;
        }

        [HttpGet("totalCount")]
        public virtual async Task<int> Get()
        {
            throw new NotImplementedException();
        } 

        [HttpGet]
        public virtual async Task<IReadOnlyList<T>> Get(QueryParamViewModel pagination)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public virtual async Task<DataResult<T>> Create(T model)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public virtual async Task<DataResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public virtual async  Task<DataResult<T>> Update(Note model)
        {
            throw new NotImplementedException();
        }
    }
}
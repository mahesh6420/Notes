using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Models;
using Notes.Models;

namespace Notes.Services
{
    public interface INoteService : IBaseService<Note, Note, int>
    {
    }
}

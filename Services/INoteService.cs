using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Models;
using Notes.Models;

namespace Notes.Services
{
    public interface INoteService: IBaseService<Note, Note, int>
    {
    }
}

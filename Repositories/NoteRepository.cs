using Notes.Repositories;
using Notes.Data;

namespace Notes.Repositories
{
    public class NoteRepository : BaseRepository, INoteRepository
    {
        private readonly ApplicationDbContext _context;
        public NoteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
using Notes.Common.Enums;

namespace Notes.ViewModels
{
    public class PaginationViewModel
    {
        public int PageNo { get; set; } = 1;
        public int Take { get; set; } = 10;
        public int Skip => (PageNo - 1) * Take;
    }
}

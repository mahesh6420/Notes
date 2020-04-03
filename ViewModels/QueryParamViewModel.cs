using Notes.Common.Enums;

namespace Notes.ViewModels
{
    public class QueryParamViewModel
    {
        public string SearchText { get; set; } = "";
        public int PageNo { get; set; } = 1;
        public int Take { get; set; } = 10;
        public int Skip => (PageNo - 1) * Take;
    }
}

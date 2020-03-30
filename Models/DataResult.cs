using Notes.Common.Enums;

namespace Notes.Models
{
    public class DataResult
    {
        public Status Status { get; set; }
        public string Message { get; set; }
        public string ReturnId { get; set; }
    }

    public class DataResult<T>
    {
        public Status Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
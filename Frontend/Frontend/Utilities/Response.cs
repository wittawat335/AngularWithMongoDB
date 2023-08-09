namespace Frontend.Utilities
{
    public class Response<T>
    {
        public T Value { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
    public class ResponseStatus
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class ResponseListValue<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<T> List { get; set; }
    }
}

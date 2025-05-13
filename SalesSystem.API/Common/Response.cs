namespace SalesSystem.API.Common
{
    public class Response<T>
    {
        public bool Success { get; set; }

        public T? Value { get; set; }

        public string? ErrorMessage { get; set; }
    }
}

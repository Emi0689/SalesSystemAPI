namespace SalesSystem.API.Utilities
{
    public class Response<T>
    {
        public bool Status { get; set; }

        public T Value { get; set; }

        public string? ErrorMessage { get; set; }
    }
}

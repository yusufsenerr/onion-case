namespace API.Common.Domain.Commons
{
    public class BaseResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
    }
}

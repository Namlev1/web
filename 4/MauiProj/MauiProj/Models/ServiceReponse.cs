
namespace MauiProj.Models
{
    public class ServiceReponse<T>
    {
        public T? Data { get; set; } = default;
        public bool Success { get; set; } = false;
        public string Message { get; set; } = null;
    }
}

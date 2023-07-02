namespace Newshore.api.Integration
{
    public interface IHttpClientService<T>
    {
        Task<List<T>> GetAsync();
    }
}

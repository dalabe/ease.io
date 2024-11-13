namespace Ease.Services
{
    public class ICacheService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T data, TimeSpan expiration);
        object RemoveMetadata(string key);
    }
}

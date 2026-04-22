namespace Millenium_rekru.Services;

public interface ICacheService
{
    void Set(string key, string value, int ttlInMinutes);
    void Get(string key);
}
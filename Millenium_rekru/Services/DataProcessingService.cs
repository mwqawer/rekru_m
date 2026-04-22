using Millenium_rekru.Exceptions;

namespace Millenium_rekru.Services;

public class DataProcessingService(ICacheService _cacheService) : IDataProcessingService
{
    public async Task ProcessDataAsync(string key, string data)
    {
        _cacheService.Set(key, "", 5);
        await Task.Delay(TimeSpan.FromSeconds(60));
        _cacheService.Set(key, data, 5);
    }

    public string GetData(string key)
    {
        var val = _cacheService.Get(key);
        if (val == null)
        {
            throw new DataNotFoundException();
        }
        
        if (val == "")
        {
            throw new ProcessingPendingException();
        }

        return val;
    }

    public IDataProcessingService.Status GetStatus(string key)
    {
        var val = _cacheService.Get(key);
        if (val == null)
        {
            throw new DataNotFoundException();
        }

        
        return val == "" ? IDataProcessingService.Status.Processing : IDataProcessingService.Status.Completed; 
    }
}
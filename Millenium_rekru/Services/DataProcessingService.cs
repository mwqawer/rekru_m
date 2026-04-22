using Millenium_rekru.Exceptions;

namespace Millenium_rekru.Services;

public class DataProcessingService : IDataProcessingService
{
    public static Dictionary<string, bool> ProcessedData = new Dictionary<string, bool>();
    public async Task ProcessDataAsync(string key)
    {
        ProcessedData.Add(key, false);
        await Task.Delay(TimeSpan.FromSeconds(60));
        ProcessedData[key] = true;
    }

    public string GetData(string key)
    {
        if (!ProcessedData.TryGetValue(key, out bool value))
        {
            throw new DataNotFoundException();
        }

        if (!value)
        {
            throw new ProcessingPendingException();
        }

        return value.ToString();
    }

    public IDataProcessingService.Status GetStatus(string key)
    {
        if (!ProcessedData.TryGetValue(key, out bool value))
        {
            throw new DataNotFoundException();
        }
        
        return value? IDataProcessingService.Status.Completed : IDataProcessingService.Status.Processing; 
    }
}
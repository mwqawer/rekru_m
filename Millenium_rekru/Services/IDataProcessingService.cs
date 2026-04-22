namespace Millenium_rekru.Services;

public interface IDataProcessingService
{
    enum Status { Processing, Completed }
    Task ProcessDataAsync(string key, string data);
    string GetData(string key);
    Status GetStatus(string key);
}